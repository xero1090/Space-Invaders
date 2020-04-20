using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Core;
using Windows.Media.Playback;
using System.Drawing;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Characters;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassicGame : Page
    {
        // Constants
        const int INTERVAL = 10;
        const byte FIRE_WAIT = 10;
        const byte  DEFAULT_ENEMY_WAIT_MOD = 3;
        const byte DEFAULT_ENEMY_WAIT = 50;

        // Field Variables
        bool _canFire;
        bool _gameOn;
        uint _counter;
        byte _enemyWait;
        byte _enemyMOD;

        // Objects
        double _position;
        SpaceInvaders _game;
        PlayerTurret _playerTurret;
        DispatcherTimer _timer;

        // Images
        ImageBrush _imgRocket;
        BitmapImage _imgFaceGrin;
        BitmapImage _imgFaceShoot;
        ImageBrush _imgTank;
        ImageBrush _imgTankFire;
        ImageBrush _imgExplode;
        ImageBrush _imgEnemyDeath;
        List<ImageBrush> _imgEnemies;

        MediaPlayer soundplayer;
        MediaPlayer musicplayer;
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
            _game.BulletCheck(_imgExplode);

            if (_counter % _enemyWait == 0)
            {
                _game.EnemyMove(); //UNCOMMENT FOR MOVEMENT
                EnemySpeed();
            }

            if (_counter%FIRE_WAIT == 0)
            {
                _canFire = true;
            }

            if (_counter % 20*FIRE_WAIT == 0)
            {
                _face.Source = _imgFaceGrin;
                _tank.Fill = _imgTank;
            }

            if (_game.Player.Lives == 0)
            {
                // Player Dies
                _gameover.Visibility = Visibility.Visible;
            }

            if (_game.Enemies.Count == 0)
            {
                // All Aliens Die
                _win.Visibility = Visibility.Visible;
                _gameOn = false;
                _counter = 0;
            }

            if (_counter == 100001)
            {
                _counter = 0;
            }
            _score.Text = $"{_game.Score}";
            ++_counter;
        }
        
        private void EndOfRound()
        {
            _win.Visibility = Visibility.Collapsed;
            _game.Enemies.Clear();
            EnemyCreation();
            _gameOn = true;
        }

        private void EnemyCreation(bool firstTime = false)
        {
            _game.EnemySetup(_imgEnemies, _enemy);
            if (!firstTime)
            {
                --_enemyMOD;
            }
        }

        private void EnemySpeed()
        {
            _enemyWait = (byte) ( _game.Enemies.Count * _enemyMOD);

            if (_enemyWait > DEFAULT_ENEMY_WAIT)
            {
                _enemyWait = DEFAULT_ENEMY_WAIT;
            }

            // Avoiding destroying the world by dividing 
            if (_enemyWait == 0)
            {
                _enemyWait = 1;
            }
        }

        private void PlayerMissile_fired()
        {
            if (_canFire)
            {
                _game.PlayerShoot(_missile); // missile copy creation
                _face.Source = _imgFaceShoot;
                _missile.Fill = _imgRocket;
                _canFire = false;
                soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/boom.mp3"));
                soundplayer.Play();
            }
        }
        private void EnemyMissile_fired()
        {
         // _game.EnemyShoot(_laser);
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/laser3.ogg"));
            soundplayer.Play();
        }
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
            }
        }

        private void SpriteLoader()
        {
            _imgFaceGrin = new BitmapImage(new Uri("ms-appx:///Assets/Face Grin.png"));
            _imgFaceShoot = new BitmapImage(new Uri("ms-appx:///Assets/Face shoot.png"));
            _imgTank = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/tank.png")) };
            _imgTankFire = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/tank fire.png")) };
            _imgRocket = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Missile.png")) };
            _imgExplode = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Splosion.png")) };
            _imgEnemyDeath = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Explode4.png")) };

            _imgEnemies = new List<ImageBrush>();
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyRed.png")) }); 
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyGreen.png")) }); 
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyBlue.png")) }); 
            _imgEnemies.Add(new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemyBlack.png")) });

        }

        private void SoundLoader()
        {
            soundplayer = new MediaPlayer();
            musicplayer = new MediaPlayer();
            musicplayer.Pause();
            musicplayer.Source = null;
            musicplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/classic.mp3"));
        }

        private void TimerSetup()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            _timer.Start();
        }

        private void GameSetup()
        {
            _position = (double)_tank.GetValue(Canvas.LeftProperty);
            _playerTurret = new PlayerTurret(_position, (double)_tank.GetValue(Canvas.TopProperty), _imgTank, _imgTankFire, _tank);
            _game = new SpaceInvaders(ref _playerTurret, _canvas, _enemy.Width);
            _canFire = true;
            _counter = 1;
            _enemyMOD = DEFAULT_ENEMY_WAIT_MOD;
            EnemyCreation(true);
            EnemySpeed();
            _gameOn = true;
        }
    }
}
