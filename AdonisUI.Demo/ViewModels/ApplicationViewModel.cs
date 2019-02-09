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

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    RaisePropertyChanged(nameof(IsEnabled));
                }
            }
        }

        private ApplicationNextViewCommand _nextViewCommand;

        public ApplicationNextViewCommand NextViewCommand => _nextViewCommand ?? (_nextViewCommand = new ApplicationNextViewCommand(this));

        private ApplicationPreviousViewCommand _previousViewCommand;

        public ApplicationPreviousViewCommand PreviousViewCommand => _previousViewCommand ?? (_previousViewCommand = new ApplicationPreviousViewCommand(this));

        private ApplicationToggleIsEnabledCommand _toggleIsEnabledCommand;

        public ApplicationToggleIsEnabledCommand ToggleIsEnabledCommand => _toggleIsEnabledCommand ?? (_toggleIsEnabledCommand = new ApplicationToggleIsEnabledCommand(this));

        public ApplicationViewModel()
        {
            _content = new WelcomeScreenViewModel();
            IsEnabled = true;
        }
    }
}
