using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Characters;

namespace SpaceInvaders.Entities
{

    enum Effect
    {
        ExtraLife = 1,
        DoubleTap,
        Shield,
        ExtraPoints
    }

    class PowerUp
    {
        private location _location;
        private Effect _type;
        private BitmapImage _sprite;

        public Effect PowerUpType
        { get { return _type; } }

        public location Location
        { get { return _location; } }

        public BitmapImage Sprite
        { get { return _sprite; } set { _sprite = value; } }

        public PowerUp(double xStart, double yStart, Effect type)
        {
            _location = new location(xStart, yStart);
            _type = type;
        }

        public PowerUp(double xStart, double yStart) : this(xStart, yStart, Effect.ExtraPoints)
        {
        }

        public void Fall(int yMod)
        {
            _location.Y += yMod;
        } 

        public void Contact()
        {
            // TODO: Do something when it connects with something
        }
    }
}
