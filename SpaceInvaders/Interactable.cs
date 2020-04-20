using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders
{
    public struct location
    {
        private double _xLocation;
        private double _yLocation;

        public double X
        { get { return _xLocation; } set { _xLocation = value; } }
        public double Y
        { get { return _yLocation; } set { _yLocation = value; } }

        public location(double xLoc, double yLoc)
        {
            _xLocation = xLoc;
            _yLocation = yLoc;
        }
    }

    public abstract class Interactable
    {

        protected location _location;
        protected Rectangle _obj;

        public location Location
        { get { return _location; } }

        public Rectangle Obj
        { get { return _obj; } }

        public Interactable(double xStart, double yStart, Rectangle obj)
        {
            _location = new location(xStart, yStart);
            _obj = obj;
        }

    }
}
