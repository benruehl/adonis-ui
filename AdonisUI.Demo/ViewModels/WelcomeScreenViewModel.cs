using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class WelcomeScreenViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Welcome";

        public bool HasPreviousView => false;

        public bool HasNextView => true;

        public IApplicationContentView GetPreviousView()
        {
            throw new InvalidOperationException();
        }

        public IApplicationContentView GetNextView()
        {
            return new LayerDemoViewModel();
        }
    }
}
