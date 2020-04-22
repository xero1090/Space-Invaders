using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Characters
{
    /// <summary>
    /// Acts as the place the aliens get to to make the player lose a life
    /// </summary>
    class Barrier : CharInstance
    {
        /// <summary>
        /// Passes the already existing object
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="barrier"></param>
        public Barrier(double xStart, double yStart, ref Rectangle barrier): base(xStart, yStart)
        {
            _obj = barrier;
        }
    }
}
