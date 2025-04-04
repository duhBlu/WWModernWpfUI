﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ModernWpf.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;
using Flyout = ModernWpf.Controls.Flyout;

namespace MUXControlsTestApp
{

    [TopLevelTestPage(Name = "DropDownButton", Icon = "DropDownButton.png")]
    public sealed partial class DropDownButtonPage : TestPage
    {
        private int _clickCount = 0;
        private int _flyoutOpenedCount = 0;
        private int _flyoutClosedCount = 0;

        private Flyout _flyout;

        public DropDownButtonPage()
        {
            InitializeComponent();

            _flyout = new Flyout();
            _flyout.Placement = FlyoutPlacementMode.Bottom;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "New Flyout";
            _flyout.Content = textBlock;
            _flyout.Opened += TestDropDownButtonFlyout_Opened;
            _flyout.Closed += TestDropDownButtonFlyout_Closed;
        }

        private void TestDropDownButton_Click(object sender, object e)
        {
            ClickCountTextBlock.Text = (++_clickCount).ToString();
        }

        private void TestDropDownButtonFlyout_Opened(object sender, object e)
        {
            FlyoutOpenedCountTextBlock.Text = (++_flyoutOpenedCount).ToString();
        }

        private void TestDropDownButtonFlyout_Closed(object sender, object e)
        {
            FlyoutClosedCountTextBlock.Text = (++_flyoutClosedCount).ToString();
        }

        private void SetFlyoutCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (TestDropDownButton != null && _flyout != null)
            {
                TestDropDownButton.Flyout = _flyout;
            }
        }

        private void SetFlyoutCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TestDropDownButton != null)
            {
                TestDropDownButton.Flyout = null;
            }
        }
    }
}
