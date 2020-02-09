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
    class MessageBoxDemoViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Message Box";

        public bool HasPreviousView => true;

        public bool HasNextView => false;

        private MessageBoxDemoShowMessageBoxCommand _showInfoCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowInfoCommand => _showInfoCommand ?? (_showInfoCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Information,
        });

        private MessageBoxDemoShowMessageBoxCommand _showQuestionCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowQuestionCommand => _showQuestionCommand ?? (_showQuestionCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Question,
        });

        private MessageBoxDemoShowMessageBoxCommand _showWarningCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowWarningCommand => _showWarningCommand ?? (_showWarningCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Warning,
        });

        private MessageBoxDemoShowMessageBoxCommand _showErrorCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowErrorCommand => _showErrorCommand ?? (_showErrorCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            Icon = MessageBoxImage.Error,
        });

        private MessageBoxDemoShowMessageBoxCommand _showSmallCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowSmallCommand => _showSmallCommand ?? (_showSmallCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            CaptionLength = 10,
            MessageLength = 300,
        });

        private MessageBoxDemoShowMessageBoxCommand _showMediumCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowMediumCommand => _showMediumCommand ?? (_showMediumCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            CaptionLength = 100,
            MessageLength = 5000,
        });

        private MessageBoxDemoShowMessageBoxCommand _showLargeCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowLargeCommand => _showLargeCommand ?? (_showLargeCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            CaptionLength = 500,
            MessageLength = 10000,
        });

        private MessageBoxDemoShowMessageBoxCommand _showHugeCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowHugeCommand => _showHugeCommand ?? (_showHugeCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            CaptionLength = 1000,
            MessageLength = 100000,
        });

        private MessageBoxDemoShowMessageBoxCommand _showEmptyCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowEmptyCommand => _showEmptyCommand ?? (_showEmptyCommand = new MessageBoxDemoShowMessageBoxCommand(this)
        {
            CaptionLength = 0,
            MessageLength = 0,
            Buttons = new IMessageBoxButtonModel[] { },
        });

        private MessageBoxDemoShowMessageBoxCommand _showCustomButtonsCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowCustomButtonsCommand => _showCustomButtonsCommand ?? (_showCustomButtonsCommand = new MessageBoxDemoShowMessageBoxCommand(this)
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

        private MessageBoxDemoShowMessageBoxCommand _showManyButtonsCommand;

        public MessageBoxDemoShowMessageBoxCommand ShowManyButtonsCommand => _showManyButtonsCommand ?? (_showManyButtonsCommand = new MessageBoxDemoShowMessageBoxCommand(this)
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

        public IApplicationContentView GetPreviousView()
        {
            return new ValidationDemoViewModel();
        }

        public IApplicationContentView GetNextView()
        {
            throw new NotImplementedException();
        }
    }
}
