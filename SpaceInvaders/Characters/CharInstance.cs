using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;


namespace SpaceInvaders
{
	public struct location
	{
		private int _xLocation;
		private int _yLocation;

		public int X
		{ get { return _xLocation; } set { _xLocation = value; } }
		public int Y
		{ get { return _yLocation; } set { _yLocation = value; } }

		public location(int xLoc, int yLoc)
		{
			_xLocation = xLoc;
			_yLocation = yLoc;
		}
	}

	public abstract class CharInstance
	{

		protected location _location;
		protected bool _isAlive;
		protected BitmapImage _sprite;

		public location Location
		{ get { return _location; } }

		public bool IsAlive
		{ get { return _isAlive; } set { _isAlive = value; } }

		public BitmapImage Sprite
		{ get { return _sprite; } set { _sprite = value; } }

		public CharInstance(int xStart, int yStart)
		{
			_location = new location(xStart, yStart);
			_isAlive = true;
		}

		public bool IsHit()
		{
			//TODO: figure this crud out
		}

		public void Move(int xMod, int yMod)
		{
			_location.X += xMod;
			_location.Y += yMod;
		}

		public void MoveTo(int xSet, int ySet)
		{
			_location.X = xSet;
			_location.Y = ySet;
		}

		public virtual void OnDestruction()
		{
			//TODO: Specific crap when you die
		}
	}
}
