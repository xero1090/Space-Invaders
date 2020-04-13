using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Entities;

namespace SpaceInvaders.Characters
{
    class PlayerTurret : CharInstance
    {
        private byte _lives;
        private bool _doubleShot;
        
        public byte Lives
        { get { return _lives; } set { _lives = value; } }

        public PlayerTurret(int xStart, int yStart, BitmapImage sprite) : base (xStart, yStart)
        {
            base._sprite = sprite;
        }

        public void ShootMissile()
        {
            //TODO: Bang Bang
        }

        public override void OnDestruction()
        {
            --_lives;
            //TODO: when you die
            base.OnDestruction();
        }

        public void OnPowerUp(Effect type)
        {
            //TODO: implement buffs
        }
    }
}
