using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Entities;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using SpaceInvaders;

namespace SpaceInvaders.Characters
{
    class PlayerTurret : CharInstance
    {
        const byte DEFAULT_LIVES = 3;
        private byte _lives;
        private bool _doubleShot;
        private ImageBrush _spriteShoot;
        

        public byte Lives
        { get { return _lives; } set { _lives = value; } }

        public PlayerTurret(double xStart, double yStart, ImageBrush sprite, ImageBrush spriteShoot, Rectangle obj) : base (xStart, yStart, obj)
        {
            base._sprite = sprite.ImageSource as BitmapImage;
            _spriteShoot = spriteShoot;
            _lives = DEFAULT_LIVES;
            _doubleShot = false;
        }

        public void ShootMissile()
        {
            _obj.Fill = _spriteShoot;
        }
        

        public override void OnDestruction()
        {
            --_lives;
            //TODO: when you die
            

        }

        public void OnPowerUp(Effect type)
        {
            //Red buff: double missile
            //Yellow buff: Stop enemy movement
            // blue buff: shield
            //TODO: implement buffs
            /*
              if (redBuff == True)
                {
                    idk how to add a second missile beside the normal one
                }
              if (blueBuff == True)
                {
                    _shield.visibility = 0;
                    //idk how to keep shield permanently on tank 
                }
             if (yellowBuff == True)
                {
                    enemies.move == false;
                }
            */
        }

    }
}
