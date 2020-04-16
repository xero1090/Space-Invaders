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
using Windows.UI.Xaml.Media;

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

        public void PlayerShoot(Rectangle missileCopy)
        {
            Projectile missile = _player.ShootMissile(missileCopy);
            missile.Move();
            _canvas.Children.Add(missile.Obj);
            _bullets.Add(missile);
        }

        public void BulletCheck(ImageBrush explosion)
        {
            List<Projectile> active = new List<Projectile>();
            foreach (Projectile bullet in _bullets)
            {
                bullet.Move();

                if (bullet.State == MissileState.Intact)
                {
                    if (bullet.Location.Y <= 0)
                    {
                        bullet.MoveTo(0);
                        bullet.Hit(explosion);
                    }
                    active.Add(bullet);
                }
                else
                {
                    bullet.Destroy();
                }
            }

            _bullets.Clear();
            _bullets = active;
            active = null;
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
