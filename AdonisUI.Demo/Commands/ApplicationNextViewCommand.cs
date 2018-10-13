using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;
using AdonisUI.Demo.ViewModels;

namespace AdonisUI.Demo.Commands
{
    class ApplicationNextViewCommand
        : ViewModelCommand<ApplicationViewModel>
    {
        public ApplicationNextViewCommand(ApplicationViewModel contextViewModel) : base(contextViewModel)
        {
        }

        public override void Execute(ApplicationViewModel contextViewModel, object parameter)
        {
            if (!(contextViewModel.Content is IApplicationContentView currentView))
                return;

            contextViewModel.Content = currentView.GetNextView() as ViewModel;
        }

        public override bool CanExecute(ApplicationViewModel contextViewModel, object parameter)
        {
            if (!(contextViewModel.Content is IApplicationContentView currentView))
                return false;

            return currentView.HasNextView;
        }
    }
}
