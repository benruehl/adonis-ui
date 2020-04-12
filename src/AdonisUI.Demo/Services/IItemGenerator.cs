using System;
using System.Collections.Generic;
using System.Text;
using AdonisUI.Demo.ViewModels;

namespace AdonisUI.Demo.Services
{
    interface IItemGenerator
    {
        IEnumerable<ItemViewModel> CreateDummyItems(int count, double childCreationProbability, Random random);

        ItemViewModel CreateItemInItems(ICollection<ItemViewModel> existingItems);
    }
}
