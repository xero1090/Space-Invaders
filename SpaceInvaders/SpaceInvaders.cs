using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SpaceInvaders.Characters;
using SpaceInvaders.Entities;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders
{
    class SpaceInvaders
    {
        private List<CharInstance> _targets;
        private List<Enemy> _enemies;
        private List<Projectile> _bullets;
        private List<PowerUp> _powerUps;
        private PlayerTurret _player;
        private Canvas _canvas;

        public PlayerTurret Player
        { get { return _player; } }

        public SpaceInvaders(ref PlayerTurret playerTurret, Canvas canvas)
        {
            _targets = new List<CharInstance>();
            _enemies = new List<Enemy>();
            _bullets = new List<Projectile>();
            _powerUps = new List<PowerUp>();
            _player = playerTurret;
            _canvas = canvas;
        }

        public void BulletCheck()
        {
            //TODO: check bullets for contact with any instances or entities
        }

        public void PlayerMove(double xMod)
        {
            if (xMod <= 0)
            {
                if (_player.Location.X > 0)
                {
                    _player.Move(xMod, 0);
                }
            }
            else
            {
                if (_player.Location.X < (_canvas.ActualWidth - _player.Obj.Width))
                {
                    _player.Move(xMod, 0);
                }
            }
        }
    }
}
