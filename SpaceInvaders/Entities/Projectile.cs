using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Characters;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Entities
{
    class Projectile
    {
        private location _location;
        private double[] _modifier;
        private BitmapImage _sprite;
        protected Rectangle _obj;

        public location Location
        { get { return _location; } }

        public BitmapImage Sprite
        { get { return _sprite; } set { _sprite = value; } }

        public Rectangle Obj
        { get { return _obj; } }

        public Projectile(double xStart, double yStart, double xMod, double yMod, Rectangle obj)
        {
            _location = new location(xStart, yStart);
            _modifier = new double[] { xMod, yMod };
            _obj = obj;
        }

        public void Move()
        {
            _location.Y += _modifier[1];
            _obj.SetValue(Canvas.TopProperty, _location.Y);
        }

        public void Hit()
        {
            _obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _obj = null;
        }

    }
}
