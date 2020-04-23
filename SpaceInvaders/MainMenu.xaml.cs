using System;
using Windows.ApplicationModel.Core;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;



namespace SpaceInvaders
{

    public sealed partial class MainMenu : Page
    {
        #region Object
        MediaPlayer soundplayer;
        #endregion

        #region Constructor
        public MainMenu()
        {
            this.InitializeComponent();
            _title.FontFamily = new FontFamily("/Assets/Fonts/KenneyThick.ttf#Kenney Thick Regular");
            soundplayer = new MediaPlayer();
            soundplayer.Volume = 0.3;
            soundplayer.Pause();
            soundplayer.Source = null;
        }
        #endregion

        #region Methods
        private void ToClassic(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Classic), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/it-krimah.mp3"));
            soundplayer.Play();
        }

        private void ToGame(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassicGame), e, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            soundplayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/begin.ogg"));
            soundplayer.Play();

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();

        }
        #endregion

    }
}
