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
    /// <summary>
    /// Player Class
    /// </summary>
    class PlayerTurret : CharInstance
    {
        // Constants
        const sbyte DEFAULT_LIVES = 3;
        
        // Field Variables
        private sbyte _lives;
        private ImageBrush _spriteShoot;

        // Properties
        public sbyte Lives
        { get { return _lives; } set { _lives = value; } }

        /// <summary>
        /// Initializer for the PlayerTurret
        /// </summary>
        /// <param name="xStart"> Starting location X </param>
        /// <param name="yStart"> Starting location Y </param>
        /// <param name="sprite"> Base tank sprite </param>
        /// <param name="spriteShoot"> sprite for when the tank shoots </param>
        /// <param name="obj"> the obj the player is assaigned </param>
        public PlayerTurret(double xStart, double yStart, ImageBrush sprite, ImageBrush spriteShoot, Rectangle obj) : base (xStart, yStart)
        {
            base._sprite = sprite.ImageSource as BitmapImage;
            _spriteShoot = spriteShoot;
            _lives = DEFAULT_LIVES;
            _obj = obj;
        }

        // When the player shoots
        public void ShootMissile()
        {
            _obj.Fill = _spriteShoot;
        }
    }
}
