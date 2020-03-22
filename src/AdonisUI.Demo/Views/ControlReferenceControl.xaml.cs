using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdonisUI.Demo.Views
{
    /// <summary>
    /// Interaction logic for ControlReferenceControl.xaml
    /// </summary>
    [ContentProperty(nameof(ReferenceContent))]
    public partial class ControlReferenceControl : UserControl
    {
        public ControlReferenceControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ControlReferenceControl), new PropertyMetadata(null));

        public object ReferenceContent
        {
            get => (object)GetValue(ReferenceContentProperty);
            set => SetValue(ReferenceContentProperty, value);
        }

        public static readonly DependencyProperty ReferenceContentProperty = DependencyProperty.Register("ReferenceContent", typeof(object), typeof(ControlReferenceControl), new PropertyMetadata(null));
    }
}
