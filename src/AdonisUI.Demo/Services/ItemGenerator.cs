using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.ViewModels;

namespace AdonisUI.Demo.Services
{
    class ItemGenerator
        : IItemGenerator
    {
        public IEnumerable<ItemViewModel> CreateDummyItems(int count, double childCreationProbability, Random random)
        {
            for (int i = 0; i < count; i++)
            {
                var item = new ItemViewModel
                {
                    Name = CreateItemName(i + 1),
                    Weight = random.NextDouble(),
                };

                if (random.NextDouble() <= childCreationProbability)
                {
                    foreach (ItemViewModel child in CreateDummyItems(random.Next(count), childCreationProbability, random))
                    {
                        item.AddChild(child);
                    }
                }

                yield return item;
            }
        }

        public ItemViewModel CreateItemInItems(ICollection<ItemViewModel> existingItems)
        {
            var newItem = new ItemViewModel
            {
                Name = GetNextUniqueItemName(existingItems),
                Weight = new Random().NextDouble(),
            };

            existingItems.Add(newItem);
            return newItem;
        }

        private string GetNextUniqueItemName(ICollection<ItemViewModel> existingItems)
        {
            int iteration = 1;
            string itemName = CreateItemName(iteration);

            while (existingItems.Any(item => item.Name == itemName))
            {
                iteration++;
                itemName = CreateItemName(iteration);
            }

            return itemName;
        }

        private string CreateItemName(int itemId)
        {
            return $"Item {itemId}";
        }
    }
}
