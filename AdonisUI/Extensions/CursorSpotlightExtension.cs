using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace AdonisUI.Extensions
{
    public class CursorSpotlightExtension
    {
        private const string SpotlightName = "CursorSpotlightExtension_SpotlightName_Key";

        public static FrameworkElement GetMouseEventSourceForSpotlight(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(MouseEventSourceForSpotlightProperty);
        }

        public static void SetMouseEventSourceForSpotlight(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(MouseEventSourceForSpotlightProperty, value);
        }

        public static readonly DependencyProperty MouseEventSourceForSpotlightProperty = DependencyProperty.RegisterAttached("MouseEventSourceForSpotlight", typeof(FrameworkElement), typeof(CursorSpotlightExtension), new PropertyMetadata(null, MouseEventTargetPropertyChanged));

        private static void MouseEventTargetPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue != null)
                ClearSpotlight(obj as FrameworkElement, args.OldValue as FrameworkElement);

            if (args.NewValue != null)
                InitSpotlight(obj as FrameworkElement, args.NewValue as FrameworkElement);
        }

        private static void InitSpotlight(FrameworkElement spotlightTarget, FrameworkElement mouseEventSource)
        {
            // binding required to enable bindings to spotlightTarget's size in visual brush
            // see https://social.msdn.microsoft.com/Forums/vstudio/en-US/21580413-6f42-429c-b9e0-17331bae87cc/binding-width-and-height-of-a-border-in-a-visualbrush-resource?forum=wpf
            BindingOperations.SetBinding(spotlightTarget, FrameworkElement.DataContextProperty, new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
            });

            var opacityMask = new VisualBrush();
            var canvas = new Canvas();

            BindingOperations.SetBinding(canvas, FrameworkElement.WidthProperty, new Binding("ActualWidth"));
            BindingOperations.SetBinding(canvas, FrameworkElement.HeightProperty, new Binding("ActualHeight"));

            canvas.ClipToBounds = true;
            canvas.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            opacityMask.Visual = canvas;
            spotlightTarget.OpacityMask = opacityMask;

            mouseEventSource.MouseEnter += TargetElementOnMouseEnter(spotlightTarget);
            mouseEventSource.PreviewMouseMove += TargetElementOnMouseMove(spotlightTarget);
            mouseEventSource.MouseLeave += TargetElementOnMouseLeave(spotlightTarget);
        }

        private static void ClearSpotlight(FrameworkElement spotlightTarget, FrameworkElement mouseEventSource)
        {
            spotlightTarget.OpacityMask = null;

            mouseEventSource.MouseEnter -= TargetElementOnMouseEnter(spotlightTarget);
            mouseEventSource.PreviewMouseMove -= TargetElementOnMouseMove(spotlightTarget);
            mouseEventSource.MouseLeave -= TargetElementOnMouseLeave(spotlightTarget);
        }

        private static MouseEventHandler TargetElementOnMouseEnter(FrameworkElement spotlightTarget)
        {
            return (sender, args) => {
                if (!((spotlightTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                canvas.Children.Add(CreateSpotlight(spotlightTarget));
            };
        }

        private static MouseEventHandler TargetElementOnMouseMove(FrameworkElement spotlightTarget)
        {
            return (sender, args) =>
            {
                if (!((spotlightTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                Ellipse spotlight = canvas.Children.OfType<Ellipse>().FirstOrDefault(e => e.Name == SpotlightName);
                if (spotlight == null)
                {
                    spotlight = CreateSpotlight(spotlightTarget);
                    canvas.Children.Add(spotlight);
                }

                Point cursorLocation = args.MouseDevice.GetPosition(spotlightTarget);

                Canvas.SetTop(spotlight, cursorLocation.Y - spotlight.ActualHeight / 2);
                Canvas.SetLeft(spotlight, cursorLocation.X - spotlight.ActualWidth / 2);
            };
        }

        private static MouseEventHandler TargetElementOnMouseLeave(FrameworkElement spotlightTarget)
        {
            return (sender, args) =>
            {
                if (!((spotlightTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                Ellipse spotlight = canvas.Children.OfType<Ellipse>().FirstOrDefault(e => e.Name == SpotlightName);
                if (spotlight != null)
                    canvas.Children.Remove(spotlight);
            };
        }

        private static Ellipse CreateSpotlight(FrameworkElement targetElement)
        {
            double spotlightSize = Math.Max(targetElement.ActualWidth, targetElement.ActualHeight) * 1.65;

            // limit blur radius size to solve rendering performance issues
            const double maxBlurRadius = 128;

            return new Ellipse
            {
                Name = SpotlightName,
                Width = spotlightSize,
                Height = spotlightSize,
                Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                Effect = new BlurEffect
                {
                    Radius = Math.Min(spotlightSize * 0.75, maxBlurRadius),
                },
            };
        }
    }
}
