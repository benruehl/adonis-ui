using AdonisUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Displays a message box.
    /// </summary>
    [TemplatePart(Name = PART_MessageBoxButtonContainer, Type = typeof(DependencyObject))]
    public class MessageBoxWindow
        : AdonisWindow
    {
        private const string PART_MessageBoxButtonContainer = "PART_MessageBoxButtonContainer";

        public IMessageBoxModel ViewModel
        {
            get => DataContext as IMessageBoxModel;
            set => DataContext = value;
        }

        public Brush DialogButtonsBackground
        {
            get => GetValue(DialogButtonsBackgroundProperty) as Brush;
            set => SetValue(DialogButtonsBackgroundProperty, value);
        }

        /// <summary>
        /// A <see cref="double"/> that represents the maximum height of the message box relative to the current screen height.
        /// </summary>
        public double MaxRelativeScreenHeight
        {
            get => (double)GetValue(MaxRelativeScreenHeightProperty);
            set => SetValue(MaxRelativeScreenHeightProperty, value);
        }

        /// <summary>
        /// A <see cref="T:double[]"/> that represents the available relative screen widths the message box can choose from when its size is calculated.
        /// The message box will choose the smallest value where its height does not become larger than specified via <see cref="MaxRelativeScreenHeight"/>.
        /// </summary>
        public double[] MaxRelativeScreenWidthThresholds
        {
            get => (double[])GetValue(MaxRelativeScreenWidthThresholdsProperty);
            set => SetValue(MaxRelativeScreenWidthThresholdsProperty, value);
        }

        public static readonly DependencyProperty DialogButtonsBackgroundProperty = DependencyProperty.Register(nameof(DialogButtonsBackground), typeof(Brush), typeof(MessageBoxWindow), new PropertyMetadata(null));

        /// <summary>
        /// A <see cref="double"/> that represents the maximum height of the message box relative to the current screen height.
        /// </summary>
        public static readonly DependencyProperty MaxRelativeScreenHeightProperty = DependencyProperty.Register("MaxRelativeScreenHeight", typeof(double), typeof(MessageBoxWindow), new PropertyMetadata(0.9));

        /// <summary>
        /// A <see cref="T:double[]"/> that represents the available relative screen widths the message box can choose from when its size is calculated.
        /// The message box will choose the smallest value where its height does not become larger than specified via <see cref="MaxRelativeScreenHeightProperty"/>.
        /// </summary>
        public static readonly DependencyProperty MaxRelativeScreenWidthThresholdsProperty = DependencyProperty.Register("MaxRelativeScreenWidthThresholds", typeof(double[]), typeof(MessageBoxWindow), new PropertyMetadata(new [] { 0.2, 0.6, 0.9 }));

        static MessageBoxWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxWindow), new FrameworkPropertyMetadata(typeof(MessageBoxWindow)));
        }

        /// <summary>
        /// Creates a new instance of <see cref="MessageBoxWindow"/>.
        /// </summary>
        public MessageBoxWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Rect screenBounds = GetCurrentScreenBounds();
            Size transformedScreenBounds = TransformToWindowCoordinates(new Size(screenBounds.Width, screenBounds.Height));
            MaxHeight = transformedScreenBounds.Height * MaxRelativeScreenHeight;
            MaxWidth = CalcMaxWidth(transformedScreenBounds);

            AttachButtonClickHandlers();

            if (ViewModel != null && ViewModel.IsSoundEnabled)
                PlayOpeningSound();
        }

        /// <summary>
        /// Gets the screen bounds of the screen the window is currently in.
        /// </summary>
        /// <returns>A <see cref="Rect"/> representing the current screen bounds.</returns>
        protected Rect GetCurrentScreenBounds()
        {
            return ScreenInterop.FromHandle(new WindowInteropHelper(this).Handle).Bounds;
        }

        /// <summary>
        /// Calculates the maximum width of the window depending on its content and the available screen space.
        /// </summary>
        /// <param name="availableSize">A <see cref="Size"/> that reflects the available size that the window can give to the child.</param>
        /// <returns>A <see cref="double"/> that is used as the window's MaxWidth.</returns>
        protected virtual double CalcMaxWidth(Size availableSize)
        {
            int currentRelativeWidthThresholdIndex = 0;

            // save current max size
            double maxWidth = MaxWidth;
            double maxHeight = MaxHeight;
            
            // remove max height for calculation and test different max widths
            MaxHeight = double.PositiveInfinity;
            MaxWidth = availableSize.Width * MaxRelativeScreenWidthThresholds[currentRelativeWidthThresholdIndex];

            Size measuredSize = base.MeasureOverride(availableSize);

            // measure with different max widths until the window fits the screen or the final maximum is reached
            while (measuredSize.Height > availableSize.Height * MaxRelativeScreenHeight && currentRelativeWidthThresholdIndex + 1 < MaxRelativeScreenWidthThresholds.Length)
            {
                currentRelativeWidthThresholdIndex++;
                MaxWidth = availableSize.Width * MaxRelativeScreenWidthThresholds[currentRelativeWidthThresholdIndex];
                measuredSize = base.MeasureOverride(availableSize);
            }

            // save result
            double fittingMaxWidth = MaxWidth;

            // reset max size
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;

            return fittingMaxWidth;
        }

        /// <summary>
        /// Finds all child buttons in <see cref="PART_MessageBoxButtonContainer"/> and attaches a click handler to of them.
        /// </summary>
        private void AttachButtonClickHandlers()
        {
            var buttonContainer = UINavigator.FindVisualChild<DependencyObject>(this, PART_MessageBoxButtonContainer);

            if (buttonContainer == null)
                throw new InvalidOperationException($"An element named ${PART_MessageBoxButtonContainer} must be present in the message box control template.");

            var buttons = UINavigator.FindVisualChildren<Button>(buttonContainer);

            foreach (Button button in buttons)
            {
                button.Click += MessageBoxButton_Click;

                if (button.IsDefault)
                    button.Focus();
            }
        }

        /// <summary>
        /// Handles a click on one of the displayed message box buttons.
        /// </summary>
        /// <param name="sender">The button that was clicked on.</param>
        /// <param name="e">Event arguments.</param>
        protected virtual void MessageBoxButton_Click(object sender, RoutedEventArgs e)
        {
            IMessageBoxButtonModel buttonModel = (IMessageBoxButtonModel) ((FrameworkElement) sender).DataContext;

            if (ViewModel != null)
            {
                ViewModel.Result = buttonModel.CausedResult;
                ViewModel.ButtonPressed = buttonModel;
            }

            DialogResult = buttonModel.CausedResult != MessageBoxResult.No && buttonModel.CausedResult != MessageBoxResult.Cancel;
        }

        /// <summary>
        /// Plays a system sound depending on the associated <see cref="MessageBoxImage"/>.
        /// </summary>
        protected virtual void PlayOpeningSound()
        {
            switch (ViewModel.Icon)
            {
                case MessageBoxImage.Asterisk:
                case MessageBoxImage.Information:
                    SystemSounds.Asterisk.Play();
                    break;
                case MessageBoxImage.Error:
                case MessageBoxImage.Hand:
                case MessageBoxImage.Stop:
                    SystemSounds.Hand.Play();
                    break;
                case MessageBoxImage.Exclamation:
                case MessageBoxImage.Warning:
                    SystemSounds.Exclamation.Play();
                    break;
                case MessageBoxImage.Question:
                    SystemSounds.Question.Play();
                    break;
            }
        }
    }
}
