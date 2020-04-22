using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Characters
{
    class Barrier : CharInstance
    {
        public Barrier(double xStart, double yStart, ref Rectangle barrier): base(xStart, yStart)
        {
            _obj = barrier;
        }

        public override void OnDestruction()
        {
            //TODO: Implement the destruction of the barrier
        }

    }
}
