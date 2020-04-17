using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
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
        int enemyCount;
        MessageDialog Win = new MessageDialog("You have Saved Earth Comrade!");
        public Enemy(double xStart, double yStart, BitmapImage enemySprite, Rectangle obj) : base(xStart, yStart, obj)
		{
			base._sprite = enemySprite;
            enemyCount = 40;
		}

        public void EnemyShoot()
        {
        }

		public async override void OnDestruction()
		{
            //TODO: Specific crap when the enemy dies
            if (enemyCount == 0)
            {
                await Win.ShowAsync();
            }
		}
        
	}
}
