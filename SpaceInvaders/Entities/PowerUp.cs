﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace SpaceInvaders.Entities
{

    enum Effect
    {
        ExtraLife = 1,
        ExtraPoints
    }

    class PowerUp : Interactable
    {
        const byte DEFAULT_FALLING_SPEED = 20;

        private Effect _type;
        private BitmapImage _sprite;

        public Effect PowerUpType
        { get { return _type; } }

        public BitmapImage Sprite
        { get { return _sprite; } set { _sprite = value; } }

        public PowerUp(double xStart, double yStart, ImageBrush sprite, Rectangle powerUpCopy, Effect type) : base(xStart, yStart)
        {
            _type = type;
            _obj.Fill = sprite;
            _obj.Width = powerUpCopy.Width;
            _obj.Height = powerUpCopy.Height;
        }

        public PowerUp(double xStart, double yStart, ImageBrush sprite, Rectangle powerUpCopy) : this(xStart, yStart, sprite, powerUpCopy, Effect.ExtraPoints)
        {
        }

        public void Fall(double yMod = DEFAULT_FALLING_SPEED)
        {
            _location.Y += yMod;
            _obj.SetValue(Canvas.TopProperty, (double) _obj.GetValue(Canvas.TopProperty) + (double) yMod);
        } 

        public void Contact()
        {
            _obj.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _obj = null;
        }
    }
}
