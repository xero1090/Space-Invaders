using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
	public class Enemy : CharInstance
	{
		public Enemy(int xStart, int yStart, BitmapImage enemySprite) : base(xStart, yStart)
		{

			base._sprite = enemySprite;

		}
	}
}
