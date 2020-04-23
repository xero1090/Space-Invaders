using SpaceInvaders.Characters;
using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders
{
    /// <summary>
    /// The class meant to run most of the game
    /// </summary>
    class SpaceInvaders
    {
        #region Constants
        // All of the Constants
        private const sbyte DEFAULT_MISSILE_SPEED = -20;
        private const byte DEFAULT_ENEMY_COLUMNS = 10;
        private const double ENEMY_PLACEMENT_BUFFER = 8;
        private const double POWERUP_DROP_CHANCE = 0.01; // Chance every enemy drops a powerUp
        private const byte SCORE_BONUS = 50;
        private const byte SCORE_CHANGE = 10;
        #endregion

        #region Field Variables
        // The Field Variables
        private int _score;
        private location _lastKill;
        private EnemyDirection _direction;
        private double _enemyWidth;
        private bool _goLeft;
        private bool _skip;
        private Random _rand;
        private List<Enemy> _enemies;
        private List<Projectile> _bullets;
        private List<PowerUp> _powerUps;
        private PlayerTurret _player;
        private Canvas _canvas;
        private Barrier _barrier;
        #endregion

        #region Properties
        // Properties
        public PlayerTurret Player
        { get { return _player; } }

        public List<Enemy> Enemies
        { get { return _enemies; } }

        public int Score
        { get { return _score; } }

        public location LastKill
        { get { return _lastKill; } }
        #endregion

        /// <summary>
        /// Initializer for the class
        /// </summary>
        /// <param name="playerTurret"> Passing the premade turret to a playerturret field variable </param>
        /// <param name="canvas"> The canvas the game takes place on</param>
        /// <param name="enemyWidth"> Simply saving the universal enemy width </param>
        /// <param name="barrier"> The barrier that is used for detection of a loss condition </param>

        #region Constructor
        public SpaceInvaders(ref PlayerTurret playerTurret, Canvas canvas, double enemyWidth, ref Rectangle barrier)
        {
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
            _rand = new Random();
            _barrier = new Barrier(0, _canvas.ActualHeight - barrier.ActualHeight, ref barrier);
        }
        #endregion

        /// <summary>
        /// Sets up the enemies and their config for a new round
        /// </summary>
        /// <param name="shipSprites"> list of their sprites </param>
        /// <param name="enemyCopy"> the base invisible copy from which the new rectangles copy from </param>

        #region Methods
        public void EnemySetup(List<ImageBrush> shipSprites, Rectangle enemyCopy)
        {
            Enemy enemyHolder;
            double placementStart = _canvas.Width / 2 - ((DEFAULT_ENEMY_COLUMNS / 2) * (enemyCopy.Width + ENEMY_PLACEMENT_BUFFER));
            _goLeft = false;

            for (byte rows = 0; rows < shipSprites.Count; ++rows)
            {
                for (byte columns = 0; columns < DEFAULT_ENEMY_COLUMNS; ++columns)
                {
                    enemyHolder = new Enemy(placementStart + (columns * (enemyCopy.Width + ENEMY_PLACEMENT_BUFFER)), rows * (enemyCopy.Height + ENEMY_PLACEMENT_BUFFER), shipSprites[rows], enemyCopy);
                    _canvas.Children.Add(enemyHolder.Obj);
                    _enemies.Add(enemyHolder);
                }
            }
        }

        /// <summary>
        /// When the game Ends
        /// </summary>
        public void End()
        {
            for (int index = 0; index < _enemies.Count; ++index)
            {
                _enemies[index].OnDestruction();
            }

            _enemies.Clear();
        }

        /// <summary>
        /// Creating missiles when the player shoots
        /// </summary>
        /// <param name="missileCopy"> the base invisible copy from which the new rectangles copy from </param>
        public void PlayerShoot(Rectangle missileCopy)
        {
            Projectile missile = CreateMissile(missileCopy, _player);
            _player.ShootMissile();
            missile.Move();
            _canvas.Children.Add(missile.Obj);
            _bullets.Add(missile);
        }

        /// <summary>
        /// Chance of dropping a power up at the most recent kill
        /// </summary>
        /// <param name="sprites"> The different sprites for the powerups </param>
        /// <param name="powerUpCopy"> the base invisible copy from which the new rectangles copy from </param>
        private void CreatePowerUP(List<ImageBrush> sprites, Rectangle powerUpCopy)
        {
            if (_rand.NextDouble() <= POWERUP_DROP_CHANCE)
            {
                Effect type = (Effect)_rand.Next(1, sprites.Count);
                PowerUp powerUp = new PowerUp(_lastKill.X, _lastKill.Y, sprites[(int)type], powerUpCopy, type);
                _powerUps.Add(powerUp);
                _canvas.Children.Add(powerUp.Obj);
            }
        }

        /// <summary>
        /// Creating Missiles
        /// </summary>
        /// <param name="missileCopy"> the base invisible copy from which the new rectangles copy from </param>
        /// <param name="shooter"> The shooter of the missiles </param>
        /// <returns></returns>
        private Projectile CreateMissile(Rectangle missileCopy, CharInstance shooter)
        {
            sbyte velocity = DEFAULT_MISSILE_SPEED;

            // creating the projectile
            Projectile missile = new Projectile(shooter.Location.X + shooter.Obj.Width / 2, shooter.Location.Y, 0, velocity, missileCopy);
            missile.Obj.SetValue(Canvas.TopProperty, (double)shooter.Obj.GetValue(Canvas.TopProperty) - missile.Obj.Height / 1.5);
            missile.Obj.SetValue(Canvas.LeftProperty, (double)shooter.Obj.GetValue(Canvas.LeftProperty) + (shooter.Obj.Width / 2 - missile.Obj.Width / 2));

            return missile;
        }

        /// <summary>
        /// Checking the status of each bullet and clling a create powerup
        /// </summary>
        /// <param name="explosion"> explosion sprite </param>
        /// <param name="powerUps"> sprites for powerups </param>
        /// <param name="powerUpCopy"> the base invisible copy from which the new rectangles copy from </param>
        public void BulletCheck(ImageBrush explosion, List<ImageBrush> powerUps, Rectangle powerUpCopy)
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
                        CreatePowerUP(powerUps, powerUpCopy);
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

        /// <summary>
        /// Moving enemies based off of which enemies currently exist, and where they are headed
        /// </summary>
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

        /// <summary>
        /// Making powerups fall
        /// </summary>
        public void PowerUpMove()
        {
            for (int index = 0; index < _powerUps.Count; ++index)
            {
                _powerUps[index].Fall();
                if (_powerUps[index].Location.Y >= _canvas.Height)
                {
                    _powerUps[index].Contact();
                    _powerUps.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Finding the direction which the enemies should face
        /// </summary>
        /// <returns> direction where the enemies are headed </returns>
        private EnemyDirection WhichDirection()
        {
            double mostLeft = _canvas.Width;
            double mostRight = 0;
            double adjustWidth = _enemyWidth + ENEMY_PLACEMENT_BUFFER;

            //Finding a new direction
            if (!_skip) // Making sure it doesnt check after they move down
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

        /// <summary>
        /// Making sure the player moves acording to boundries
        /// </summary>
        /// <param name="xMod"> How much the player moves by </param>
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

        /// <summary>
        /// Checking if the aliens get hit by a projectile
        /// </summary>
        /// <param name="bullet"> the bullet the check is being performed on </param>
        /// <returns></returns>
        private bool HitAlien(Projectile bullet)
        {
            for (int index = 0; index < _enemies.Count; ++index)
            {
                if (CollideCheck(bullet, _enemies[index]))
                {
                    _enemies[index].OnDestruction();
                    _lastKill.X = _enemies[index].Location.X;
                    _lastKill.Y = _enemies[index].Location.Y;
                    _enemies.RemoveAt(index);
                    _score += SCORE_CHANGE;
                    if (_enemies.Count == 0)
                    {
                        _score = _score * 2;
                    }
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Checking if a powerup is grabbed and making a change if it is
        /// </summary>
        /// <returns> if a power up was gotton </returns>
        public bool PowerUpGet()
        {
            for (int index = 0; index < _powerUps.Count; ++index)
            {
                if (CollideCheck(_powerUps[index], _player))
                {
                    if (_powerUps[index].PowerUpType == Effect.ExtraLife)
                    {
                        ++_player.Lives;
                    }
                    else
                    {
                        _score += SCORE_BONUS;
                    }

                    _powerUps[index].Contact();
                    _powerUps.RemoveAt(index);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Called when the player loses
        /// </summary>
        public void AlienWin()
        {
            for (int index = 0; index < _enemies.Count; ++index)
            {
                if (CollideCheck(_enemies[index], _barrier))
                {
                    _enemies[index].OnDestruction();
                    _enemies.RemoveAt(index);
                    _score -= SCORE_CHANGE;
                    --_player.Lives;
                }
            }

        }

        /// <summary>
        /// Generic collision checking for various objects derived from Interactables
        /// </summary>
        /// <param name="projectile"> object to be checked against another </param>
        /// <param name="character"> object being checked by another </param>
        /// <returns></returns>
        private bool CollideCheck(Interactable projectile, Interactable character)
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
            if (projectile.Location.X >= character.Location.X && projectile.Location.X <= character.Location.X + character.Obj.Width) // Checking left most point
            {
                xCollision = true;
            }
            if (projectile.Location.X + projectile.Obj.Width >= character.Location.X && projectile.Location.X + projectile.Obj.Width <= character.Location.X + character.Obj.Width) // Checking right most point
            {
                xCollision = true;
            }

            if (yCollision && xCollision)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
