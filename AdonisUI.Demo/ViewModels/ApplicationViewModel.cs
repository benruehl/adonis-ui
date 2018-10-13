using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Commands;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class ApplicationViewModel
        : ViewModel
    {
        private ViewModel _content;

        public ViewModel Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value;
                    RaisePropertyChanged(nameof(Content));

                    NextViewCommand.RaiseCanExecuteChanged();
                    PreviousViewCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private ApplicationNextViewCommand _nextViewCommand;

        public ApplicationNextViewCommand NextViewCommand => _nextViewCommand ?? (_nextViewCommand = new ApplicationNextViewCommand(this));

        private ApplicationPreviousViewCommand _previousViewCommand;

        public ApplicationPreviousViewCommand PreviousViewCommand => _previousViewCommand ?? (_previousViewCommand = new ApplicationPreviousViewCommand(this));

        public ApplicationViewModel()
        {
            _content = new WelcomeScreenViewModel();
        }
    }
}
