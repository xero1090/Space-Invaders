using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Entities
{
    /// <summary>
    /// Different Power Up Effects
    /// </summary>
    enum Effect
    {
        ExtraLife = 1,
        ExtraPoints
    }

    /// <summary>
    /// Power Up Class
    /// </summary>
    class PowerUp : Interactable
    {
        // ONLY EXTRALIVES DUE TO TIME CONSTRAINT / BUG

        // Constants
        const byte DEFAULT_FALLING_SPEED = 20;

        // Field Variables
        private Effect _type;

        // Properties
        public Effect PowerUpType
        { get { return _type; } }

        /// <summary>
        /// The Initializer for the power up class
        /// </summary>
        /// <param name="xStart"> Starting location X </param>
        /// <param name="yStart"> Starting location Y </param>
        /// <param name="sprite"> The PowerUp Icon Used </param>
        /// <param name="powerUpCopy"> The Rectangle the power up copies from </param>
        /// <param name="type"> The type of powerup </param>
        public PowerUp(double xStart, double yStart, ImageBrush sprite, Rectangle powerUpCopy, Effect type) : base(xStart, yStart)
        {
            _type = type;
            _obj.Fill = sprite;
            _obj.Width = powerUpCopy.Width;
            _obj.Height = powerUpCopy.Height;
        }

        /// <summary>
        /// Making the power up move
        /// </summary>
        /// <param name="yMod"> how fast it falls </param>
        public void Fall(double yMod = DEFAULT_FALLING_SPEED)
        {
            _location.Y += yMod;
            _obj.SetValue(Canvas.TopProperty, (double) _obj.GetValue(Canvas.TopProperty) + (double) yMod);
        } 

        /// <summary>
        /// When it comes in contact with something
        /// </summary>
        public void Contact()
        {
            _obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _obj = null;
        }
    }
}
