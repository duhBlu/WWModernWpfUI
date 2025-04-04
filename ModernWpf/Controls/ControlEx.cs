﻿using ModernWpf.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls
{
    public class ControlEx : Control
    {
        #region CornerRadius

        public static readonly DependencyProperty CornerRadiusProperty =
            ControlHelper.CornerRadiusProperty.AddOwner(typeof(ControlEx));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion
    }
}
