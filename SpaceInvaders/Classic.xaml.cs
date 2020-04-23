using System;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;


namespace SpaceInvaders
{

    public sealed partial class Classic : Page
    {
        #region Field Variables
        // Declare our Windows Media Player
        private MediaPlayer player;
        #endregion

        #region Constructor
        public Classic()
        {
            this.InitializeComponent();
            //create a new MediaPlayer obj
            player = new MediaPlayer();
            player.Pause();
            player.Source = null;
        }
        #endregion

        #region Methods
        private void ToMenu(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MainMenu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });

        }

        private async void PlayClassic(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassicGame));
            // Searches within the Assets Folder
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            // Searches for specific file
            Windows.Storage.StorageFile file = await folder.GetFileAsync("begin.ogg");

            //plays the song
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }
        #endregion
    }
}
