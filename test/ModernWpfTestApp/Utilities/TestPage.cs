﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ModernWpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Media;
using Page = ModernWpf.Controls.Page;

namespace MUXControlsTestApp
{
    public class TestPage : Page
    {
        public TestPage()
        {
            AutomationProperties.SetName(this, "TestPage");
            Loaded += OnLoaded;

            var resources = new ResourceDictionaryEx();
            resources.ThemeDictionaries["Default"] = new ResourceDictionary
            {
                { "WhichTheme" , ApplicationTheme.Dark }
            };
            resources.ThemeDictionaries["Light"] = new ResourceDictionary
            {
                { "WhichTheme" , ApplicationTheme.Light }
            };
            resources.ThemeDictionaries["HighContrast"] = new ResourceDictionary
            {
                { "WhichTheme" , ApplicationTheme.Light }
            };
            Resources = resources;
        }

        private static DependencyProperty s_currentTheme = DependencyProperty.RegisterAttached("CurrentTheme", typeof(ApplicationTheme), typeof(TestPage),
            new PropertyMetadata(ApplicationTheme.Light, OnCurrentThemeChangedStatic));

        private bool _loaded = false;

        private static void OnCurrentThemeChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var page = d as TestPage;
            if (page != null && page._loaded) // Don't call unless we've been loaded
            {
                page.OnCurrentThemeChanged();
            }
        }

        protected virtual void OnCurrentThemeChanged()
        {
        }

        protected ApplicationTheme CurrentTheme
        {
            get { return GetCurrentTheme(this); }
        }

        public static DependencyProperty CurrentThemeProperty { get { return s_currentTheme; } set { s_currentTheme = value; } }
        public static void SetCurrentTheme(DependencyObject obj, ApplicationTheme value)
        {
            obj.SetValue(s_currentTheme, value);
        }
        public static ApplicationTheme GetCurrentTheme(DependencyObject obj)
        {
            return (ApplicationTheme)obj.GetValue(s_currentTheme);
        }

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    if (!DesignModeHelpers.IsInDesignMode)
        //    {
        //        var testContentLoadedCheckBox = SearchVisualTree(this.Frame, "TestContentLoadedCheckBox") as CheckBox;
        //        if (testContentLoadedCheckBox != null)
        //        {
        //            testContentLoadedCheckBox.IsChecked = false;
        //        }

        //        var testFrame = this.Frame as TestFrame;
        //        testFrame.ChangeBarVisibility(Visibility.Visible);
        //    }
        //}

        DependencyObject SearchVisualTree(DependencyObject root, string name)
        {
            int size = VisualTreeHelper.GetChildrenCount(root);
            DependencyObject child = null;

            for (int i = 0; i < size && child == null; i++)
            {
                DependencyObject depObj = VisualTreeHelper.GetChild(root, i);
                FrameworkElement fe = depObj as FrameworkElement;

                if (fe.Name.Equals(name))
                {
                    child = fe;
                }
                else
                {
                    child = SearchVisualTree(fe, name);
                }
            }

            return child;
        }

        public static DependencyObject FindVisualChildByName(FrameworkElement parent, string name)
        {
            if (parent.Name == name)
            {
                return parent;
            }

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                FrameworkElement childAsFE = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;

                if (childAsFE != null)
                {
                    DependencyObject result = FindVisualChildByName(childAsFE, name);

                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        public static List<T> FindVisualChildrenByType<T>(FrameworkElement parent) where T : class
        {
            List<T> children = new List<T>();
            T parentAsT = parent as T;

            if (parentAsT != null)
            {
                children.Add(parentAsT);
            }

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                FrameworkElement childAsFE = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;

                if (childAsFE != null)
                {
                    List<T> result = FindVisualChildrenByType<T>(childAsFE);
                    children.AddRange(result);
                }
            }

            return children;
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new FrameworkElementAutomationPeer(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _loaded = true;
            var automationPeer = new FrameworkElementAutomationPeer(this);
            automationPeer.RaiseAutomationEvent(AutomationEvents.AsyncContentLoaded);

            //if (!DesignModeHelpers.IsInDesignMode)
            //{
            //    var frame = Application.Current.MainWindow.Content != null ?
            //                    Application.Current.MainWindow.Content as Frame :
            //                    this.Frame;
            //    var testContentLoadedCheckBox = FindVisualChildByName(frame, "TestContentLoadedCheckBox") as CheckBox;
            //    if (testContentLoadedCheckBox != null)
            //    {
            //        testContentLoadedCheckBox.IsChecked = true;
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException("The test loaded checkbox was not found.");
            //    }
            //}
        }
    }
}
