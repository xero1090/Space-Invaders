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
        private ImageBrush _spriteShoot;
        

        public byte Lives
        { get { return _lives; } set { _lives = value; } }

        public PlayerTurret(double xStart, double yStart, ImageBrush sprite, ImageBrush spriteShoot, Rectangle obj) : base (xStart, yStart)
        {
            base._sprite = sprite.ImageSource as BitmapImage;
            _spriteShoot = spriteShoot;
            _lives = DEFAULT_LIVES;
            _obj = obj;
        }

        public void ShootMissile()
        {
            _obj.Fill = _spriteShoot;
        }
    }
}
