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
        const sbyte DEFAULT_MISSILE_SPEED = -20;
        private byte _lives;
        private bool _doubleShot;
        private ImageBrush _spriteShoot;
        
        public byte Lives
        { get { return _lives; } set { _lives = value; } }

        public PlayerTurret(double xStart, double yStart, ImageBrush sprite, ImageBrush spriteShoot, Rectangle obj) : base (xStart, yStart, obj)
        {
            base._sprite = sprite.ImageSource as BitmapImage;
            _spriteShoot = spriteShoot;
            _lives = DEFAULT_LIVES;
            _doubleShot = false;
        }

        public Projectile ShootMissile(Rectangle missileCopy)
        {
            _obj.Fill = _spriteShoot;
            return CreateMissile(missileCopy);
        }

        public override void OnDestruction()
        {
            --_lives;
            //TODO: when you die
            base.OnDestruction();
        }

        public void OnPowerUp(Effect type)
        {
            //TODO: implement buffs
        }

        private Projectile CreateMissile(Rectangle missileCopy)
        {
            // copying the missile
            Rectangle projection = new Rectangle();
            projection.Width = missileCopy.Width;
            projection.Height = missileCopy.Height;
            projection.Fill = missileCopy.Fill;
            projection.SetValue(Canvas.TopProperty, (double) _obj.GetValue(Canvas.TopProperty) - projection.Height/1.5);
            projection.SetValue(Canvas.LeftProperty, (double) _obj.GetValue(Canvas.LeftProperty) + (_obj.Width/2 - projection.Width/2));
            projection.Visibility = Windows.UI.Xaml.Visibility.Visible;

            // creating the projectile
            Projectile missile = new Projectile(_location.X, _location.Y, 0, DEFAULT_MISSILE_SPEED, projection);

            return missile;
        }
    }
}
