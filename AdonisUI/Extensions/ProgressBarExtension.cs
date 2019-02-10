using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AdonisUI.Extensions
{
    public class ProgressBarExtension
    {
        public static bool GetIsProgressAnimationEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsProgressAnimationEnabledProperty);
        }

        public static void SetIsProgressAnimationEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsProgressAnimationEnabledProperty, value);
        }

        public static readonly DependencyProperty IsProgressAnimationEnabledProperty = DependencyProperty.RegisterAttached("IsProgressAnimationEnabled", typeof(bool), typeof(ProgressBarExtension), new PropertyMetadata(false));
    }
}
