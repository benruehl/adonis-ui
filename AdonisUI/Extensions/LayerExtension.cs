using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AdonisUI.Extensions
{
    public class LayerExtension
    {
        public static int? GetLayer(DependencyObject obj)
        {
            return (int?)obj.GetValue(LayerProperty);
        }

        public static void SetLayer(DependencyObject obj, int? value)
        {
            obj.SetValue(LayerProperty, value);
        }

        public static int GetComputedLayer(DependencyObject obj)
        {
            return (int)obj.GetValue(ComputedLayerProperty);
        }

        private static void SetComputedLayer(DependencyObject obj, int value)
        {
            obj.SetValue(ComputedLayerPropertyKey, value);
        }

        public static readonly DependencyProperty LayerProperty = DependencyProperty.RegisterAttached("Layer", typeof(int?), typeof(LayerExtension), new PropertyMetadata(null, OnLayerPropertyChanged));

        private static readonly DependencyPropertyKey ComputedLayerPropertyKey = DependencyProperty.RegisterAttachedReadOnly("ComputedLayer", typeof(int), typeof(LayerExtension), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ComputedLayerProperty = ComputedLayerPropertyKey.DependencyProperty;

        private static void OnLayerPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs eventArgs)
        {
            SetComputedLayer(depObj, (int)eventArgs.NewValue);

            int increasedLayer = (int)eventArgs.NewValue + 1;

            SetComputedLayerOfChildren(depObj as FrameworkElement, increasedLayer);
        }

        private static void SetComputedLayerOfChildren(FrameworkElement element, int targetLayer)
        {
            if (element == null)
                return;

            element.Loaded += (sender, args) =>
            {
                foreach (object child in LogicalTreeHelper.GetChildren((DependencyObject)sender))
                {
                    if (!(child is DependencyObject childObject))
                        continue;

                    if (GetLayer(childObject) == null)
                        SetComputedLayer(childObject, targetLayer);
                }
            };
        }
    }
}
