using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Characters
{
	public class Enemy : CharInstance
	{
		public Enemy(int xStart, int yStart, BitmapImage enemySprite) : base(xStart, yStart)
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
