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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassicGame : Page
    {
        MediaPlayer soundplayer;
        public ClassicGame()
        {
            this.InitializeComponent();
            soundplayer = new MediaPlayer();
            soundplayer.Pause();
            soundplayer.Source = null;
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/classic.mp3"));
            //soundplayer.Play();
            
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

        private void KeyPress(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                //  Shooting
                case Windows.System.VirtualKey.W: // W key
                case Windows.System.VirtualKey.Up: // Up arrow key
                case Windows.System.VirtualKey.Space: // Spacebar
                case Windows.System.VirtualKey.GamepadDPadUp: // Up on the Dpad (controller)
                case Windows.System.VirtualKey.GamepadLeftTrigger: // Left Trigger (controller)
                case Windows.System.VirtualKey.GamepadRightTrigger: // Right Trigger (controller)
                case Windows.System.VirtualKey.GamepadA: // A button (controller)
                case Windows.System.VirtualKey.GamepadX: // X button (controller)
                    //TODO: SHOOT
                    soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/boom.mp3"));
                    soundplayer.Play();
                    break;

                // Moving Left
                case Windows.System.VirtualKey.A: // A key
                case Windows.System.VirtualKey.Left: // Left arrow key
                case Windows.System.VirtualKey.GamepadDPadLeft: // Left on the Dpad (controller)
                case Windows.System.VirtualKey.GamepadLeftThumbstickLeft: // Left on the left thumbstick / analogstick (controller)
                case Windows.System.VirtualKey.GamepadRightThumbstickLeft: // Left on the right thumbstick / analogstick (controller)
                    //TODO: Move Left
                    break;

                // Moving Right
                case Windows.System.VirtualKey.D: // D key
                case Windows.System.VirtualKey.Right: // Right arrow Key
                case Windows.System.VirtualKey.GamepadDPadRight: // Right on the Dpad (controller)
                case Windows.System.VirtualKey.GamepadLeftThumbstickRight: // Right on the left thumbstick / analogstick (controller)
                case Windows.System.VirtualKey.GamepadRightThumbstickRight: // Right on the right thumbstick / analogstick (controller)
                    //TODO: Move Right
                    break;

            }
        }
    }
}
