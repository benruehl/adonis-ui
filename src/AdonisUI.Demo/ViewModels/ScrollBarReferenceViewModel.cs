using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using AdonisUI.Demo.Framework;
using AdonisUI.Demo.Services;

namespace AdonisUI.Demo.ViewModels
{
    class ScrollBarReferenceViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Scroll Bars";

        public ApplicationNavigationGroup Group => ApplicationNavigationGroup.Reference;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private readonly ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        public ReadOnlyObservableCollection<ItemViewModel> Items { get; set; }

        private readonly IItemGenerator _itemGenerator;

        public ScrollBarReferenceViewModel(IItemGenerator itemGenerator)
        {
            _itemGenerator = itemGenerator;
            Items = new ReadOnlyObservableCollection<ItemViewModel>(_items);
        }

        public void Init()
        {
            Dispatch(() => _items.Clear());
            AddDummyItems(25);
        }

        private void AddDummyItems(int count)
        {
            foreach (ItemViewModel item in _itemGenerator.CreateDummyItems(count, 0, new Random()))
            {
                Dispatch(() => _items.Add(item));
            }
        }

        private void Dispatch(Action action)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, action);
        }
    }
}
