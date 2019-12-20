using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdonisUI.Demo.Framework;
using AdonisUI.Demo.ViewModels;

namespace AdonisUI.Demo.Commands
{
    class CollectionDemoAddItemCommand
        : ViewModelCommand<CollectionDemoViewModel>
    {
        public CollectionDemoAddItemCommand(CollectionDemoViewModel contextViewModel) : base(contextViewModel)
        {
        }

        public override void Execute(CollectionDemoViewModel contextViewModel, object parameter)
        {
            contextViewModel.CreateItemInItems();
        }
    }
}
