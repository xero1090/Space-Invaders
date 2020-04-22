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
	/// <summary>
	/// Base class for all characters
	/// </summary>
	public abstract class CharInstance : Interactable
	{
		// Field Variables
		protected BitmapImage _sprite;

		// Properties
		public BitmapImage Sprite
		{ get { return _sprite; } set { _sprite = value; } }

		/// <summary>
		/// Initializer
		/// </summary>
		/// <param name="xStart"> Starting location X </param>
		/// <param name="yStart"> Starting location Y </param>
		public CharInstance(double xStart, double yStart): base(xStart, yStart)
		{
			_location = new location(xStart, yStart);
		}

		/// <summary>
		/// Moving an onject on the canvas and in the logic by modifying the current location
		/// </summary>
		/// <param name="xMod"> movement by modification of existing X </param>
		/// <param name="yMod"> movement by modification of existing Y </param>
		public void Move(double xMod, double yMod)
		{
			_location.X += xMod;
			_location.Y += yMod;

			_obj.SetValue(Canvas.LeftProperty, _location.X);
			_obj.SetValue(Canvas.TopProperty, _location.Y);
		}

		/// <summary>
		/// Moving an onject on the canvas by giving a new location
		/// </summary>
		/// <param name="xSet"> new location X </param>
		/// <param name="ySet"> new location Y </param>
		public void MoveTo(double xSet, double ySet)
		{
			_location.X = xSet;
			_location.Y = ySet;

			_obj.SetValue(Canvas.LeftProperty, _location.X);
			_obj.SetValue(Canvas.TopProperty, _location.Y);
		}

		/// <summary>
		/// When something dies (this is so sad)
		/// </summary>
		public virtual void OnDestruction()
		{
			//TODO: Specific crud when it dies
		}
	}
}
