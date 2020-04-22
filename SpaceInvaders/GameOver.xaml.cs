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


        public GameOver()
        {
            this.InitializeComponent();
            
        }

        private void SubmitScore(object sender, RoutedEventArgs e)
        {
            name = _name.Text;
            _scores.Text = name + " -------------- " + "800";
            _submitScore.Visibility = Visibility.Collapsed;
            _menubtn.Visibility = Visibility.Visible;
           
        }

        private void ToMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
            _submitScore.Visibility = Visibility.Visible;
            _menubtn.Visibility = Visibility.Collapsed;
        }
    }
}
