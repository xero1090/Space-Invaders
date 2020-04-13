using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Characters;
using SpaceInvaders.Entities;

namespace SpaceInvaders
{
    class SpaceInvaders
    {
        private List<CharInstance> _targets;
        private List<Enemy> _enemies;
        private List<Projectile> _bullets;
        private List<PowerUp> powerUps;
        private PlayerTurret _player;
        private BitmapImage _background;

        public SpaceInvaders()
        {

        }

        public void BulletCheck()
        {
            //TODO: check bullets for contact with any instances or entities
        }
    }
}
