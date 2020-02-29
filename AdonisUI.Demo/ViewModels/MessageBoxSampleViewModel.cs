using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdonisUI.Controls;
using AdonisUI.Demo.Commands;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class MessageBoxSampleViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Message Box";

        public IApplicationContentView.NavigationGroup Group => IApplicationContentView.NavigationGroup.Samples;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private MessageBoxSampleShowMessageBoxCommand _showInfoCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowInfoCommand => _showInfoCommand ?? (_showInfoCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Information,
        });

        private MessageBoxSampleShowMessageBoxCommand _showQuestionCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowQuestionCommand => _showQuestionCommand ?? (_showQuestionCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Question,
        });

        private MessageBoxSampleShowMessageBoxCommand _showWarningCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowWarningCommand => _showWarningCommand ?? (_showWarningCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Warning,
        });

        private MessageBoxSampleShowMessageBoxCommand _showErrorCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowErrorCommand => _showErrorCommand ?? (_showErrorCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Error,
        });

        private MessageBoxSampleShowMessageBoxCommand _showSmallCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowSmallCommand => _showSmallCommand ?? (_showSmallCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 300,
        });

        private MessageBoxSampleShowMessageBoxCommand _showMediumCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowMediumCommand => _showMediumCommand ?? (_showMediumCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 100,
            MessageLength = 5000,
        });

        private MessageBoxSampleShowMessageBoxCommand _showLargeCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowLargeCommand => _showLargeCommand ?? (_showLargeCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 500,
            MessageLength = 10000,
        });

        private MessageBoxSampleShowMessageBoxCommand _showHugeCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowHugeCommand => _showHugeCommand ?? (_showHugeCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 1000,
            MessageLength = 100000,
        });

        private MessageBoxSampleShowMessageBoxCommand _showOkCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowOkCommand => _showOkCommand ?? (_showOkCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            Buttons = new [] { MessageBoxButtons.Ok() },
        });

        private MessageBoxSampleShowMessageBoxCommand _showCancelCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowCancelCommand => _showCancelCommand ?? (_showCancelCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            Buttons = new[] { MessageBoxButtons.Cancel() },
        });

        private MessageBoxSampleShowMessageBoxCommand _showOkCancelCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowOkCancelCommand => _showOkCancelCommand ?? (_showOkCancelCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            Buttons = MessageBoxButtons.OkCancel().ToArray(),
        });

        private MessageBoxSampleShowMessageBoxCommand _showYesNoCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowYesNoCommand => _showYesNoCommand ?? (_showYesNoCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            Buttons = MessageBoxButtons.YesNo().ToArray(),
        });

        private MessageBoxSampleShowMessageBoxCommand _showYesNoCancelCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowYesNoCancelCommand => _showYesNoCancelCommand ?? (_showYesNoCancelCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            Buttons = MessageBoxButtons.YesNoCancel().ToArray(),
        });

        private MessageBoxSampleShowMessageBoxCommand _showCustomButtonsCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowCustomButtonsCommand => _showCustomButtonsCommand ?? (_showCustomButtonsCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            MessageLength = 100,
            Buttons = new[]
            {
                new MessageBoxButtonModel("Extra Cheese", MessageBoxResult.Custom),
                MessageBoxButtons.Custom("Extra Sauce"),
                MessageBoxButtons.Yes("Both please"),
                MessageBoxButtons.Cancel(),
            }
        });

        private MessageBoxSampleShowMessageBoxCommand _showManyButtonsCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowManyButtonsCommand => _showManyButtonsCommand ?? (_showManyButtonsCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            MessageLength = 100,
            Buttons = new[]
            {
                MessageBoxButtons.Custom("Light"),
                MessageBoxButtons.Custom("Dark"),
                MessageBoxButtons.Yes(),
                MessageBoxButtons.Yes(),
                MessageBoxButtons.No(),
                MessageBoxButtons.No(),
                MessageBoxButtons.Cancel(),
                MessageBoxButtons.Cancel(),
            }
        });

        private MessageBoxSampleShowMessageBoxCommand _showCheckBoxBelowTextCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowCheckBoxBelowTextCommand => _showCheckBoxBelowTextCommand ?? (_showCheckBoxBelowTextCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            CheckBoxes = new []
            {
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.BelowText, IsChecked = true },
            },
        });

        private MessageBoxSampleShowMessageBoxCommand _showCheckBoxesBelowTextCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowCheckBoxesBelowTextCommand => _showCheckBoxesBelowTextCommand ?? (_showCheckBoxesBelowTextCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            CheckBoxes = new[]
            {
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.BelowText, IsChecked = true },
                new MessageBoxCheckBoxModel("I accept that this very long labeled check box might cause issues to the message box's layout") { Placement = MessageBoxCheckBoxPlacement.BelowText },
                new MessageBoxCheckBoxModel("Apply to all") { Placement = MessageBoxCheckBoxPlacement.BelowText },
            },
        });

        private MessageBoxSampleShowMessageBoxCommand _showCheckBoxNextToButtonsCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowCheckBoxNextToButtonsCommand => _showCheckBoxNextToButtonsCommand ?? (_showCheckBoxNextToButtonsCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            CheckBoxes = new[]
            {
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.NextToButtons, IsChecked = true },
            },
        });

        private MessageBoxSampleShowMessageBoxCommand _showCheckBoxesNextToButtonsCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowCheckBoxesNextToButtonsCommand => _showCheckBoxesNextToButtonsCommand ?? (_showCheckBoxesNextToButtonsCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            CheckBoxes = new[]
            {
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.NextToButtons, IsChecked = true },
                new MessageBoxCheckBoxModel("I accept that this very long labeled check box might cause issues to the message box's layout") { Placement = MessageBoxCheckBoxPlacement.NextToButtons },
                new MessageBoxCheckBoxModel("Apply to all") { Placement = MessageBoxCheckBoxPlacement.NextToButtons },
            },
        });

        private MessageBoxSampleShowMessageBoxCommand _showCheckBoxesEverywhereCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowCheckBoxesEverywhereCommand => _showCheckBoxesEverywhereCommand ?? (_showCheckBoxesEverywhereCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 1000,
            CheckBoxes = new[]
            {
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.BelowText, IsChecked = true },
                new MessageBoxCheckBoxModel("I accept that this very long labeled check box might cause issues to the message box's layout") { Placement = MessageBoxCheckBoxPlacement.BelowText },
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.NextToButtons, IsChecked = true },
                new MessageBoxCheckBoxModel("I accept that this very long labeled check box might cause issues to the message box's layout") { Placement = MessageBoxCheckBoxPlacement.NextToButtons },
            },
        });

        private MessageBoxSampleShowMessageBoxCommand _showEmptyCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowEmptyCommand => _showEmptyCommand ?? (_showEmptyCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 0,
            MessageLength = 0,
            Buttons = new IMessageBoxButtonModel[] { },
        });

        private MessageBoxSampleShowMessageBoxCommand _showOnlyControlsCommand;

        public MessageBoxSampleShowMessageBoxCommand ShowOnlyControlsCommand => _showOnlyControlsCommand ?? (_showOnlyControlsCommand = new MessageBoxSampleShowMessageBoxCommand(this)
        {
            CaptionLength = 0,
            MessageLength = 0,
            Buttons = new[]
            {
                MessageBoxButtons.Custom("Light"),
                MessageBoxButtons.Custom("Dark"),
                MessageBoxButtons.Yes(),
                MessageBoxButtons.Yes(),
                MessageBoxButtons.No(),
                MessageBoxButtons.No(),
                MessageBoxButtons.Cancel(),
                MessageBoxButtons.Cancel(),
            },
            CheckBoxes = new[]
            {
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.BelowText, IsChecked = true },
                new MessageBoxCheckBoxModel("I accept that this very long labeled check box might cause issues to the message box's layout") { Placement = MessageBoxCheckBoxPlacement.BelowText },
                new MessageBoxCheckBoxModel("Don't show again") { Placement = MessageBoxCheckBoxPlacement.NextToButtons, IsChecked = true },
                new MessageBoxCheckBoxModel("I accept that this very long labeled check box might cause issues to the message box's layout") { Placement = MessageBoxCheckBoxPlacement.NextToButtons },
            },
        });

        private IMessageBoxModel _currentMessageBox;

        public IMessageBoxModel CurrentMessageBox
        {
            get => _currentMessageBox;
            set
            {
                if (_currentMessageBox != value)
                {
                    _currentMessageBox = value;
                    RaisePropertyChanged(nameof(CurrentMessageBox));
                }
            }
        }

        public void Init()
        {
        }
    }
}
