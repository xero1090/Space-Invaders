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


namespace SpaceInvaders
{

    public sealed partial class Classic : Page
    { 
        // Declare our Windows Media Player
        MediaPlayer player;
        public Classic()
        {
            this.InitializeComponent();
            //create a new MediaPlayer obj
            player = new MediaPlayer();
            player.Pause();
            player.Source = null;
        }

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
            Windows.Storage.StorageFile file = await folder.GetFileAsync("Classic.mp3");

            //plays the song
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }
    }
}
