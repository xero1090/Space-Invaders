using SpaceInvaders.Characters;
using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace SpaceInvaders
{
    /// <summary>
    /// The page the actual game runs on!
    /// </summary>
    public sealed partial class ClassicGame : Page
    {
        // Constants
        private const int INTERVAL = 10;
        private const byte FIRE_WAIT = 10;
        private const byte DEFAULT_ENEMY_WAIT_MOD = 3;
        private const byte DEFAULT_ENEMY_WAIT = 50;
        private const byte WAIT = 10;

        // Field Variables
        private bool _canFire;
        private bool _gameOn;
        private bool _lose;
        private byte _counter;
        private byte _enemyWait;
        private byte _enemyMOD;
        private double _position;

        // Objects
        private SpaceInvaders _game;
        private PlayerTurret _playerTurret;
        private DispatcherTimer _timer;

        // Images
        private ImageBrush _imgRocket;
        private BitmapImage _imgFaceGrin;
        private BitmapImage _imgFaceShoot;
        private ImageBrush _imgTank;
        private ImageBrush _imgTankFire;
        private ImageBrush _imgExplode;
        private List<ImageBrush> _imgEnemies;
        private List<ImageBrush> _imgPowerUps;

        // Music Players
        private MediaPlayer soundplayer;
        private MediaPlayer musicplayer;

        /// <summary>
        /// Intitializes the Classic Game Page
        /// </summary>
        public ClassicGame()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += KeyPress; // input
            SoundLoader();
            musicplayer.Volume = 0.2; //0.5 is default
            soundplayer.Volume = 0.3;
            musicplayer.Play(); // UNCOMMENT FOR MUSIC
            SpriteLoader();
            TimerSetup();
            GameSetup();
        }

        /// <summary>
        /// Practically acts as the game loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, object e)
        {
            // Check Bullets
            _game.BulletCheck(_imgExplode, _imgPowerUps, _powerUp);

            // Enemy Movement
            if (_counter % _enemyWait == 0)
            {
                _game.EnemyMove(); //UNCOMMENT FOR MOVEMENT
                EnemySpeed();
                _game.AlienWin();
            }

            // PowerUp Falling and interaction
            if (_counter % WAIT == 0)
            {
                _game.PowerUpMove(); //UNCOMMENT FOR POWERUP FALLING
                if (_game.PowerUpGet())
                {
                    _powerUpType.Text = "Extra Life";
                }
            }

            // Shooting Delay
            if (_counter % FIRE_WAIT == 0)
            {
                _canFire = true;
            }

            // Animation for face and tank
            if (_counter % 20 * FIRE_WAIT == 0)
            {
                _face.Source = _imgFaceGrin;
                _tank.Fill = _imgTank;
            }
            // If player Dies
            if (_game.Player.Lives <= 0)
            {
                // Player Dies
                _gameover.Visibility = Visibility.Visible;
                _scoreButton.Visibility = Visibility.Visible;
                _lose = true;
                _game.End();
            }

            // If enemies are gone
            if (_game.Enemies.Count == 0 && !_lose)
            {
                // All Aliens Die
                _win.Visibility = Visibility.Visible;
                _gameOn = false;
                _counter = 0;
                _powerUpType.Text = "Press E / Enter";
            }

            // Counter Reset
            if (_counter == 201)
            {
                _powerUpType.Text = "";
                _counter = 0;
            }
            _score.Text = $"{_game.Score}";
            _lives.Text = $"{_game.Player.Lives}";
            ++_counter;
        }

        /// <summary>
        /// New Round initiated
        /// </summary>
        private void EndOfRound()
        {
            _win.Visibility = Visibility.Collapsed;
            _game.Enemies.Clear();
            EnemyCreation();
            _powerUpType.Text = "";
            _gameOn = true;
        }

        /// <summary>
        /// Creating the Enemies
        /// </summary>
        /// <param name="firstTime"> First round of the game </param>
        private void EnemyCreation(bool firstTime = false)
        {
            _game.EnemySetup(_imgEnemies, _enemy);
            if (!firstTime)
            {
                --_enemyMOD;
            }
        }

        /// <summary>
        /// Determines the enemy speed
        /// </summary>
        private void EnemySpeed()
        {
            _enemyWait = (byte)(_game.Enemies.Count * _enemyMOD);

            if (_enemyWait > DEFAULT_ENEMY_WAIT)
            {
                _enemyWait = DEFAULT_ENEMY_WAIT;
            }

            // Avoiding destroying the world by avoiding dividing by 0
            if (_enemyWait == 0)
            {
                _enemyWait = 5;
            }
        }

        /// <summary>
        /// When the player shoots a missile
        /// </summary>
        private void PlayerMissile_fired()
        {
            if (_canFire)
            {
                _game.PlayerShoot(_missile); // missile copy creation
                _face.Source = _imgFaceShoot;
                _missile.Fill = _imgRocket;
                _canFire = false;
                soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/explosion2.ogg"));
                soundplayer.Play();
            }
        }

        /// <summary>
        /// Called when the user provides input from the keyboard or a controller
        /// </summary>
        /// <param name="sender"> The actual window of the game </param>
        /// <param name="args"></param>
        private void KeyPress(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            //  Shooting
            if (sender.GetKeyState(Windows.System.VirtualKey.W).HasFlag(CoreVirtualKeyStates.Down) || // W key
                    sender.GetKeyState(Windows.System.VirtualKey.Up).HasFlag(CoreVirtualKeyStates.Down) || // Up arrow key
                    sender.GetKeyState(Windows.System.VirtualKey.Space).HasFlag(CoreVirtualKeyStates.Down) || // Spacebar
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadDPadUp).HasFlag(CoreVirtualKeyStates.Down) || // Up on the Dpad (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadLeftTrigger).HasFlag(CoreVirtualKeyStates.Down) || // Left Trigger (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadRightTrigger).HasFlag(CoreVirtualKeyStates.Down) || // Right Trigger (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadA).HasFlag(CoreVirtualKeyStates.Down) || // A button (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadX).HasFlag(CoreVirtualKeyStates.Down)) // X button (controller)
            {
                PlayerMissile_fired();
            }

            // Moving Left
            if (sender.GetKeyState(Windows.System.VirtualKey.A).HasFlag(CoreVirtualKeyStates.Down) || // A key
                    sender.GetKeyState(Windows.System.VirtualKey.Left).HasFlag(CoreVirtualKeyStates.Down) || // Left arrow key
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadDPadLeft).HasFlag(CoreVirtualKeyStates.Down) || // Left on the Dpad (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadLeftThumbstickLeft).HasFlag(CoreVirtualKeyStates.Down) || // Left on the left thumbstick / analogstick (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadRightThumbstickLeft).HasFlag(CoreVirtualKeyStates.Down)) // Left on the right thumbstick / analogstick (controller)
            {
                _game.PlayerMove(-10);
            }

            // Moving Right
            if (sender.GetKeyState(Windows.System.VirtualKey.D).HasFlag(CoreVirtualKeyStates.Down) || // D key
                       sender.GetKeyState(Windows.System.VirtualKey.Right).HasFlag(CoreVirtualKeyStates.Down) || // Right arrow key
                       sender.GetKeyState(Windows.System.VirtualKey.GamepadDPadRight).HasFlag(CoreVirtualKeyStates.Down) || // Right on the Dpad (controller)
                       sender.GetKeyState(Windows.System.VirtualKey.GamepadLeftThumbstickRight).HasFlag(CoreVirtualKeyStates.Down) || // Right on the left thumbstick / analogstick (controller)
                       sender.GetKeyState(Windows.System.VirtualKey.GamepadRightThumbstickRight).HasFlag(CoreVirtualKeyStates.Down)) // Right on the right thumbstick / analogstick (controller)
            {
                _game.PlayerMove(10);
            }

            if (sender.GetKeyState(Windows.System.VirtualKey.Enter).HasFlag(CoreVirtualKeyStates.Down) || // Enter Key
                    sender.GetKeyState(Windows.System.VirtualKey.E).HasFlag(CoreVirtualKeyStates.Down) || // E Key
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadY).HasFlag(CoreVirtualKeyStates.Down) || // Y button (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadB).HasFlag(CoreVirtualKeyStates.Down)) // B button (controller)
            {
                if ((!_gameOn))
                {
                    EndOfRound();
                    _enemyWait = DEFAULT_ENEMY_WAIT;
                }

                if (_lose)
                {
                    _lose = false;
                }
            }
        }

        // Loading methods past this line

        /// <summary>
        /// Loading Images into field variables
        /// </summary>
        private void SpriteLoader()
        {
            // Singular Images
            _imgFaceGrin = new BitmapImage(new Uri("ms-appx:///Assets/Face Grin.png"));
            _imgFaceShoot = new BitmapImage(new Uri("ms-appx:///Assets/Face shoot.png"));
            _imgTank = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/tank.png")) };
            _imgTankFire = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/tank fire.png")) };
            _imgRocket = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Missile.png")) };
            _imgExplode = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Splosion.png")) };

            // Enemy Images
            _imgEnemies = new List<ImageBrush>();
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyRed.png")) });
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyGreen.png")) });
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyBlue.png")) });
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyBlack.png")) });

            // PowerUps 
            _imgPowerUps = new List<ImageBrush>();
            _imgPowerUps.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Powerups/powerupYellow_bolt.png")) }); // Extra Life
            _imgPowerUps.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Powerups/powerupGreen_bolt.png")) }); // Extra Points
        }

        /// <summary>
        /// Setting up the Sound Players and assets
        /// </summary>
        private void SoundLoader()
        {
            soundplayer = new MediaPlayer();
            musicplayer = new MediaPlayer();
            musicplayer.Pause();
            musicplayer.Source = null;
            musicplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/classic.mp3"));
        }

        /// <summary>
        /// Setting up the dispatch timer
        /// </summary>
        private void TimerSetup()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            _timer.Start();
        }

        /// <summary>
        /// Setting up the actual SpaceInvaders.cs file
        /// </summary>
        private void GameSetup()
        {
            _position = (double)_tank.GetValue(Canvas.LeftProperty);
            _playerTurret = new PlayerTurret(_position, (double)_tank.GetValue(Canvas.TopProperty), _imgTank, _imgTankFire, _tank);
            _game = new SpaceInvaders(ref _playerTurret, _canvas, _enemy.Width, ref _barrier);
            _canFire = true;
            _counter = 1;
            _enemyMOD = DEFAULT_ENEMY_WAIT_MOD;
            EnemyCreation(true);
            EnemySpeed();
            _gameOn = true;
            _lose = false;
        }

        private void ToScore(object sender, RoutedEventArgs e)
        {
            musicplayer.Pause();
            _canvas.Children.Clear();
            this._page = null;
            this.Frame.Navigate(typeof(GameOver), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
