using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOver : Page
    {
        private string name;
        MediaPlayer soundplayer;

        public GameOver()
        {
            this.InitializeComponent();
            soundplayer = new MediaPlayer();
            soundplayer.Volume = 0.3;
            soundplayer.Pause();
            soundplayer.Source = null;

        }

        private void SubmitScore(object sender, RoutedEventArgs e)
        {
            name = _name.Text;
            _scores.Text = name + " -------------- " + "800";
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/sans-screm.mp3"));
            soundplayer.Play();
            _submitScore.Visibility = Visibility.Collapsed;
            _menubtn.Visibility = Visibility.Visible;
           
        }

        private void ToMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/russ.mp3"));
            soundplayer.Play();
            _submitScore.Visibility = Visibility.Visible;
            _menubtn.Visibility = Visibility.Collapsed;
        }
    }
}
