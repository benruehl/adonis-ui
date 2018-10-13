using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class LayerDemoViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Layers";

        public bool HasPreviousView => true;

        public bool HasNextView => true;

        public IApplicationContentView GetPreviousView()
        {
            return new WelcomeScreenViewModel();
        }

        public IApplicationContentView GetNextView()
        {
            return new ValidationDemoViewModel();
        }
    }
}
