using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdonisUI.Demo.ViewModels
{
    interface IApplicationContentView
    {
        string Name { get; }

        ApplicationNavigationGroup Group { get; }

        bool IsLoading { get; set; }

        void Init();
    }
}
