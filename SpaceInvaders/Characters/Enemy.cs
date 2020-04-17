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
        int rows;
        int skins;
        const byte HP =  1;
        private byte _health;
		public Enemy(double xStart, double yStart, BitmapImage enemySprite, Rectangle obj) : base(xStart, yStart, obj)
		{
			base._sprite = enemySprite;
            _health = HP;
		}

        public void EnemyShoot()
        {
			_obj.Fill = _enemyShoot;
        }

		public override void OnDestruction()
		{
			//TODO: Specific crap when the enemy dies
		}
        public void CreateEnemies()
        {
            for (skins = 0; skins >= 4; skins++)
            {
                for (rows = 0; rows >= 4; rows++)
                {

                }
            }
        }
	}
}
