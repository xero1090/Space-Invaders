using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Entities
{

    enum Effect
    {
        ExtraLife = 1,
        DoubleTap,
        Shield,
        ExtraPoints
    }

    class PowerUp : Interactable
    {
        const byte DEFAULT_FALLING_SPEED = 20;

        private Effect _type;
        private BitmapImage _sprite;

        public Effect PowerUpType
        { get { return _type; } }

        public BitmapImage Sprite
        { get { return _sprite; } set { _sprite = value; } }

        public PowerUp(double xStart, double yStart, Rectangle obj, Effect type): base(xStart, yStart)
        {
            _type = type;
        }

        public PowerUp(double xStart, double yStart, Rectangle obj) : this(xStart, yStart, obj, Effect.ExtraPoints)
        {
        }

        public void Fall(int yMod = DEFAULT_FALLING_SPEED)
        {
            _location.Y += yMod;
        } 

        public void Contact()
        {
            // TODO: Do something when it connects with something
        }
    }
}
