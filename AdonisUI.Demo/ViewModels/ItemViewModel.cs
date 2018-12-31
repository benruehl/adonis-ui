using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class ItemViewModel
        : ViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;

                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        private double _weight;

        public double Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;

                    RaisePropertyChanged(nameof(Weight));
                }
            }
        }

        private readonly ObservableCollection<ItemViewModel> _children = new ObservableCollection<ItemViewModel>();

        public ReadOnlyObservableCollection<ItemViewModel> Children { get; set; }

        public ItemViewModel()
        {
            Children = new ReadOnlyObservableCollection<ItemViewModel>(_children);
        }

        public void AddChild(ItemViewModel child)
        {
            _children.Add(child);
        }
    }
}
