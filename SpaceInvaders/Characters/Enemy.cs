using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
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

        public Enemy(double xStart, double yStart, ImageBrush enemySprite, Rectangle enemyCopy) : base(xStart, yStart)
		{
			base._sprite = enemySprite.ImageSource as BitmapImage;
			_obj.Width = enemyCopy.Width;
			_obj.Height = enemyCopy.Height;
			_obj.Fill = enemySprite;
		}

        public void EnemyShoot()
        {
        }

		public override void OnDestruction()
		{
			_obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
			_obj = null;
		}
        
	}
}
