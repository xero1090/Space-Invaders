using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Entities
{
    /// <summary>
    /// Enum used to determine whether the missile is dead
    /// </summary>
    enum MissileState
    {
        Intact,
        Explode
    }

    /// <summary>
    /// Class used for projectiles
    /// </summary>
    class Projectile : Interactable
    {
        // Field variables
        private double[] _modifier;
        private MissileState _state;

        // Properties
        public MissileState State
        { get { return _state; } }

        /// <summary>
        /// Initializer for projectiles
        /// </summary>
        /// <param name="xStart"> Starting location X </param>
        /// <param name="yStart"> Starting location Y </param>
        /// <param name="xMod"> modification of its location X </param>
        /// <param name="yMod"> modification of its location Y </param>
        /// <param name="missileCopy"></param>
        public Projectile(double xStart, double yStart, double xMod, double yMod, Rectangle missileCopy) : base(xStart, yStart)
        {
            _modifier = new double[] { xMod, yMod };
            _state = MissileState.Intact;
            _obj.Width = missileCopy.Width;
            _obj.Height = missileCopy.Height;
            _obj.Fill = missileCopy.Fill;
        }

        /// <summary>
        /// Making the missile move in its direction
        /// </summary>
        public void Move()
        {
            _location.Y += _modifier[1];
            _obj.SetValue(Canvas.TopProperty, _location.Y);
        }

        /// <summary>
        /// make the missile move to a specific location
        /// </summary>
        /// <param name="newY"> exact Y location </param>
        public void MoveTo(double newY)
        {
            _location.Y = newY;
            _obj.SetValue(Canvas.TopProperty, _location.Y);
        }

        /// <summary>
        /// When the missile hits a target
        /// </summary>
        /// <param name="explosion"> explosion sprite that replaces the missile sprite</param>
        public void Hit(ImageBrush explosion)
        {
            _state = MissileState.Explode;
            _obj.Fill = explosion;
        }

        /// <summary>
        /// When a missile is destroyed
        /// </summary>
        public void Destroy()
        {
            _obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _obj = null;
        }

    }
}
