using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;
using AdonisUI.Demo.ViewModels;

namespace AdonisUI.Demo.Commands
{
    class ApplicationToggleIsEnabledCommand
        : ViewModelCommand<ApplicationViewModel>
    {
        public ApplicationToggleIsEnabledCommand(ApplicationViewModel contextViewModel) : base(contextViewModel)
        {
        }

        public override void Execute(ApplicationViewModel contextViewModel, object parameter)
        {
            contextViewModel.IsEnabled = !contextViewModel.IsEnabled;
        }
    }
}
