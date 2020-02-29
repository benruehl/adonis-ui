using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AdonisUI.Demo.Commands;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class ApplicationViewModel
        : ViewModel
    {
        private readonly ObservableCollection<IApplicationContentView> _pages;

        public ReadOnlyObservableCollection<IApplicationContentView> Pages { get; }

        public ICollectionView PagesCollectionView { get; }

        private IApplicationContentView _selectedPage;

        public IApplicationContentView SelectedPage
        {
            get => _selectedPage;
            set
            {
                if (value != null && !value.IsLoading)
                {
                    Task.Run(() =>
                    {
                        value.IsLoading = true;
                        value.Init();
                    }).ContinueWith((task) => value.IsLoading = false);
                }

                SetProperty(ref _selectedPage, value);
            }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private bool _isDeveloperMode;

        public bool IsDeveloperMode
        {
            get => _isDeveloperMode;
            set
            {
                SetProperty(ref _isDeveloperMode, value);
                PagesCollectionView.Refresh();
            }
        }

        public ApplicationViewModel()
        {
            _pages = new ObservableCollection<IApplicationContentView>(CreateAllPages());
            Pages = new ReadOnlyObservableCollection<IApplicationContentView>(_pages);
            PagesCollectionView = CollectionViewSource.GetDefaultView(Pages);
            PagesCollectionView.Filter = FilterPages;
            PagesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(IApplicationContentView.Group)));
            SelectedPage = Pages.FirstOrDefault();
            IsEnabled = true;
        }

        private IEnumerable<IApplicationContentView> CreateAllPages()
        {
            yield return new OverviewSampleViewModel();
            yield return new CollectionSampleViewModel();
            yield return new LayerSimpleSampleViewModel();
            yield return new LayerSampleViewModel();
            yield return new ValidationSampleViewModel();
            yield return new MessageBoxSampleViewModel();
            yield return new Issue5ScenarioViewModel();
            yield return new Issue23ScenarioViewModel();
            yield return new Issue26ScenarioViewModel();
            yield return new IssueRippleContentInvisibleScenarioViewModel();
        }

        private bool FilterPages(object item)
        {
            var page = (IApplicationContentView)item;

            if (!IsDeveloperMode)
                return page.Group != IApplicationContentView.NavigationGroup.IssueScenarios;

            return true;
        }
    }
}
