using System;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;


namespace SpaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOver : Page
    {
        #region Field Variables
        private int _score;
        private string name;
        MediaPlayer soundplayer;
        SpaceInvaders score;
        #endregion

        #region Constructor
        public GameOver()
        {
            this.InitializeComponent();
            soundplayer = new MediaPlayer();
            soundplayer.Volume = 0.3;
            soundplayer.Pause();
            soundplayer.Source = null;
        }
        #endregion

        #region Methods
        /*private int getScore()
        {
           return score.Score;

        }*/

        private void SubmitScore(object sender, RoutedEventArgs e)
        {
            //getScore();
           // _score = score.Score;
            name = _name.Text;
            _scores.Text = name + " -------------- " + "800" ;//_score 
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
        #endregion
    }
}
