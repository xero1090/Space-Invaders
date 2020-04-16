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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassicGame : Page
    {
        double _position;

        BitmapImage _imgFaceGrin;
        BitmapImage _imgFaceShoot;
        ImageBrush _imgTank;
        ImageBrush _imgTankFire;
        ImageBrush _rocket; 
        MediaPlayer soundplayer;
        MediaPlayer musicplayer;
        public ClassicGame()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += KeyPress;
            soundplayer = new MediaPlayer();
            musicplayer = new MediaPlayer();
            musicplayer.Pause();
            musicplayer.Source = null;
            musicplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/classic.mp3"));
            //musicplayer.Play(); // UNCOMMENT FOR LOUD MUSIC

            _position = (double) _tank.GetValue(Canvas.LeftProperty);
            _rocket = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Missile.png"))};
            _imgFaceGrin = new BitmapImage(new Uri("ms-appx:///Assets/Face Grin.png"));
            _imgFaceShoot = new BitmapImage(new Uri("ms-appx:///Assets/Face shoot.png"));
            _imgTank = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/tank.png")) };
            _imgTankFire = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/tank fire.png")) };
        }
        public void PlayerMissile_fired()
        {
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/laser2.ogg"));
            soundplayer.Play();
        }
        public void EnemyMissile_fired()
        {
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/laser3.ogg"));
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
                //TODO: SHOOT
                _missile.Fill = _rocket; 
                _face.Source = _imgFaceShoot;
                _tank.Fill = _imgTankFire;
                soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/boom.mp3"));
                soundplayer.Play();
                _tank.Fill = _imgTank;
                _missile.Visibility = 0;
                
            }
            else
            {
                _face.Source = _imgFaceGrin;
                _tank.Fill = _imgTank;
            }

            // Moving Left
            if (sender.GetKeyState(Windows.System.VirtualKey.A).HasFlag(CoreVirtualKeyStates.Down) || // A key
                    sender.GetKeyState(Windows.System.VirtualKey.Left).HasFlag(CoreVirtualKeyStates.Down) || // Left arrow key
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadDPadLeft).HasFlag(CoreVirtualKeyStates.Down) || // Left on the Dpad (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadLeftThumbstickLeft).HasFlag(CoreVirtualKeyStates.Down) || // Left on the left thumbstick / analogstick (controller)
                    sender.GetKeyState(Windows.System.VirtualKey.GamepadRightThumbstickLeft).HasFlag(CoreVirtualKeyStates.Down)) // Left on the right thumbstick / analogstick (controller)
            {
                if (_position > 0)
                {
                    _position -= 10;
                }
            }

            // Moving Right
            if (sender.GetKeyState(Windows.System.VirtualKey.D).HasFlag(CoreVirtualKeyStates.Down) || // D key
                       sender.GetKeyState(Windows.System.VirtualKey.Right).HasFlag(CoreVirtualKeyStates.Down) || // Right arrow key
                       sender.GetKeyState(Windows.System.VirtualKey.GamepadDPadRight).HasFlag(CoreVirtualKeyStates.Down) || // Right on the Dpad (controller)
                       sender.GetKeyState(Windows.System.VirtualKey.GamepadLeftThumbstickRight).HasFlag(CoreVirtualKeyStates.Down) || // Right on the left thumbstick / analogstick (controller)
                       sender.GetKeyState(Windows.System.VirtualKey.GamepadRightThumbstickRight).HasFlag(CoreVirtualKeyStates.Down)) // Right on the right thumbstick / analogstick (controller)
            {
                if (_position < (_canvas.ActualWidth - _tank.Width))
                {
                    _position += 10;
                }
            }

            // Sending in the change
            _tank.SetValue(Canvas.LeftProperty, _position);
        }
    }
}
