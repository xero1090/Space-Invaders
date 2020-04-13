using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Characters;

namespace SpaceInvaders.Entities
{
    class Projectile
    {
        private location _location;
        private int[] _modifier;
        private BitmapImage _sprite;

        public location Location
        { get { return _location; } }

        public BitmapImage Sprite
        { get { return _sprite; } set { _sprite = value; } }

        public Projectile(int xStart, int yStart, int xMod, int yMod)
        {
            _location = new location(xStart, yStart);
            _modifier = new int[] { xMod, yMod };
        }

        public void Move()
        {
            _location.X += _modifier[0];
            _location.Y += _modifier[1];
        }

        public void Hit()
        {
            //TODO: make something happens when it hits
        }

    }
}
