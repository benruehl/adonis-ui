using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdonisUI.Demo.Views
{
    /// <summary>
    /// Interaction logic for ControlBrushesBreakdown.xaml
    /// </summary>
    public partial class ControlBrushesBreakdown : UserControl
    {
        public double MouseX
        {
            get => (double)GetValue(MouseXProperty);
            set => SetValue(MouseXProperty, value);
        }

        public double MouseY
        {
            get => (double)GetValue(MouseYProperty);
            set => SetValue(MouseYProperty, value);
        }

        public static readonly DependencyProperty MouseXProperty = DependencyProperty.Register("MouseX", typeof(double), typeof(ControlBrushesBreakdown), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MouseYProperty = DependencyProperty.Register("MouseY", typeof(double), typeof(ControlBrushesBreakdown), new PropertyMetadata(0.0));

        public ControlBrushesBreakdown()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            var canvas = (FrameworkElement)Template.FindName("Canvas", this);
            canvas.MouseMove += Container_OnMouseMove;
        }

        private void Container_OnMouseMove(object sender, MouseEventArgs e)
        {
            var canvas = (Canvas)sender;
            Point pos = e.GetPosition(canvas);
            MouseX = pos.X;
            MouseY = pos.Y;
        }
    }
}
