using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdonisUI.Controls;
using AdonisUI.Demo.Framework;
using AdonisUI.Demo.ViewModels;

namespace AdonisUI.Demo.Commands
{
    class MessageBoxSampleShowMessageBoxCommand
        : ViewModelCommand<MessageBoxSampleViewModel>
    {
        public int MessageLength { get; set; } = 100;
        
        public int CaptionLength { get; set; } = 10;
        
        public MessageBoxImage Icon { get; set; } = MessageBoxImage.Information;

        public IMessageBoxButtonModel[] Buttons { get; set; } = { MessageBoxButtons.Cancel() };

        public IMessageBoxCheckBoxModel[] CheckBoxes { get; set; } = {};

        public MessageBoxSampleShowMessageBoxCommand(MessageBoxSampleViewModel contextViewModel) : base(contextViewModel)
        {
        }

        public override void Execute(MessageBoxSampleViewModel contextViewModel, object parameter)
        {
            var messageBox = new MessageBoxModel
            {
                Text = CreateMessage(MessageLength),
                Caption = CreateMessage(CaptionLength),
                Icon = Icon,
                Buttons = Buttons,
                CheckBoxes = CheckBoxes,
            };

            contextViewModel.CurrentMessageBox = messageBox;

            MessageBox.Show(messageBox);
        }

        private string CreateMessage(int charCount)
        {
            Random rng = new Random();
            StringBuilder builder = new StringBuilder(charCount);

            while (builder.Length < charCount)
            {
                builder.Append(_availableWords[rng.Next(_availableWords.Length)]);
                builder.Append(" ");
            }

            return builder.ToString();
        }

        private string[] _availableWords =
        {
            "Lorem",
            "ipsum",
            "dolor",
            "sit",
            "amet",
            "consetetur",
            "sadipscing",
            "elitr",
            "sed",
            "diam",
            "nonumy",
            "eirmod",
            "tempor",
            "invidunt",
            "ut",
            "labore",
            "et",
            "dolore",
            "magna",
            "aliquyam",
            "erat",
            "sed",
            "diam",
            "voluptua",
        };
    }
}
