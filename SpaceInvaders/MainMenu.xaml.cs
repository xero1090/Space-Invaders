using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
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



namespace SpaceInvaders
{
 
    public sealed partial class MainMenu : Page
    {
        MediaPlayer soundplayer;


        public MainMenu()
        {
            this.InitializeComponent();
            _title.FontFamily = new FontFamily("/Assets/Fonts/KenneyThick.ttf#Kenney Thick Regular");
            soundplayer = new MediaPlayer();
            soundplayer.Pause();
            soundplayer.Source = null;
        }

        private void ToClassic(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Classic), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
          
        }

        private void ToGame(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassicGame), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/begin.ogg"));
            soundplayer.Play();

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();

        }

        
    }
}
