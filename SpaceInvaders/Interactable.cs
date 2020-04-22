using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders
{
    /// <summary>
    /// Custom struct used for location
    /// </summary>
    public struct location
    {
        // Field Variables
        private double _xLocation;
        private double _yLocation;

        // Properties
        public double X
        { get { return _xLocation; } set { _xLocation = value; } }
        public double Y
        { get { return _yLocation; } set { _yLocation = value; } }
        
        // Initializer
        public location(double xLoc, double yLoc)
        {
            _xLocation = xLoc;
            _yLocation = yLoc;
        }
    }

    /// <summary>
    /// Base class everything is eventually generalizing
    /// </summary>
    public abstract class Interactable
    {
        // Field Variables
        protected location _location;
        protected Rectangle _obj;

        // Properties
        public location Location
        { get { return _location; } }

        public Rectangle Obj
        { get { return _obj; } }

        /// <summary>
        /// Creates the rectangle
        /// </summary>
        /// <param name="xStart"> Starting location X </param>
        /// <param name="yStart"> Starting location Y </param>
        public Interactable(double xStart, double yStart)
        {
            _location = new location(xStart, yStart);
            _obj = new Rectangle();
            _obj.Visibility = Windows.UI.Xaml.Visibility.Visible;
            _obj.SetValue(Canvas.TopProperty, yStart);
            _obj.SetValue(Canvas.LeftProperty, xStart);
        }

    }
}
