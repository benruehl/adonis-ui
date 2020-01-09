using AdonisUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public static readonly DependencyProperty DialogButtonsBackgroundProperty = DependencyProperty.Register(nameof(DialogButtonsBackground), typeof(Brush), typeof(MessageBoxWindow), new PropertyMetadata(null));

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

            if (ViewModel != null && ViewModel.IsSoundEnabled)
                PlayOpeningSound();
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
