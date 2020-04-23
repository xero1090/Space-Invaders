using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders
{
    /// <summary>
    /// Custom struct used for location
    /// </summary>

    #region Struct
    public struct location
    {
        #region Field Variables
        private double _xLocation;
        private double _yLocation;
        #endregion

        #region Properties
        public double X
        { get { return _xLocation; } set { _xLocation = value; } }
        public double Y
        { get { return _yLocation; } set { _yLocation = value; } }
        #endregion

        #region Initializer
        public location(double xLoc, double yLoc)
        {
            _xLocation = xLoc;
            _yLocation = yLoc;
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// Base class everything is eventually generalizing
    /// </summary>
    public abstract class Interactable
    {
        #region Field Variables
        protected location _location;
        protected Rectangle _obj;
        #endregion

        #region Properties
        public location Location
        { get { return _location; } }

        public Rectangle Obj
        { get { return _obj; } }
        #endregion

        /// <summary>
        /// Creates the rectangle
        /// </summary>
        /// <param name="xStart"> Starting location X </param>
        /// <param name="yStart"> Starting location Y </param>

        #region Constructor
        public Interactable(double xStart, double yStart)
        {
            _location = new location(xStart, yStart);
            _obj = new Rectangle();
            _obj.Visibility = Windows.UI.Xaml.Visibility.Visible;
            _obj.SetValue(Canvas.TopProperty, yStart);
            _obj.SetValue(Canvas.LeftProperty, xStart);
        }
        #endregion

    }
}
