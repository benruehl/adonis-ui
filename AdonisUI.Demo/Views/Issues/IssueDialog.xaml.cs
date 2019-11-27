using System.Collections.Generic;
using System.Windows;

namespace AdonisUI.Demo.Views.Issues
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
            "Issue5Template",
            "IssueRippleContentInvisibleTemplate",
            "Issue23Template",
            "Issue26Template",
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