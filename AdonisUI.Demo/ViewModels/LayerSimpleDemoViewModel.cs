using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class LayerSimpleDemoViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Layers (Simple)";

        public bool HasPreviousView => true;

        public bool HasNextView => true;

        public IApplicationContentView GetPreviousView()
        {
            return new CollectionDemoViewModel();
        }

        public IApplicationContentView GetNextView()
        {
            return new LayerDemoViewModel();
        }
    }
}
