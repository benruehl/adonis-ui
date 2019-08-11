using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;
using AdonisUI.Helpers;
using Point = System.Windows.Point;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Window with custom chrome supporting theming of non-client areas
    /// </summary>
    [TemplatePart(Name = PART_DragMoveThumb, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_Icon, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_MinimizeButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_MaximizeRestoreButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    public class AdonisWindow
        : Window
    {
        private const string PART_DragMoveThumb = "PART_DragMoveThumb";
        private const string PART_Icon = "PART_Icon";
        private const string PART_MinimizeButton = "PART_MinimizeButton";
        private const string PART_MaximizeRestoreButton = "PART_MaximizeRestoreButton";
        private const string PART_CloseButton = "PART_CloseButton";

        public FrameworkElement DragMoveThumb { get; protected set; }

        public FrameworkElement Icon { get; protected set; }

        public Button MinimizeButton { get; protected set; }

        public Button MaximizeRestoreButton { get; protected set; }

        public Button CloseButton { get; protected set; }

        /// <summary>
        /// Gets or sets the visibility of the icon component of the window.
        /// </summary>
        public Visibility IconVisibility
        {
            get => (Visibility)GetValue(IconVisibilityProperty);
            set => SetValue(IconVisibilityProperty, value);
        }

        /// <summary>
        /// Gets or sets the window's icon as <see cref="ImageSource">ImageSource</see>.
        /// When the <see cref="Window.IconProperty">IconProperty</see> property changes, this is updated accordingly.
        /// </summary>
        protected internal ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the content of the window's title bar
        /// between the title and the window buttons.
        /// </summary>
        public object TitleBarContent
        {
            get => (object)GetValue(TitleBarContentProperty);
            set => SetValue(TitleBarContentProperty, value);
        }

        /// <summary>
        /// Gets the size of the display overlapping area when the window is maximized.
        /// </summary>
        protected internal Thickness MaximizeBorderThickness
        {
            get => (Thickness)GetValue(MaximizeBorderThicknessProperty);
            private set => SetValue(MaximizeBorderThicknessPropertyKey, value);
        }

        public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(AdonisWindow), new PropertyMetadata(Visibility.Visible));

        protected internal static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(AdonisWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register("TitleBarContent", typeof(object), typeof(AdonisWindow), new PropertyMetadata(null));

        protected internal static readonly DependencyPropertyKey MaximizeBorderThicknessPropertyKey = DependencyProperty.RegisterReadOnly("MaximizeBorderThickness", typeof(Thickness), typeof(AdonisWindow), new PropertyMetadata(new Thickness()));

        protected internal static readonly DependencyProperty MaximizeBorderThicknessProperty = MaximizeBorderThicknessPropertyKey.DependencyProperty;

        static AdonisWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdonisWindow), new FrameworkPropertyMetadata(typeof(AdonisWindow)));
            IconProperty.OverrideMetadata(typeof(AdonisWindow), new FrameworkPropertyMetadata(OnIconPropertyChanged));
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is AdonisWindow sourceWindow))
                return;

            string newIcon = e.NewValue.ToString();

            sourceWindow.IconSource = String.IsNullOrEmpty(newIcon) ? null : new BitmapImage(new Uri(newIcon));
        }

        /// <inheritdoc/>
        public AdonisWindow()
        {
            IconSource = GetApplicationIcon();
            MaximizeBorderThickness = GetSystemMaximizeBorderThickness();
        }

        private BitmapSource GetApplicationIcon()
        {
            Icon appIcon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly()?.ManifestModule.Name);

            if (appIcon == null)
                return null;

            return Imaging.CreateBitmapSourceFromHIcon(appIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private Thickness GetSystemMaximizeBorderThickness()
        {
            Thickness frameThickness = SystemParameters.WindowNonClientFrameThickness;
            Thickness resizeBorderThickness = SystemParameters.WindowResizeBorderThickness;

            return new Thickness(
                frameThickness.Left + resizeBorderThickness.Left - 1,
                frameThickness.Top + resizeBorderThickness.Top - SystemParameters.CaptionHeight - 1,
                frameThickness.Right + resizeBorderThickness.Right - 1,
                frameThickness.Bottom + resizeBorderThickness.Bottom - 1);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DragMoveThumb = GetTemplateChild(PART_DragMoveThumb) as FrameworkElement;
            Icon = GetTemplateChild(PART_Icon) as FrameworkElement;
            MinimizeButton = GetTemplateChild(PART_MinimizeButton) as Button;
            MaximizeRestoreButton = GetTemplateChild(PART_MaximizeRestoreButton) as Button;
            CloseButton = GetTemplateChild(PART_CloseButton) as Button;

            if (DragMoveThumb != null)
                InitDragMoveThumb(DragMoveThumb);
            if (Icon != null)
                InitIcon(Icon);
            if (MinimizeButton != null)
                InitMinimizeButton(MinimizeButton);
            if (MaximizeRestoreButton != null)
                InitMaximizeRestoreButton(MaximizeRestoreButton);
            if (CloseButton != null)
                InitCloseButton(CloseButton);
        }

        /// <summary>
        /// Initializes functionality of the drag/move thumb component of the window's title bar.
        /// </summary>
        /// <param name="dragMoveThumb">The drag/move thumb component of the window</param>
        protected virtual void InitDragMoveThumb(FrameworkElement dragMoveThumb)
        {
            dragMoveThumb.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    if (WindowState == WindowState.Maximized)
                        dragMoveThumb.MouseMove += RestoreOnMouseMove;

                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                        DragMove();
                }

                if (e.ClickCount == 2 &&
                    (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip))
                {
                    ToggleWindowState();
                }
            };

            dragMoveThumb.MouseLeftButtonUp += (s, e) => dragMoveThumb.MouseMove -= RestoreOnMouseMove;

            dragMoveThumb.MouseRightButtonUp += (s, e) => OpenSystemContextMenu(e.GetPosition(this));
        }

        /// <summary>
        /// Initializes functionality of the minimize button of the window's title bar.
        /// </summary>
        /// <param name="minimizeButton">The minimize button of the window</param>
        protected virtual void InitMinimizeButton(Button minimizeButton)
        {
            minimizeButton.Click += MinimizeClick;
        }

        /// <summary>
        /// Initializes functionality of the maximize/restore button of the window's title bar.
        /// </summary>
        /// <param name="maximizeRestoreButton">The maximize/restore button of the window</param>
        protected virtual void InitMaximizeRestoreButton(Button maximizeRestoreButton)
        {
            maximizeRestoreButton.Click += MaximizeRestoreClick;
        }

        /// <summary>
        /// Initializes functionality of the close button of the window's title bar.
        /// </summary>
        /// <param name="closeButton">The close button of the window</param>
        protected virtual void InitCloseButton(Button closeButton)
        {
            closeButton.Click += CloseClick;
        }

        /// <summary>
        /// Initializes functionality of the icon component of the window's title bar.
        /// </summary>
        /// <param name="icon">The icon component of the window</param>
        protected virtual void InitIcon(FrameworkElement icon)
        {
            icon.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ClickCount == 2)
                {
                    Close();
                    return;
                }

                var anchorElement = DragMoveThumb ?? Icon;
                var menuPosition = anchorElement.TranslatePoint(new Point(0, anchorElement.ActualHeight), this);
                OpenSystemContextMenu(menuPosition);
            };
        }

        /// <summary>
        /// Handles the close button's click event.
        /// </summary>
        protected virtual void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the maximize/restore button's click event.
        /// </summary>
        protected virtual void MaximizeRestoreClick(object sender, RoutedEventArgs e)
        {
            ToggleWindowState();
        }

        /// <summary>
        /// Handles the minimize button's click event.
        /// </summary>
        protected virtual void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Sets the <see cref="Window.WindowState"/> to <see cref="WindowState.Maximized"/>
        /// if it is currently at <see cref="WindowState.Normal"/> or else to <see cref="WindowState.Normal"/>.
        /// </summary>
        protected virtual void ToggleWindowState()
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void RestoreOnMouseMove(object sender, MouseEventArgs e)
        {
            var dragMoveThumb = sender as FrameworkElement;
            if (dragMoveThumb == null)
                return;

            dragMoveThumb.MouseMove -= RestoreOnMouseMove;

            Point positionInWindow = e.MouseDevice.GetPosition(this);
            Point positionOnScreen = PointToScreen(positionInWindow);
            Screen currentScreen = Screen.FromPoint(positionOnScreen);

            double restoreLeft = positionOnScreen.X - (RestoreBounds.Width * 0.5);
            double restoreTop = positionOnScreen.Y - (positionInWindow.Y - MaximizeBorderThickness.Top);

            if (restoreLeft < currentScreen.Bounds.Left)
                restoreLeft = currentScreen.Bounds.Left;
            else if (restoreLeft + RestoreBounds.Width > currentScreen.Bounds.Right)
                restoreLeft = currentScreen.Bounds.Right - RestoreBounds.Width;

            Left = restoreLeft;
            Top = restoreTop;
            WindowState = WindowState.Normal;

            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Displays the system's native window context menu at the given position.
        /// </summary>
        /// <param name="positionInWindow">Coordinate of top left corner of the context menu relative to the window</param>
        protected virtual void OpenSystemContextMenu(Point positionInWindow)
        {
            SystemContextMenu.OpenSystemContextMenu(this, positionInWindow);
        }
    }
}
