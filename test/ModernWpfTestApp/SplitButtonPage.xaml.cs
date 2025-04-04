﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Flyout = ModernWpf.Controls.Flyout;

namespace MUXControlsTestApp
{
    [TopLevelTestPage(Name = "SplitButton", Icon = "SplitButton.png")]
    public sealed partial class SplitButtonPage : TestPage
    {
        private int _clickCount = 0;
        private int _flyoutOpenedCount = 0;
        private int _flyoutClosedCount = 0;

        public MyCommand TestExecuteCommand { get; }
        private int _commandExecuteCount = 0;

        private Flyout _placementFlyout;

        public SplitButtonPage()
        {
            this.InitializeComponent();

            //SplitButtonTestApi.SimulateTouch = false;

            TestExecuteCommand = new MyCommand(this);

            _placementFlyout = new Flyout();
            _placementFlyout.Placement = FlyoutPlacementMode.Bottom;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Placement Flyout";
            _placementFlyout.Content = textBlock;

            //ControlStateViewer.ControlType = TestSplitButton.GetType();
            //ControlStateViewer.States = new List<string>
            //{
            //    "Normal",
            //    "FlyoutOpen",
            //    "TouchPressed",
            //    "PrimaryPointerOver",
            //    "PrimaryPressed",
            //    "SecondaryPointerOver",
            //    "SecondaryPressed",
            //};

            //ToggleControlStateViewer.ControlType = TestSplitButton.GetType();
            //ToggleControlStateViewer.States = new List<string>
            //{
            //    "Checked",
            //    "CheckedFlyoutOpen",
            //    "CheckedTouchPressed",
            //    "CheckedPrimaryPointerOver",
            //    "CheckedPrimaryPressed",
            //    "CheckedSecondaryPointerOver",
            //    "CheckedSecondaryPressed",
            //};

            DataContext = this;
        }

        private void TestSplitButton_Click(object sender, object e)
        {
            ClickCountTextBlock.Text = (++_clickCount).ToString();
        }

        private void TestSplitButtonFlyout_Opened(object sender, object e)
        {
            FlyoutOpenedCountTextBlock.Text = (++_flyoutOpenedCount).ToString();
        }

        private void TestSplitButtonFlyout_Closed(object sender, object e)
        {
            FlyoutClosedCountTextBlock.Text = (++_flyoutClosedCount).ToString();
        }

        private void SimulateTouchCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //SplitButtonTestApi.SimulateTouch = true;
        }

        private void SimulateTouchCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //SplitButtonTestApi.SimulateTouch = false;
        }

        private void EnableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DisabledSplitButton.IsEnabled = true;
        }

        private void EnableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DisabledSplitButton.IsEnabled = false;
        }

        private void CanExecuteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (TestExecuteCommand != null)
            {
                TestExecuteCommand.UpdateCanExecute(true);
            }
        }

        private void CanExecuteCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TestExecuteCommand != null)
            {
                TestExecuteCommand.UpdateCanExecute(false);
            }
        }

        public void CommandExecute()
        {
            ExecuteCountTextBlock.Text = (++_commandExecuteCount).ToString();
        }

        private void SetFlyoutCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (FlyoutSetSplitButton != null)
            {
                FlyoutSetSplitButton.Flyout = _placementFlyout;
            }
        }

        private void SetFlyoutCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (FlyoutSetSplitButton != null)
            {
                FlyoutSetSplitButton.Flyout = null;
            }
        }

        private void ToggleSplitButton_IsCheckedChanged(ToggleSplitButton sender, ToggleSplitButtonIsCheckedChangedEventArgs args)
        {
            ToggleStateTextBlock.Text = ToggleSplitButton.IsChecked ? "Checked" : "Unchecked";
        }

        private void ToggleSplitButton_Click(object sender, object e)
        {
            ToggleStateOnClickTextBlock.Text = ToggleSplitButton.IsChecked ? "Checked" : "Unchecked";
        }
    }

    public class MyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private SplitButtonPage _parentPage;
        private bool _canExecute = true;

        public MyCommand() { }

        public MyCommand(SplitButtonPage parentPage)
        {
            _parentPage = parentPage;
        }

        public void UpdateCanExecute(bool canExecute)
        {
            _canExecute = canExecute;
            if (CanExecuteChanged != null)
            {
                EventArgs args = new EventArgs();
                CanExecuteChanged(this, args);
            }
        }

        public bool CanExecute(object o)
        {
            return _canExecute;
        }

        public void Execute(object o)
        {
            _parentPage.CommandExecute();
        }
    }
}
