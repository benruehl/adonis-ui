using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdonisUI.Demo.ViewModels
{
    interface IApplicationContentView
    {
        string Name { get; }

        bool HasPreviousView { get; }

        bool HasNextView { get; }

        IApplicationContentView GetPreviousView();

        IApplicationContentView GetNextView();
    }
}
