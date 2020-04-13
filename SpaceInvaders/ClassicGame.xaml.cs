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

    }
}
