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

        public static MessageBoxResult Show(string text)
        {
            var messageBoxModel = new MessageBoxViewModel
            {
                Text = text,
            };

            return Show(messageBoxModel);
        }

        public static MessageBoxResult Show(IMessageBoxViewModel messageBoxModel)
        {
            var messageBox = new MessageBox
            {
                ViewModel = messageBoxModel,
            };

            if (messageBoxModel.IsSoundEnabled)
                messageBox.PlayOpeningSound();

            messageBox.ShowDialog();
            return messageBoxModel.Result;
        }

        public static MessageBoxResult Show(Window owner, IMessageBoxViewModel messageBoxModel)
        {
            var messageBox = new MessageBox
            {
                Owner = owner,
                ViewModel = messageBoxModel,
            };

            if (messageBoxModel.IsSoundEnabled)
                messageBox.PlayOpeningSound();

            messageBox.ShowDialog();
            return messageBoxModel.Result;
        }

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
