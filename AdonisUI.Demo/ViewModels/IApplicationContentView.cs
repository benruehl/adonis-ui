using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdonisUI.Demo.ViewModels
{
    public interface IApplicationContentView
    {
        string Name { get; }

        NavigationGroup Group { get; }

        bool IsLoading { get; set; }

        void Init();

        public enum NavigationGroup
        {
            Samples,
            IssueScenarios,
        }
    }
}
