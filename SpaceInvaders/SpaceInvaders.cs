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

        private const sbyte DEFAULT_MISSILE_SPEED = -20;
        private const sbyte DEFAULT_LASER_SPEED = 20;
        private const byte DEFAULT_ENEMY_COLUMNS = 10;
        private const double ENEMY_PLACEMENT_BUFFER = 8;
        
        private uint _score;
        private EnemyDirection _direction;
        private double _enemyWidth;
        private bool _goLeft;
        private bool _skip;

        private List<CharInstance> _targets;
        private List<Enemy> _enemies;
        private List<Projectile> _bullets;
        private List<PowerUp> _powerUps;
        private PlayerTurret _player;
        private Canvas _canvas;

        public PlayerTurret Player
        { get { return _player; } }

        public List<Enemy> Enemies
        { get { return _enemies; } }

        public uint Score
        { get { return _score; } }

        public SpaceInvaders(ref PlayerTurret playerTurret, Canvas canvas, double enemyWidth)
        {
            _targets = new List<CharInstance>();
            _enemies = new List<Enemy>();
            _bullets = new List<Projectile>();
            _powerUps = new List<PowerUp>();
            _player = playerTurret;
            _canvas = canvas;
            _direction = EnemyDirection.Right;
            _goLeft = false;
            _skip = false;
            _enemyWidth = enemyWidth;
            _score = 0;
        }

        public void EnemySetup( List<ImageBrush> shipSprites, Rectangle enemyCopy)
        {
            Enemy enemyHolder;
            double placementStart = _canvas.Width / 2 - ((DEFAULT_ENEMY_COLUMNS / 2)* (enemyCopy.Width + ENEMY_PLACEMENT_BUFFER));
            _goLeft = false;

            for (byte rows = 0; rows < shipSprites.Count; ++rows)
            {
                for (byte columns = 0; columns < DEFAULT_ENEMY_COLUMNS; ++columns)
                {
                     enemyHolder = (CreateEnemy(enemyCopy, placementStart + (columns * (enemyCopy.Width + ENEMY_PLACEMENT_BUFFER)), rows*(enemyCopy.Height + ENEMY_PLACEMENT_BUFFER), shipSprites[rows]));
                    _canvas.Children.Add(enemyHolder.Obj);
                    _enemies.Add(enemyHolder);
                }
            }
        }

        private Enemy CreateEnemy(Rectangle enemyCopy, double xStart, double yStart, ImageBrush sprite)
        {
            Rectangle projection = new Rectangle();
            projection.Width = enemyCopy.Width;
            projection.Height = enemyCopy.Height;
            projection.Fill = sprite;
            projection.Visibility = Windows.UI.Xaml.Visibility.Visible;
            projection.SetValue(Canvas.TopProperty, yStart);
            projection.SetValue(Canvas.LeftProperty, xStart);

            Enemy enemy = new Enemy(xStart, yStart, sprite.ImageSource as BitmapImage, projection);
            return enemy;
        }

        public void PlayerShoot(Rectangle missileCopy)
        {
            Projectile missile = CreateMissile(missileCopy, _player);
            _player.ShootMissile();
            missile.Move();
            _canvas.Children.Add(missile.Obj);
            _bullets.Add(missile);
        }

        /*
        public void EnemyShoot(Rectangle laserCopy)
        {
            Projectile laser = CreateMissile(laserCopy, _enemy);
            _enemy.EnemyShoot();
            laser.Move();
            _canvas.Children.Add(laser.Obj);
            _bullets.Add(laser);
        }*/

        private Projectile CreateMissile(Rectangle missileCopy, CharInstance shooter)
        {
            sbyte velocity = DEFAULT_LASER_SPEED;

            // copying the missile
            Rectangle projection = new Rectangle();
            projection.Width = missileCopy.Width;
            projection.Height = missileCopy.Height;
            projection.Fill = missileCopy.Fill;
            projection.SetValue(Canvas.TopProperty, (double)shooter.Obj.GetValue(Canvas.TopProperty) - projection.Height / 1.5);
            projection.SetValue(Canvas.LeftProperty, (double)shooter.Obj.GetValue(Canvas.LeftProperty) + (shooter.Obj.Width / 2 - projection.Width / 2));
            projection.Visibility = Windows.UI.Xaml.Visibility.Visible;

            if (shooter.GetType() == _player.GetType())
            {
                velocity = DEFAULT_MISSILE_SPEED;
            }

            // creating the projectile
            Projectile missile = new Projectile(shooter.Location.X, shooter.Location.Y, 0, velocity, projection);

            return missile;
        }

        public void BulletCheck(ImageBrush explosion)
        {
            bool hit = false;
            List<Projectile> active = new List<Projectile>();
            foreach (Projectile bullet in _bullets)
            {
                bullet.Move();

                if (bullet.State == MissileState.Intact)
                {
                    if (HitAlien(bullet))
                    {
                        hit = true;
                    }
                    else if (bullet.Location.Y <= 0)
                    {
                        bullet.MoveTo(0);
                        hit = true;
                    }

                    if (hit)
                    {
                        bullet.Hit(explosion);
                    }
                    active.Add(bullet);
                    hit = false;
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

        public void EnemyMove()
        {
            _direction = WhichDirection();

            switch (_direction)
            {
                case EnemyDirection.Right:
                    foreach (Enemy enemy in _enemies)
                    {
                        enemy.Move(enemy.Obj.Width + ENEMY_PLACEMENT_BUFFER, 0);
                    }
                    break;

                case EnemyDirection.Left:
                    foreach (Enemy enemy in _enemies)
                    {
                        enemy.Move(-(enemy.Obj.Width + ENEMY_PLACEMENT_BUFFER), 0);
                    }
                    break;

                case EnemyDirection.Down:
                    foreach (Enemy enemy in _enemies)
                    {
                        enemy.Move(0, enemy.Obj.Width + ENEMY_PLACEMENT_BUFFER);
                    }
                    break;
            }
        }

        private EnemyDirection WhichDirection()
        {
            double mostLeft = _canvas.Width;
            double mostRight = 0;
            double adjustWidth = _enemyWidth + ENEMY_PLACEMENT_BUFFER;

            if (!_skip)
            {
                foreach (Enemy enemy in _enemies)
                {
                    if (enemy.Location.X + enemy.Obj.Width > mostRight)
                    {
                        mostRight = (enemy.Location.X + enemy.Obj.Width);
                    }

                    if (enemy.Location.X < mostLeft)
                    {
                        mostLeft = enemy.Location.X;
                    }
                }
            }

            _skip = false;

            if (mostRight >= _canvas.Width - adjustWidth)
            {
                _goLeft = true;
                _skip = true;
                return EnemyDirection.Down;
            }
            else if (mostLeft <= _enemyWidth)
            {
                _goLeft = false;
                _skip = true;
                return EnemyDirection.Down;
            }

            if (_goLeft)
            {
                return EnemyDirection.Left;
            }

            return EnemyDirection.Right;

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

        private bool HitAlien(Projectile bullet)
        {
            for (int index = 0; index < _enemies.Count; ++index)
            {
                if (CollideCheck(bullet, _enemies[index]))
                {
                    _enemies[index].OnDestruction();
                    _enemies.RemoveAt(index); 
                    _score += 10;
                    if (_enemies.Count == 0)
                    {
                        _score = _score*2;
                    }
                    return true;
                }
            }
            return false;
        }
        private bool CollideCheck(Projectile projectile, CharInstance character)
        {
            bool yCollision = false;
            bool xCollision = false;

            // checking y axis
            if (projectile.Location.Y >= character.Location.Y && projectile.Location.Y <= character.Location.Y + character.Obj.Height) // Checking highest point
            {
                yCollision = true;
            }
            if (projectile.Location.Y + projectile.Obj.Height >= character.Location.Y && projectile.Location.Y + projectile.Obj.Height <= character.Location.Y + character.Obj.Height) // Checking lowest point
            {
                yCollision = true;
            }

            // checking x axis
            if (projectile.Location.X >= character.Location.X - character.Obj.Width && projectile.Location.X <= character.Location.X) // Checking left most point
            {
                xCollision = true;
            }
            if (projectile.Location.X + projectile.Obj.Width >= character.Location.X - character.Obj.Width && projectile.Location.X + projectile.Obj.Width <= character.Location.X) // Checking right most point
            {
                xCollision = true;
            }

            if (yCollision && xCollision)
            {
                return true;
            }

            return false;
        }

    }
}
