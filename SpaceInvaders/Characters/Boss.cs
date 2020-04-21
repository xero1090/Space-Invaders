using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Characters
{
    class Boss : CharInstance
    {
        public Boss(double xStart, double yStart, BitmapImage bossSprite, Rectangle obj) : base(xStart, yStart)
        {
            base._sprite = bossSprite;
        }
    }
}
