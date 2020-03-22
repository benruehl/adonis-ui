using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class IssueDialogViewModel
        : ViewModel
    {
        private readonly ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        public ReadOnlyObservableCollection<ItemViewModel> Items { get; set; }

        public IssueDialogViewModel()
        {
            Items = new ReadOnlyObservableCollection<ItemViewModel>(_items);

            AddDummyItems(3);
        }

        private void AddDummyItems(int count)
        {
            foreach (ItemViewModel item in CreateDummyItems(count))
            {
                _items.Add(item);
            }
        }

        private IEnumerable<ItemViewModel> CreateDummyItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = new ItemViewModel
                {
                    Name = $"Item {i + 1}",
                };

                yield return item;
            }
        }
    }
}
