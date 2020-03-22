using System;
using System.Collections.Generic;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class ButtonReferenceViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Buttons";

        public ApplicationNavigationGroup Group => ApplicationNavigationGroup.Reference;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public void Init()
        {
        }
    }
}
