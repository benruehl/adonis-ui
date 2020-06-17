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
using AdonisUI.Demo.Services;

namespace AdonisUI.Demo.ViewModels
{
    class ApplicationViewModel
        : ViewModel
    {
        private readonly ObservableCollection<IApplicationContentView> _pages;

        public ReadOnlyObservableCollection<IApplicationContentView> Pages { get; }

        public ICollectionView PagesCollectionView { get; }
        
        public ICollectionView PagesInSelectedGroupCollectionView { get; }

        private IApplicationContentView _selectedPage;

        public IApplicationContentView SelectedPage
        {
            get => _selectedPage;
            set
            {
                value ??= Pages.FirstOrDefault();

                if (value != null && !value.IsLoading)
                {
                    Task.Run(() =>
                    {
                        value.IsLoading = true;
                        value.Init();
                    }).ContinueWith((task) => value.IsLoading = false);
                }

                SetProperty(ref _selectedPage, value);

                PagesInSelectedGroupCollectionView.Refresh();
                RaisePropertyChanged(nameof(SelectedNavigationGroup));
            }
        }

        private readonly ObservableCollection<ApplicationNavigationGroup> _navigationGroups;

        public ReadOnlyObservableCollection<ApplicationNavigationGroup> NavigationGroups { get; }

        public ICollectionView NavigationGroupsCollectionView { get; }

        public ApplicationNavigationGroup SelectedNavigationGroup
        {
            get => _selectedPage.Group;
            set
            {
                SelectedPage = Pages.FirstOrDefault(p => p.Group == value);
                PagesInSelectedGroupCollectionView.Refresh();
            }
        }

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        private bool _isDeveloperMode;

        public bool IsDeveloperMode
        {
            get => _isDeveloperMode;
            set
            {
                SetProperty(ref _isDeveloperMode, value);
                PagesCollectionView.Refresh();
                NavigationGroupsCollectionView.Refresh();
            }
        }

        public ApplicationViewModel()
        {
            _pages = new ObservableCollection<IApplicationContentView>(CreateAllPages());
            Pages = new ReadOnlyObservableCollection<IApplicationContentView>(_pages);
            PagesCollectionView = CollectionViewSource.GetDefaultView(Pages);
            PagesCollectionView.Filter = FilterPages;
            PagesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(IApplicationContentView.Group)));
            PagesInSelectedGroupCollectionView = new CollectionViewSource { Source = Pages }.View;
            PagesInSelectedGroupCollectionView.Filter = FilterPagesInSelectedGroup;
            SelectedPage = Pages.FirstOrDefault();

            _navigationGroups = new ObservableCollection<ApplicationNavigationGroup>(Enum.GetValues(typeof(ApplicationNavigationGroup)).Cast<ApplicationNavigationGroup>());
            NavigationGroups = new ReadOnlyObservableCollection<ApplicationNavigationGroup>(_navigationGroups);
            NavigationGroupsCollectionView = CollectionViewSource.GetDefaultView(NavigationGroups);
            NavigationGroupsCollectionView.Filter = FilterNavigationGroups;
        }

        private IEnumerable<IApplicationContentView> CreateAllPages()
        {
            yield return new OverviewSampleViewModel();
            yield return new CollectionSampleViewModel(new ItemGenerator());
            yield return new LayerSimpleSampleViewModel();
            yield return new LayerSampleViewModel();
            yield return new ValidationSampleViewModel();
            yield return new Issue5ScenarioViewModel();
            yield return new Issue23ScenarioViewModel();
            yield return new Issue26ScenarioViewModel();
            yield return new Issue101ScenarioViewModel();
            yield return new IssueRippleContentInvisibleScenarioViewModel();
            yield return new ColorReferenceViewModel();
            yield return new ButtonReferenceViewModel();
            yield return new TextInputReferenceViewModel();
            yield return new ToggleReferenceViewModel();
            yield return new ListBoxReferenceViewModel();
            yield return new ScrollBarReferenceViewModel(new ItemGenerator());
            yield return new LoadingReferenceViewModel();
            yield return new MessageBoxReferenceViewModel();
        }

        private bool FilterPages(object item)
        {
            var page = (IApplicationContentView)item;

            if (!IsDeveloperMode)
                return page.Group != ApplicationNavigationGroup.IssueScenarios;

            return true;
        }

        private bool FilterPagesInSelectedGroup(object item)
        {
            var page = (IApplicationContentView)item;

            if (SelectedPage == null)
                return false;

            return page.Group == SelectedPage.Group && FilterPages(page);
        }

        private bool FilterNavigationGroups(object item)
        {
            var group = (ApplicationNavigationGroup)item;

            if (!IsDeveloperMode)
                return group != ApplicationNavigationGroup.IssueScenarios;

            return true;
        }
    }
}
