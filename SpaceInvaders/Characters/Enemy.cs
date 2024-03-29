﻿using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Characters
{
    /// <summary>
    /// Used to determine where the enemies go next
    /// </summary>

    #region Enum
    public enum EnemyDirection
	{
		Left = -1,
		Down,
		Right
	}
    #endregion

    /// <summary>
    /// The class from which enemies are created
    /// </summary>
  
    public class Enemy : CharInstance
	{
        /// <summary>
        /// Initializer for the enemy class
        /// </summary>
        /// <param name="xStart"> Starting location X </param>
        /// <param name="yStart"> Starting location Y </param>
        /// <param name="enemySprite"> Specific enemy sprite for this enemy </param>
        /// <param name="enemyCopy"> The rectangle it will be copying </param>

        #region Constructor
        public Enemy(double xStart, double yStart, ImageBrush enemySprite, Rectangle enemyCopy) : base(xStart, yStart)
		{
			base._sprite = enemySprite.ImageSource as BitmapImage;
			_obj.Width = enemyCopy.Width;
			_obj.Height = enemyCopy.Height;
			_obj.Fill = enemySprite;
		}
        #endregion

        /// <summary>
        /// When the enemy dies (This is so sad)
        /// </summary>

        #region Override Method
        public override void OnDestruction()
		{
			_obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
			_obj = null;
		}
        #endregion
    }
}
