using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class ColorReferenceViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Colors";

        public ApplicationNavigationGroup Group => ApplicationNavigationGroup.Reference;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private readonly ObservableCollection<BrushResourceViewModel> _brushes;

        public ReadOnlyObservableCollection<BrushResourceViewModel> Brushes { get; }

        public ColorReferenceViewModel()
        {
            _brushes = new ObservableCollection<BrushResourceViewModel>();
            Brushes = new ReadOnlyObservableCollection<BrushResourceViewModel>(_brushes);
        }

        public void Init()
        {
            Dispatch(InitBrushes);
        }

        private void InitBrushes()
        {
            _brushes.Clear();

            IEnumerable<ComponentResourceKey> brushKeys = typeof(Brushes).GetProperties().Select(p => (ComponentResourceKey)p.GetValue(null, null));

            foreach (var brushKey in brushKeys)
            {
                var brushResource = new BrushResourceViewModel
                {
                    ResourceKey = brushKey,
                };

                brushResource.IsBasedOnColorResource = typeof(Colors).GetProperty(brushResource.ShortName + "Color") != null;

                _brushes.Add(brushResource);
            }
        }

        private void Dispatch(Action action)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, action);
        }
    }
}
