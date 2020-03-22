using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class Issue26ScenarioViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Issue 26";

        public ApplicationNavigationGroup Group => ApplicationNavigationGroup.IssueScenarios;

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
