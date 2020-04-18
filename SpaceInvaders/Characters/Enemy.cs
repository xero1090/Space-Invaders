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
	public enum EnemyDirection
	{
		Left = -1,
		Down,
		Right
	}

	public class Enemy : CharInstance
	{
        private ImageBrush _enemyShoot;

        public Enemy(double xStart, double yStart, BitmapImage enemySprite, ImageBrush enemyShoot, Rectangle obj) : base(xStart, yStart, obj)
		{
            _enemyShoot = enemyShoot;
			base._sprite = enemySprite;
		}

        public void EnemyShoot()
        {
                _obj.Fill = _enemyShoot;
        }

		public override void OnDestruction()
		{
			_obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
			_obj = null;
		}
        
	}
}
