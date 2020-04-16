using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Characters
{
	public class Enemy : CharInstance
	{
		public Enemy(double xStart, double yStart, BitmapImage enemySprite, Rectangle obj) : base(xStart, yStart, obj)
		{
			base._sprite = enemySprite;
		}

		public override void OnDestruction()
		{
			//TODO: Specific crap when the enemy dies
			base.OnDestruction();
		}
	}
}
