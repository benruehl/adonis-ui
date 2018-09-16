using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace AdonisUI.Extensions
{
    public class RippleExtension
    {
        private const string RippleName = "RippleExtension_RippleElementName_Key";

        public static FrameworkElement GetMouseEventSource(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(MouseEventSourceProperty);
        }

        public static void SetMouseEventSource(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(MouseEventSourceProperty, value);
        }

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

        public static int GetFadeInDuration(DependencyObject obj)
        {
            return (int)obj.GetValue(FadeInDurationProperty);
        }

        public static void SetFadeInDuration(DependencyObject obj, int value)
        {
            obj.SetValue(FadeInDurationProperty, value);
        }

        public static int GetFadeOutDuration(DependencyObject obj)
        {
            return (int)obj.GetValue(FadeOutDurationProperty);
        }

        public static void SetFadeOutDuration(DependencyObject obj, int value)
        {
            obj.SetValue(FadeOutDurationProperty, value);
        }

        private static Timeline GetAnimationToComplete(DependencyObject obj)
        {
            return (Timeline)obj.GetValue(AnimationToCompleteProperty);
        }

        private static void SetAnimationToComplete(DependencyObject obj, Timeline value)
        {
            obj.SetValue(AnimationToCompleteProperty, value);
        }

        private static bool GetIsAnimationComplete(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAnimationCompleteProperty);
        }

        private static void SetIsAnimationComplete(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAnimationCompleteProperty, value);
        }

        public static readonly DependencyProperty MouseEventSourceProperty = DependencyProperty.RegisterAttached("MouseEventSource", typeof(FrameworkElement), typeof(RippleExtension), new PropertyMetadata(null, MouseEventTargetPropertyChanged));

        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.RegisterAttached("BackgroundBrush", typeof(Brush), typeof(RippleExtension), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(RippleExtension), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FadeInDurationProperty = DependencyProperty.RegisterAttached("FadeInDuration", typeof(int), typeof(RippleExtension), new FrameworkPropertyMetadata(200, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FadeOutDurationProperty = DependencyProperty.RegisterAttached("FadeOutDuration", typeof(int), typeof(RippleExtension), new FrameworkPropertyMetadata(200, FrameworkPropertyMetadataOptions.Inherits));

        private static readonly DependencyProperty AnimationToCompleteProperty = DependencyProperty.RegisterAttached("AnimationToComplete", typeof(Timeline), typeof(RippleExtension), new PropertyMetadata(null));

        private static readonly DependencyProperty IsAnimationCompleteProperty = DependencyProperty.RegisterAttached("IsAnimationComplete", typeof(bool), typeof(RippleExtension), new PropertyMetadata(false));

        private static void MouseEventTargetPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue != null)
                ClearRippleLayer(obj as FrameworkElement, args.OldValue as FrameworkElement);

            if (args.NewValue != null)
                InitRippleLayer(obj as FrameworkElement, args.NewValue as FrameworkElement);
        }

        private static void InitRippleLayer(FrameworkElement rippleTarget, FrameworkElement clickEventSource)
        {
            // binding required to enable bindings to target's size in visual brush
            // see https://social.msdn.microsoft.com/Forums/vstudio/en-US/21580413-6f42-429c-b9e0-17331bae87cc/binding-width-and-height-of-a-border-in-a-visualbrush-resource?forum=wpf
            BindingOperations.SetBinding(rippleTarget, FrameworkElement.DataContextProperty, new Binding
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
            rippleTarget.OpacityMask = opacityMask;

            clickEventSource.PreviewMouseDown += TargetElementOnMouseDown(rippleTarget);
            clickEventSource.PreviewMouseUp += TargetElementOnMouseUp(rippleTarget);
        }

        private static void ClearRippleLayer(FrameworkElement rippleTarget, FrameworkElement clickEventSource)
        {
            rippleTarget.OpacityMask = null;

            clickEventSource.MouseDown -= TargetElementOnMouseDown(rippleTarget);
            clickEventSource.MouseUp -= TargetElementOnMouseUp(rippleTarget);
        }

        private static MouseButtonEventHandler TargetElementOnMouseDown(FrameworkElement rippleTarget)
        {
            return (sender, args) =>
            {
                if (!((rippleTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                Point cursorLocation = args.MouseDevice.GetPosition(rippleTarget);

                Ellipse rippleEllipse = canvas.Children.OfType<Ellipse>().FirstOrDefault(e => e.Name == RippleName);
                if (rippleEllipse == null)
                {
                    rippleEllipse = CreateRippleEllipse(rippleTarget, cursorLocation);
                    canvas.Children.Add(rippleEllipse);
                }

                Canvas.SetTop(rippleEllipse, cursorLocation.Y - rippleEllipse.MaxHeight / 2);
                Canvas.SetLeft(rippleEllipse, cursorLocation.X - rippleEllipse.MaxWidth / 2);

                DoubleAnimation widthAnimation = CreateExpansionAnimation(rippleTarget);
                DoubleAnimation heightAnimation = CreateExpansionAnimation(rippleTarget);
                DoubleAnimation opacityAnimation = CreateExpansionAnimation(rippleTarget);

                ScaleTransform rippleScaleTransform = (ScaleTransform)rippleEllipse.RenderTransform;

                SetAnimationToComplete(rippleEllipse, widthAnimation);
                widthAnimation.Completed += (s, e) => SetIsAnimationComplete(widthAnimation, true);

                rippleScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, widthAnimation);
                rippleScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, heightAnimation);
                rippleEllipse.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            };
        }

        private static MouseButtonEventHandler TargetElementOnMouseUp(FrameworkElement rippleTarget)
        {
            return (sender, args) =>
            {
                if (!((rippleTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                Ellipse ripple = canvas.Children.OfType<Ellipse>().FirstOrDefault(e => e.Name == RippleName);

                if (ripple == null)
                    return;

                Timeline animationToComplete = GetAnimationToComplete(ripple);

                Duration animationDuration = new Duration(TimeSpan.FromMilliseconds(GetFadeOutDuration(rippleTarget)));
                DoubleAnimation removeAnimation = new DoubleAnimation(0, animationDuration);
                removeAnimation.Completed += (s, e) => canvas.Children.Remove(ripple);

                if (animationToComplete == null || GetIsAnimationComplete(animationToComplete))
                    ripple.BeginAnimation(UIElement.OpacityProperty, removeAnimation);
                else
                    animationToComplete.Completed += (s, e) => ripple.BeginAnimation(UIElement.OpacityProperty, removeAnimation); ;
            };
        }

        private static Ellipse CreateRippleEllipse(FrameworkElement targetElement, Point cursorLocation)
        {
            double distanceToTopLeftCoordinate = Point.Subtract(new Point(0, 0), cursorLocation).Length;
            double distanceToTopRightCoordinate = Point.Subtract(new Point(targetElement.ActualWidth, 0), cursorLocation).Length;
            double distanceToButtomLeftCoordinate = Point.Subtract(new Point(0, targetElement.ActualHeight), cursorLocation).Length;
            double distanceToBottomRightCoordinate = Point.Subtract(new Point(targetElement.ActualWidth, targetElement.ActualHeight), cursorLocation).Length;

            double maxRippleSize = 2 * Math.Max(Math.Max(Math.Max(distanceToTopLeftCoordinate, distanceToTopRightCoordinate), distanceToButtomLeftCoordinate), distanceToBottomRightCoordinate);

            return new Ellipse
            {
                Name = RippleName,
                Width = maxRippleSize,
                Height = maxRippleSize,
                MaxWidth = maxRippleSize,
                MaxHeight = maxRippleSize,
                Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                Opacity = 0.25,
                RenderTransform = new ScaleTransform(0, 0, maxRippleSize / 2, maxRippleSize / 2),
            };
        }

        private static DoubleAnimation CreateExpansionAnimation(FrameworkElement targetElement)
        {
            Duration animationDuration = new Duration(TimeSpan.FromMilliseconds(GetFadeInDuration(targetElement)));

            DoubleAnimation sizeAnimation = new DoubleAnimation(1, animationDuration)
            {
                EasingFunction = new CircleEase
                {
                    EasingMode = EasingMode.EaseInOut,
                },
            };

            Timeline.SetDesiredFrameRate(sizeAnimation, 60);

            return sizeAnimation;
        }
    }
}
