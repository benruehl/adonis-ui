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
    [TemplatePart(Name = PART_OkButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_YesButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_NoButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CancelButton, Type = typeof(Button))]
    public class MessageBox
        : AdonisWindow
    {
        private const string PART_OkButton = "PART_OkButton";
        private const string PART_YesButton = "PART_YesButton";
        private const string PART_NoButton = "PART_NoButton";
        private const string PART_CancelButton = "PART_CancelButton";

        private IMessageBoxViewModel ViewModel
        {
            get => DataContext as IMessageBoxViewModel;
            set => DataContext = value;
        }

        public Brush DialogButtonsBackground
        {
            get => GetValue(DialogButtonsBackgroundProperty) as Brush;
            set => SetValue(DialogButtonsBackgroundProperty, value);
        }

        public static readonly DependencyProperty DialogButtonsBackgroundProperty = DependencyProperty.Register(nameof(DialogButtonsBackground), typeof(Brush), typeof(MessageBox), new PropertyMetadata(null));

        static MessageBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBox), new FrameworkPropertyMetadata(typeof(MessageBox)));
        }

        public MessageBox()
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
            if (ViewModel is MessageBoxViewModel viewModel)
                viewModel.Result = MessageBoxResult.OK;
            DialogResult = true;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is MessageBoxViewModel viewModel)
                viewModel.Result = MessageBoxResult.Yes;
            DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is MessageBoxViewModel viewModel)
                viewModel.Result = MessageBoxResult.No;
            DialogResult = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is MessageBoxViewModel viewModel)
                viewModel.Result = MessageBoxResult.Cancel;
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

        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that specifies the text to display.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text)
        {
            var messageBoxModel = new MessageBoxViewModel
            {
                Text = text,
            };

            return Show(messageBoxModel);
        }

        /// <summary>
        /// Displays a message box that is configured like specified in the <see cref="IMessageBoxViewModel"/> and that returns a result.
        /// </summary>
        /// <param name="messageBoxModel">An <see cref="IMessageBoxViewModel"/> that configures the appearance and behavior of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(IMessageBoxViewModel messageBoxModel)
        {
            Window activeWindow = Application.Current?.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            return Show(activeWindow, messageBoxModel);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box is configured like specified in the <see cref="IMessageBoxViewModel"/> and returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="messageBoxModel">An <see cref="IMessageBoxViewModel"/> that configures the appearance and behavior of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, IMessageBoxViewModel messageBoxModel)
        {
            var messageBox = new MessageBox
            {
                Owner = owner,
                ViewModel = messageBoxModel,
            };

            messageBox.ShowDialog();
            return messageBoxModel.Result;
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default message box result, custom button labels and returns a result.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="String"/> that specifies the title bar caption to display.</param>
        /// <param name="buttons">A <see cref="MessageBoxButtons"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <param name="customButtonLabels">A <see cref="Dictionary{MessageBoxButton,String}"/> holding mappings between message box buttons and their labels that can be used to override the default labels.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text, string caption = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None, Dictionary<MessageBoxButton, string> customButtonLabels = null)
        {
            var messageBoxModel = new MessageBoxViewModel
            {
                Text = text,
                Caption = caption,
                Buttons = buttons,
                Icon = icon,
                DefaultResult = defaultResult,
                CustomButtonLabels = customButtonLabels,
            };

            return Show(messageBoxModel);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and accepts a default message box result, custom button labels, and returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">A <see cref="String"/> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="String"/> that specifies the title bar caption to display.</param>
        /// <param name="buttons">A <see cref="MessageBoxButtons"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <param name="customButtonLabels">A <see cref="Dictionary{MessageBoxButton,String}"/> holding mappings between message box buttons and their labels that can be used to override the default labels.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text, string caption = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None, Dictionary<MessageBoxButton, string> customButtonLabels = null)
        {
            var messageBoxModel = new MessageBoxViewModel
            {
                Text = text,
                Caption = caption,
                Buttons = buttons,
                Icon = icon,
                DefaultResult = defaultResult,
                CustomButtonLabels = customButtonLabels,
            };

            return Show(owner, messageBoxModel);
        }
    }
}
