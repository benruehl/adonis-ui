using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdonisUI.Extensions
{
    /// <summary>
    /// Provides attached behaviors related to the ScrollViewer control.
    /// </summary>
    public class ScrollViewerExtension
    {
        /// <summary>
        /// Gets the value of the <see cref="VerticalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ScrollViewer))]
        public static ScrollBarExpansionMode GetVerticalScrollBarExpansionMode(DependencyObject obj)
        {
            return (ScrollBarExpansionMode)obj.GetValue(VerticalScrollBarExpansionModeProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="VerticalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static void SetVerticalScrollBarExpansionMode(DependencyObject obj, ScrollBarExpansionMode value)
        {
            obj.SetValue(VerticalScrollBarExpansionModeProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="HorizontalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ScrollViewer))]
        public static ScrollBarExpansionMode GetHorizontalScrollBarExpansionMode(DependencyObject obj)
        {
            return (ScrollBarExpansionMode)obj.GetValue(HorizontalScrollBarExpansionModeProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="HorizontalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static void SetHorizontalScrollBarExpansionMode(DependencyObject obj, ScrollBarExpansionMode value)
        {
            obj.SetValue(HorizontalScrollBarExpansionModeProperty, value);
        }

        /// <summary>
        /// A DependencyProperty that controls when to expand and collapse the vertical scroll bar.
        /// </summary>
        public static readonly DependencyProperty VerticalScrollBarExpansionModeProperty = DependencyProperty.RegisterAttached("VerticalScrollBarExpansionMode", typeof(ScrollBarExpansionMode), typeof(ScrollViewerExtension), new PropertyMetadata(ScrollBarExpansionMode.ExpandOnHover));

        /// <summary>
        /// A DependencyProperty that controls when to expand and collapse the horizontal scroll bar.
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollBarExpansionModeProperty = DependencyProperty.RegisterAttached("HorizontalScrollBarExpansionMode", typeof(ScrollBarExpansionMode), typeof(ScrollViewerExtension), new PropertyMetadata(ScrollBarExpansionMode.ExpandOnHover));
    }
}
