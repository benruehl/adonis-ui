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

        private enum Theme
        {
            Light,
            Dark,
            DarkEmber
        }

        private Theme _currentTheme;

        private static Uri GetColorScheme(Theme theme)
        {
            switch (theme)
            {
                case Theme.Light: return ResourceLocator.LightColorScheme;
                case Theme.Dark: return ResourceLocator.DarkColorScheme;
                case Theme.DarkEmber: return ResourceLocator.DarkEmberColorScheme;
                default: throw new NotSupportedException("This theme is not supported");
            }
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            Theme newTheme;
            switch (_currentTheme)
            {
                case Theme.Light:
                    newTheme = Theme.Dark;
                    break;
                case Theme.Dark:
                    newTheme = Theme.DarkEmber;
                    break;
                case Theme.DarkEmber:
                    newTheme = Theme.Light;
                    break;
                default: throw new NotSupportedException("This theme is not supported");
            }
            ResourceLocator.SetColorScheme(Application.Current.Resources, GetColorScheme(newTheme), GetColorScheme(_currentTheme));
            _currentTheme = newTheme;
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
