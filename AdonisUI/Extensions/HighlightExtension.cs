using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AdonisUI.Extensions
{
    public class HighlightExtension
    {
        public static Brush GetBackgroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundBrushProperty);
        }

        public static void SetBackgroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundBrushProperty, value);
        }

        public static Brush GetBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BorderBrushProperty);
        }

        public static void SetBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BorderBrushProperty, value);
        }

        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.RegisterAttached("BackgroundBrush", typeof(Brush), typeof(HighlightExtension), new PropertyMetadata(System.Windows.Media.Brushes.Transparent));

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(HighlightExtension), new PropertyMetadata(System.Windows.Media.Brushes.Transparent));
    }
}
