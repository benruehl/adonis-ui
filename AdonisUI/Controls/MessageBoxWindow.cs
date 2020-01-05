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
    [TemplatePart(Name = PART_OkButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_YesButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_NoButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CancelButton, Type = typeof(Button))]
    public class MessageBoxWindow
        : AdonisWindow
    {
        private const string PART_OkButton = "PART_OkButton";
        private const string PART_YesButton = "PART_YesButton";
        private const string PART_NoButton = "PART_NoButton";
        private const string PART_CancelButton = "PART_CancelButton";

        public IMessageBoxViewModel ViewModel
        {
            get => DataContext as IMessageBoxViewModel;
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
            var okButton = UINavigator.FindVisualChild<Button>(this, PART_OkButton);
            var yesButton = UINavigator.FindVisualChild<Button>(this, PART_YesButton);
            var noButton = UINavigator.FindVisualChild<Button>(this, PART_NoButton);
            var cancelButton = UINavigator.FindVisualChild<Button>(this, PART_CancelButton);

            if (okButton != null)
                okButton.Click += OkButton_Click;

            if (yesButton != null)
                yesButton.Click += YesButton_Click;

            if (noButton != null)
                noButton.Click += NoButton_Click;

            if (cancelButton != null)
                cancelButton.Click += CancelButton_Click;

            if (ViewModel.IsSoundEnabled)
                PlayOpeningSound();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Result = MessageBoxResult.OK;
            DialogResult = true;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Result = MessageBoxResult.Yes;
            DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Result = MessageBoxResult.No;
            DialogResult = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Result = MessageBoxResult.Cancel;
            DialogResult = false;
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
