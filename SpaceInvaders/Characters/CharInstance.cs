using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Characters
{
	public struct location
	{
		private double _xLocation;
		private double _yLocation;

		public double X
		{ get { return _xLocation; } set { _xLocation = value; } }
		public double Y
		{ get { return _yLocation; } set { _yLocation = value; } }

		public location(double xLoc, double yLoc)
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
		protected Rectangle _obj;

		public location Location
		{ get { return _location; } }

		public bool IsAlive
		{ get { return _isAlive; } set { _isAlive = value; } }

		public BitmapImage Sprite
		{ get { return _sprite; } set { _sprite = value; } }

		public Rectangle Obj
		{ get { return _obj; } }

		public CharInstance(double xStart, double yStart, Rectangle obj)
		{
			_location = new location(xStart, yStart);
			_obj = obj;
			_isAlive = true;
		}

		public bool IsHit()
		{
			//TODO: figure this crud out
			return false;
		}

		public void Move(double xMod, double yMod)
		{
			_location.X += xMod;
			_location.Y += yMod;

			_obj.SetValue(Canvas.LeftProperty, _location.X);
			_obj.SetValue(Canvas.TopProperty, _location.Y);
		}

		public void MoveTo(double xSet, double ySet)
		{
			_location.X = xSet;
			_location.Y = ySet;

			_obj.SetValue(Canvas.LeftProperty, _location.X);
			_obj.SetValue(Canvas.TopProperty, _location.Y);
		}

		public virtual void OnDestruction()
		{
			//TODO: Specific crap when it dies
		}
	}
}
