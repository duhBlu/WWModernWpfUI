﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Windows;

using NavigationViewItem = ModernWpf.Controls.NavigationViewItem;

namespace MUXControlsTestApp
{
    public sealed partial class NavigationViewTopNavOverflowButtonPage : TestPage
    {
        public NavigationViewTopNavOverflowButtonPage()
        {
            this.InitializeComponent();
        }

        private void AddItem4Button_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = new NavigationViewItem
            {
                Content = $"Menu Item 4",
            };

            this.NavView.MenuItems.Add(menuItem);
        }

        private void RemoveFirstItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavView.MenuItems.Count > 0)
            {
                NavView.MenuItems.RemoveAt(0);
            }
        }

        private void RemoveLastItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavView.MenuItems.Count > 0)
            {
                NavView.MenuItems.RemoveAt(NavView.MenuItems.Count - 1);
            }
        }
    }
}
