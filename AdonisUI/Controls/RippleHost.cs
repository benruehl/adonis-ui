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
using System.Windows.Shapes;

namespace AdonisUI.Controls
{
    public class RippleHost : ContentControl
    {
        private const string RippleName = "RippleExtension_RippleElementName_Key";

        static RippleHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleHost), new FrameworkPropertyMetadata(typeof(RippleHost)));
        }

        public FrameworkElement MouseEventSource
        {
            get => (FrameworkElement)GetValue(MouseEventSourceProperty);
            set => SetValue(MouseEventSourceProperty, value);
        }

        public TimeSpan FadeInDuration
        {
            get => (TimeSpan)GetValue(FadeInDurationProperty);
            set => SetValue(FadeInDurationProperty, value);
        }

        public TimeSpan FadeOutDuration
        {
            get => (TimeSpan)GetValue(FadeOutDurationProperty);
            set => SetValue(FadeOutDurationProperty, value);
        }

        private Timeline AnimationToComplete { get; set; }

        private static bool GetIsAnimationComplete(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAnimationCompleteProperty);
        }

        private static void SetIsAnimationComplete(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAnimationCompleteProperty, value);
        }

        public static readonly DependencyProperty MouseEventSourceProperty = DependencyProperty.Register("MouseEventSource", typeof(FrameworkElement), typeof(RippleHost), new FrameworkPropertyMetadata(MouseEventSourcePropertyChanged, null));

        public static readonly DependencyProperty FadeInDurationProperty = DependencyProperty.Register("FadeInDuration", typeof(TimeSpan), typeof(RippleHost), new PropertyMetadata(TimeSpan.FromMilliseconds(200)));

        public static readonly DependencyProperty FadeOutDurationProperty = DependencyProperty.Register("FadeOutDuration", typeof(TimeSpan), typeof(RippleHost), new PropertyMetadata(TimeSpan.FromMilliseconds(200)));

        private static readonly DependencyProperty IsAnimationCompleteProperty = DependencyProperty.RegisterAttached("IsAnimationComplete", typeof(bool), typeof(RippleHost), new PropertyMetadata(false));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FrameworkElement mouseEventSource = MouseEventSource ?? this;

            InitRippleLayer(mouseEventSource);
        }

        private static void MouseEventSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
                ((RippleHost)d).ClearRippleLayer(e.OldValue as FrameworkElement);

            if (e.NewValue != null)
                ((RippleHost)d).InitRippleLayer(e.NewValue as FrameworkElement);
        }

        private void InitRippleLayer(FrameworkElement clickEventSource)
        {
            // binding required to enable bindings to target's size in visual brush
            // see https://social.msdn.microsoft.com/Forums/vstudio/en-US/21580413-6f42-429c-b9e0-17331bae87cc/binding-width-and-height-of-a-border-in-a-visualbrush-resource?forum=wpf
            BindingOperations.SetBinding(this, FrameworkElement.DataContextProperty, new Binding
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
            OpacityMask = opacityMask;

            clickEventSource.PreviewMouseLeftButtonDown += MouseEventSourceOnMouseDown();
            clickEventSource.PreviewMouseLeftButtonUp += MouseEventSourceOnMouseUp();

            Window parentWindow = Window.GetWindow(clickEventSource);
            if (parentWindow != null)
            {
                parentWindow.PreviewMouseLeftButtonUp += MouseEventSourceOnMouseUp();
                parentWindow.Deactivated += ParentWindowOnDeactivated;
            }

            IsVisibleChanged += (s, e) => Reset();
        }

        private void ClearRippleLayer(FrameworkElement clickEventSource)
        {
            OpacityMask = null;

            clickEventSource.PreviewMouseLeftButtonDown -= MouseEventSourceOnMouseDown();
            clickEventSource.PreviewMouseLeftButtonUp -= MouseEventSourceOnMouseUp();

            Window parentWindow = Window.GetWindow(clickEventSource);
            if (parentWindow != null)
            {
                parentWindow.PreviewMouseLeftButtonUp -= MouseEventSourceOnMouseUp();
                parentWindow.Deactivated -= ParentWindowOnDeactivated;
            }
        }

        private MouseButtonEventHandler MouseEventSourceOnMouseDown()
        {
            return (sender, args) => StartRipple(args.MouseDevice.GetPosition(this));
        }

        private void StartRipple(Point cursorLocation)
        {
            if (!((OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                return;

            Ellipse rippleEllipse = canvas.Children.OfType<Ellipse>().FirstOrDefault(e => e.Name == RippleName);
            if (rippleEllipse == null)
            {
                rippleEllipse = CreateRippleEllipse(cursorLocation);
                canvas.Children.Add(rippleEllipse);
            }

            Canvas.SetTop(rippleEllipse, cursorLocation.Y - rippleEllipse.MaxHeight / 2);
            Canvas.SetLeft(rippleEllipse, cursorLocation.X - rippleEllipse.MaxWidth / 2);

            DoubleAnimation widthAnimation = CreateExpansionAnimation();
            DoubleAnimation heightAnimation = CreateExpansionAnimation();
            DoubleAnimation opacityAnimation = CreateExpansionAnimation();

            ScaleTransform rippleScaleTransform = (ScaleTransform)rippleEllipse.RenderTransform;

            AnimationToComplete = widthAnimation;
            widthAnimation.Completed += (s, e) => SetIsAnimationComplete(widthAnimation, true);

            rippleScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, widthAnimation);
            rippleScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, heightAnimation);
            rippleEllipse.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }

        private MouseButtonEventHandler MouseEventSourceOnMouseUp()
        {
            return (sender, args) => EndRipple();
        }

        private void ParentWindowOnDeactivated(object sender, EventArgs args)
        {
            EndRipple();
        }

        private void EndRipple()
        {
            if (!((OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                return;

            Ellipse ripple = canvas.Children.OfType<Ellipse>().FirstOrDefault(e => e.Name == RippleName);

            if (ripple == null)
                return;

            Duration animationDuration = new Duration(FadeOutDuration);
            DoubleAnimation removeAnimation = new DoubleAnimation(0, animationDuration);
            removeAnimation.Completed += (s, e) => canvas.Children.Remove(ripple);

            if (AnimationToComplete == null || GetIsAnimationComplete(AnimationToComplete))
                ripple.BeginAnimation(UIElement.OpacityProperty, removeAnimation);
            else
                AnimationToComplete.Completed += (s, e) => ripple.BeginAnimation(UIElement.OpacityProperty, removeAnimation);
        }

        private Ellipse CreateRippleEllipse(Point cursorLocation)
        {
            double distanceToTopLeftCoordinate = Point.Subtract(new Point(0, 0), cursorLocation).Length;
            double distanceToTopRightCoordinate = Point.Subtract(new Point(ActualWidth, 0), cursorLocation).Length;
            double distanceToButtomLeftCoordinate = Point.Subtract(new Point(0, ActualHeight), cursorLocation).Length;
            double distanceToBottomRightCoordinate = Point.Subtract(new Point(ActualWidth, ActualHeight), cursorLocation).Length;

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

        private DoubleAnimation CreateExpansionAnimation()
        {
            Duration animationDuration = new Duration(FadeInDuration);

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

        public void Reset()
        {
            if (!((OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                return;

            Ellipse ripple = canvas.Children.OfType<Ellipse>().FirstOrDefault(e => e.Name == RippleName);

            if (ripple == null)
                return;

            canvas.Children.Remove(ripple);
        }
    }
}
