using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Characters
{
    class Boss : CharInstance
    {
        private byte health = 20;
        private byte _hp;
        private Brush _spriteShoot;

        public byte hitPoints
        { get { return _hp; } set { _hp = value; } }
        public Boss(double xStart, double yStart, BitmapImage bossSprite, Rectangle obj) : base(xStart, yStart)
        {
            _hp = health;
            base._sprite = bossSprite;
        }
        public void ShootMissile()
        {
            _obj.Fill = _spriteShoot;
        }
        public void OnHit()
        {
            --_hp;
            //TODO: when you die


        }
    }
}
