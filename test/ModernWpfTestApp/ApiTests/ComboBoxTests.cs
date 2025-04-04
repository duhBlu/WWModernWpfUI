﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Common;
using MUXControlsTestApp.Utilities;
using System.Windows.Controls;
using System.Linq;
using MUXControlsTestApp;
using System.Windows;

#if USING_TAEF
using WEX.TestExecution;
using WEX.TestExecution.Markup;
using WEX.Logging.Interop;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace ModernWpf.Tests.MUXControls.ApiTests
{
    [TestClass]
    public class ComboBoxTests : ApiTestBase
    {
        [TestMethod]
        public void VerifyComboBoxOverlayCornerRadius()
        {
            /*
            if (PlatformConfiguration.IsOSVersionLessThan(OSVersion.Redstone5))
            {
                Log.Warning("ComboBox corner radius is not available pre-rs5");
                return;
            }
            */

            var comboBox = SetupComboBox();
            RunOnUIThread.Execute(() =>
            {
                comboBox.SetCornerRadius(new CornerRadius(2));
                comboBox.IsDropDownOpen = true;
            });
            IdleSynchronizer.Wait();

            RunOnUIThread.Execute(() =>
            {
                var background = TestUtilities.FindDescendents<Border>(comboBox).Where(e => e.Name == "Background").Single();
                Verify.AreEqual(new CornerRadius(2, 2, 0, 0 /*2, 2, 2, 2*/), background.CornerRadius);

                var overlayCornerRadius = new CornerRadius(0, 0, 0, 0);
                var radius = App.Current.Resources["OverlayCornerRadius"];
                if (radius != null)
                {
                    overlayCornerRadius = (CornerRadius)radius;
                }
                var popup = VisualTreeHelperEx.GetOpenPopups(WindowEx.Current).Last();
                var popupBorder = TestUtilities.FindDescendents<Border>(popup).Where(e => e.Name == "PopupBorder").Single();
                Verify.AreEqual(new CornerRadius(0, 0, overlayCornerRadius.BottomRight, overlayCornerRadius.BottomLeft) /*overlayCornerRadius*/, popupBorder.CornerRadius);
            });
        }

        [TestMethod]
        public void VerifyComboBoxEditModeCornerRadius()
        {
            /*
            if (PlatformConfiguration.IsOSVersionLessThan(OSVersion.Redstone5))
            {
                Log.Warning("ComboBox corner radius is not available pre-rs5");
                return;
            }
            */

            var comboBox = SetupComboBox();
            RunOnUIThread.Execute(() =>
            {
                comboBox.SetCornerRadius(new CornerRadius(2));
                comboBox.IsEditable = true;
                comboBox.IsDropDownOpen = true;
            });
            IdleSynchronizer.Wait();

            RunOnUIThread.Execute(() =>
            {
                var editableText = TestUtilities.FindDescendents<TextBox>(comboBox).Where(e => e.Name == "PART_EditableTextBox").Single();
                Verify.AreEqual(new CornerRadius(2, 2, 0, 0), editableText.GetCornerRadius());

                var overlayCornerRadius = new CornerRadius(0, 0, 0, 0);
                var radius = App.Current.Resources["OverlayCornerRadius"];
                if (radius != null)
                {
                    overlayCornerRadius = (CornerRadius)radius;
                }
                var popup = VisualTreeHelperEx.GetOpenPopups(WindowEx.Current).Last();
                var popupBorder = TestUtilities.FindDescendents<Border>(popup).Where(e => e.Name == "PopupBorder").Single();
                Verify.AreEqual(new CornerRadius(0, 0, overlayCornerRadius.BottomRight, overlayCornerRadius.BottomLeft), popupBorder.CornerRadius);
            });
        }

        /*
        [TestMethod]
        public void VerifyVisualTree()
        {
            var comboBox = SetupComboBox(useContent: false);
            RunOnUIThread.Execute(() =>
            {
                comboBox.IsDropDownOpen = true;
            });
            IdleSynchronizer.Wait();

            VisualTreeTestHelper.VerifyVisualTree(root: comboBox, masterFilePrefix: "ComboBox");
        }
        */

        private ComboBox SetupComboBox(bool useContent = true)
        {
            ComboBox comboBox = null;
            RunOnUIThread.Execute(() =>
            {
                comboBox = new ComboBox();
                comboBox.Items.Add("Item 1");
                comboBox.Items.Add("Item 2");
                comboBox.Items.Add("Item 3");
                comboBox.Items.Add("Item 4");
                comboBox.Items.Add("Item 5");
                comboBox.Items.Add("Item 6");
                if (useContent)
                {
                    Content = comboBox;
                    Content.UpdateLayout();
                }
            });
            if (!useContent)
            {
                TestUtilities.SetAsVisualTreeRoot(comboBox);
            }
            Verify.IsNotNull(comboBox);
            return comboBox;
        }

    }
}
