using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using SpaceInvaders;

namespace SpaceInvaders.Characters
{
	public abstract class CharInstance : Interactable
	{

		protected bool _isAlive;
		protected BitmapImage _sprite;

		public bool IsAlive
		{ get { return _isAlive; } set { _isAlive = value; } }

		public BitmapImage Sprite
		{ get { return _sprite; } set { _sprite = value; } }

		public CharInstance(double xStart, double yStart, Rectangle obj): base(xStart, yStart, obj)
		{
			_location = new location(xStart, yStart);
			_obj = obj;
			_isAlive = true;
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
