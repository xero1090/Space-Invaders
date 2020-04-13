using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Characters
{
    class Boss : CharInstance
    {
        public Boss(int xStart, int yStart, BitmapImage bossSprite) : base(xStart, yStart)
        {
            base._sprite = bossSprite;
        }
    }
}
