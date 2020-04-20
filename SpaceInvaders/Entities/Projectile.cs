using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SpaceInvaders.Entities
{
    enum MissileState
    {
        Intact,
        Explode
    }

    class Projectile : Interactable
    {
        private double[] _modifier;
        private BitmapImage _sprite;
        private MissileState _state;

        public BitmapImage Sprite
        { get { return _sprite; } set { _sprite = value; } }

        public MissileState State
        { get { return _state; } }

        public Projectile(double xStart, double yStart, double xMod, double yMod, Rectangle obj): base (xStart, yStart, obj)
        {
            _modifier = new double[] { xMod, yMod };
            _state = MissileState.Intact;
        }

        public void Move()
        {
            _location.Y += _modifier[1];
            _obj.SetValue(Canvas.TopProperty, _location.Y);
        }
        public void MoveTo(double newY)
        {
            _location.Y = newY;
            _obj.SetValue(Canvas.TopProperty, _location.Y);
        }

        public void Hit(ImageBrush explosion)
        {
            _state = MissileState.Explode;
            _obj.Fill = explosion;
        }

        public void Destroy()
        {
            _obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _obj = null;
        }

    }
}
