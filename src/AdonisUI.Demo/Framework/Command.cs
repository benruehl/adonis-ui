using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AdonisUI.Demo.Framework
{
    abstract class Command
        : ICommand
    {
        public abstract void Execute(object parameter);

        public abstract bool CanExecute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
