using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using AdonisUI.Demo.Commands;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class CollectionSampleViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Collections";

        public IApplicationContentView.NavigationGroup Group => IApplicationContentView.NavigationGroup.Samples;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private readonly ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        public ReadOnlyObservableCollection<ItemViewModel> Items { get; set; }

        public CollectionSampleViewModel()
        {
            Items = new ReadOnlyObservableCollection<ItemViewModel>(_items);
        }

        public void Init()
        {
            Dispatch(() => _items.Clear());
            AddDummyItems(50);
        }

        private void AddDummyItems(int count)
        {
            foreach (ItemViewModel item in CreateDummyItems(count, 0.25, new Random()))
            {
                Dispatch(() => _items.Add(item));
            }
        }

        private IEnumerable<ItemViewModel> CreateDummyItems(int count, double childCreationProbability, Random random)
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

        public ItemViewModel CreateItemInItems()
        {
            var newItem = new ItemViewModel
            {
                Name = GetNextUniqueItemName(),
                Weight = new Random().NextDouble(),
            };

            _items.Add(newItem);
            return newItem;
        }

        private string GetNextUniqueItemName()
        {
            int iteration = 1;
            string itemName = CreateItemName(iteration);

            while (_items.Any(item => item.Name == itemName))
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

        private void Dispatch(Action action)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, action);
        }

        private CollectionSampleAddItemCommand _addItemCommand;

        public CollectionSampleAddItemCommand AddItemCommand => _addItemCommand ?? (_addItemCommand = new CollectionSampleAddItemCommand(this));
    }
}
