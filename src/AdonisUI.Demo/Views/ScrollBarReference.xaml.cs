using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdonisUI.Demo.Views
{
    public partial class ScrollBarReference : ResourceDictionary
    {
        private void HorizontalScrollDemo_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = ((ScrollViewer) sender);
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.ContentHorizontalOffset - e.Delta);
        }
    }
}
