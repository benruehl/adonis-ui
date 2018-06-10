using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaktionslogik für IssueControl.xaml
    /// </summary>
    public partial class IssueControl : UserControl
    {
        public IssueControl()
        {
            InitializeComponent();
        }


        public string IssueTitle
        {
            get { return (string)GetValue(IssueTitleProperty); }
            set { SetValue(IssueTitleProperty, value); }
        }

        public static readonly DependencyProperty IssueTitleProperty = DependencyProperty.Register("IssueTitle", typeof(string), typeof(IssueControl), new PropertyMetadata(null));



        public string IssueDescription
        {
            get { return (string)GetValue(IssueDescriptionProperty); }
            set { SetValue(IssueDescriptionProperty, value); }
        }

        public static readonly DependencyProperty IssueDescriptionProperty = DependencyProperty.Register("IssueDescription", typeof(string), typeof(IssueControl), new PropertyMetadata(null));



        public string StepsToReproduce
        {
            get { return (string)GetValue(StepsToReproduceProperty); }
            set { SetValue(StepsToReproduceProperty, value); }
        }

        public static readonly DependencyProperty StepsToReproduceProperty = DependencyProperty.Register("StepsToReproduce", typeof(string), typeof(IssueControl), new PropertyMetadata(null));



        public string IssueLink
        {
            get { return (string)GetValue(IssueLinkProperty); }
            set { SetValue(IssueLinkProperty, value); }
        }

        public static readonly DependencyProperty IssueLinkProperty = DependencyProperty.Register("IssueLink", typeof(string), typeof(IssueControl), new PropertyMetadata(null));



        public DependencyObject IssueContent
        {
            get { return (DependencyObject)GetValue(IssueContentProperty); }
            set { SetValue(IssueContentProperty, value); }
        }

        public static readonly DependencyProperty IssueContentProperty = DependencyProperty.Register("IssueContent", typeof(DependencyObject), typeof(IssueControl), new PropertyMetadata(null));

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
