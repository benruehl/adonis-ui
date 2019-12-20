using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdonisUI.Controls;
using AdonisUI.Demo.ViewModels;
using IssueDialog = AdonisUI.Demo.Views.Issues.IssueDialog;

namespace AdonisUI.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        public MainWindow()
        {
            DataContext = new ApplicationViewModel();
            InitializeComponent();
        }

        private bool _isDark;

        public bool IsAdonisUiIncluded
        {
            get => (bool)GetValue(IsAdonisUiIncludedProperty);
            set => SetValue(IsAdonisUiIncludedProperty, value);
        }

        public static readonly DependencyProperty IsAdonisUiIncludedProperty = DependencyProperty.Register("IsAdonisUiIncluded", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));

        private void ToggleAdonisResources(object sender, RoutedEventArgs e)
        {
            if (IsAdonisUiIncluded)
            {
                ResourceLocator.RemoveAdonisResources(Application.Current.Resources);
            }
            else
            {
                ResourceLocator.AddAdonisResources(Application.Current.Resources);
            }

            IsAdonisUiIncluded = !IsAdonisUiIncluded;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            ResourceLocator.SetColorScheme(Application.Current.Resources, _isDark ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);

            _isDark = !_isDark;
        }

        private void OpenIssueDialog(object sender, RoutedEventArgs e)
        {
            Window issueDialog = new IssueDialog
            {
                DataContext = new IssueDialogViewModel()
            };

            issueDialog.ShowDialog();
        }
    }
}
