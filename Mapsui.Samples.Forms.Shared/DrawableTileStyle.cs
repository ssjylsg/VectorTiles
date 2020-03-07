﻿using Mapsui.Styles;

namespace Mapsui.Samples.Forms
{
    public class DrawableTileStyle : IStyle
    {
        public double MinVisible { get; set; } = 0;
        public double MaxVisible { get; set; } = double.MaxValue;
        public bool Enabled { get; set; } = true;
        public float Opacity { get; set; } = 1.0f;
    }
}