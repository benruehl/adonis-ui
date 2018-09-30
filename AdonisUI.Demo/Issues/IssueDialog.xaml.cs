using System;
using System.Collections.Generic;
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

namespace AdonisUI.Demo.Issues
{
    /// <summary>
    /// Interaktionslogik für IssueDialog.xaml
    /// </summary>
    public partial class IssueDialog : Window
    {
        private int _currentIssueIndex = 0;

        public int CurrentIssueIndex
        {
            get => _currentIssueIndex;
            set
            {
                _currentIssueIndex = value;
                UpdateCurrentIssueTemplate();
            }
        }

        private readonly List<string> _issueTemplates = new List<string>
        {
            "IssueMissingValidationsTemplate",
            "Issue5Template",
        };

        public IssueDialog()
        {
            InitializeComponent();
            UpdateCurrentIssueTemplate();
        }

        private void NextIssue(object sender, RoutedEventArgs e)
        {
            if (CurrentIssueIndex < _issueTemplates.Count - 1)
                CurrentIssueIndex++;
        }

        private void PreviousIssue(object sender, RoutedEventArgs e)
        {
            if (CurrentIssueIndex > 0)
                CurrentIssueIndex--;
        }

        private void UpdateCurrentIssueTemplate()
        {
            CurrentIssueIndexTextBlock.Text = (CurrentIssueIndex + 1).ToString();
            IssueCountTextBlock.Text = _issueTemplates.Count.ToString();
            NextIssueButton.IsEnabled = CurrentIssueIndex < _issueTemplates.Count - 1;
            PreviousIssueButton.IsEnabled = CurrentIssueIndex > 0;

            IssueContainer.ContentTemplate = FindResource(_issueTemplates[_currentIssueIndex]) as DataTemplate;
        }
    }
}