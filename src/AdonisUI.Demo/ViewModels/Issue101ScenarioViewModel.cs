using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class Issue101ScenarioViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Issue 101";

        public ApplicationNavigationGroup Group => ApplicationNavigationGroup.IssueScenarios;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isIndeterminate;

        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set => SetProperty(ref _isIndeterminate, value);
        }

        public void Init()
        {
        }
    }
}
