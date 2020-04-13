using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Characters
{
    class Barrier : CharInstance
    {
        public Barrier(int xStart, int yStart, BitmapImage sprite): base(xStart, yStart)
        {
            base.Sprite = sprite;
        }

        public override void OnDestruction()
        {
            //TODO: Implement the destruction of the barrier
            base.OnDestruction();
        }

    }
}
