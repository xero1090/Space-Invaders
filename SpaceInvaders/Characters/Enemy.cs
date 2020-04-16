using SpaceInvaders.Entities;
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
	public class Enemy : CharInstance
	{
        const byte HP =  1;
        private ImageBrush _enemyShoot;
        private byte _health;
		public Enemy(double xStart, double yStart, BitmapImage enemySprite, ImageBrush enemyShoot, Rectangle obj) : base(xStart, yStart, obj)
		{
			base._sprite = enemySprite;
            _enemyShoot = enemyShoot;
            _health = HP;
		}
        /* public Projectile EnemyLaser(Rectangle lasers)
        {
            _obj.Fill = _enemyShoot
            return
        } */
		public override void OnDestruction()
		{
			//TODO: Specific crap when the enemy dies
			base.OnDestruction();
		}
	}
}
