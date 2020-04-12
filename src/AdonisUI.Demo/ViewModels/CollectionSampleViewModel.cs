using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using AdonisUI.Demo.Commands;
using AdonisUI.Demo.Framework;
using AdonisUI.Demo.Services;

namespace AdonisUI.Demo.ViewModels
{
    class CollectionSampleViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Collections";

        public ApplicationNavigationGroup Group => ApplicationNavigationGroup.Samples;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private readonly ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        public ReadOnlyObservableCollection<ItemViewModel> Items { get; set; }

        private readonly IItemGenerator _itemGenerator;

        public CollectionSampleViewModel(IItemGenerator itemGenerator)
        {
            _itemGenerator = itemGenerator;
            Items = new ReadOnlyObservableCollection<ItemViewModel>(_items);
        }

        public void Init()
        {
            Dispatch(() => _items.Clear());
            AddDummyItems(50);
        }

        private void AddDummyItems(int count)
        {
            foreach (ItemViewModel item in _itemGenerator.CreateDummyItems(count, 0.25, new Random()))
            {
                Dispatch(() => _items.Add(item));
            }
        }

        public ItemViewModel CreateItemInItems()
        {
            return _itemGenerator.CreateItemInItems(_items);
        }

        private void Dispatch(Action action)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, action);
        }

        private CollectionSampleAddItemCommand _addItemCommand;

        public CollectionSampleAddItemCommand AddItemCommand => _addItemCommand ?? (_addItemCommand = new CollectionSampleAddItemCommand(this));
    }
}
