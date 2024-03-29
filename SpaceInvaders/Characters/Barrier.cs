﻿using Windows.UI.Xaml.Shapes;

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

        #region Constructor
        public Barrier(double xStart, double yStart, ref Rectangle barrier) : base(xStart, yStart)
        {
            _obj = barrier;
        }
        #endregion
    }
}
