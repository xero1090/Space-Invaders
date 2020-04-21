using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Entities;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using SpaceInvaders;

namespace SpaceInvaders.Characters
{
    class PlayerTurret : CharInstance
    {
        const byte DEFAULT_LIVES = 3;
        private byte _lives;
        private bool _doubleShot;
        private bool _hasShield;
        private ImageBrush _spriteShoot;
        

        public byte Lives
        { get { return _lives; } set { _lives = value; } }

        public bool DoubleTap
        { get { return _doubleShot; } }

        public PlayerTurret(double xStart, double yStart, ImageBrush sprite, ImageBrush spriteShoot, Rectangle obj) : base (xStart, yStart)
        {
            base._sprite = sprite.ImageSource as BitmapImage;
            _spriteShoot = spriteShoot;
            _lives = DEFAULT_LIVES;
            _doubleShot = false;
            _hasShield = false;
            _obj = obj;
        }

        public void ShootMissile()
        {
            _obj.Fill = _spriteShoot;
        }
        

        public override void OnDestruction()
        {
            if (!_hasShield)
            {
                --_lives;
                //TODO: when you die

            }
            else
            {
                _hasShield = false;
            }
        }

        public void OnPowerUp(Effect type)
        {
            // Red buff: double missile
            // blue buff: shield
            // Yellow buff: extra life
            //TODO: implement buffs
            if (type == Effect.DoubleTap) // Double Missile
            {
                _doubleShot = true;
            }
            if (type == Effect.Shield) // Shield
            {
                _hasShield = true;
            }
            if (type == Effect.ExtraLife)
            {
                ++_lives;
            }

        }

    }
}
