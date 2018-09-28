using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AdonisUI.Controls
{
    public class ValidationErrorIndicator
        : Control
    {
        static ValidationErrorIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValidationErrorIndicator), new FrameworkPropertyMetadata(typeof(ValidationErrorIndicator)));
        }

        public FrameworkElement ValidatedElement
        {
            get { return (FrameworkElement)GetValue(ValidatedElementProperty); }
            set { SetValue(ValidatedElementProperty, value); }
        }

        public bool IsValidatedElementFocused
        {
            get { return (bool)GetValue(IsValidatedElementFocusedProperty); }
            set { SetValue(IsValidatedElementFocusedProperty, value); }
        }

        public bool IsPopupVisibleOnFocus
        {
            get { return (bool)GetValue(IsPopupVisibleOnFocusProperty); }
            set { SetValue(IsPopupVisibleOnFocusProperty, value); }
        }

        public bool IsPopupVisibleOnMouseOver
        {
            get { return (bool)GetValue(IsPopupVisibleOnMouseOverProperty); }
            set { SetValue(IsPopupVisibleOnMouseOverProperty, value); }
        }

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public static readonly DependencyProperty ValidatedElementProperty = DependencyProperty.Register("ValidatedElement", typeof(FrameworkElement), typeof(ValidationErrorIndicator), new PropertyMetadata(null));

        public static readonly DependencyProperty IsValidatedElementFocusedProperty = DependencyProperty.Register("IsValidatedElementFocused", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(false));

        public static readonly DependencyProperty IsPopupVisibleOnFocusProperty = DependencyProperty.Register("IsPopupVisibleOnFocus", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(true));

        public static readonly DependencyProperty IsPopupVisibleOnMouseOverProperty = DependencyProperty.Register("IsPopupVisibleOnMouseOver", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(true));

        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(ValidationErrorIndicator), new PropertyMetadata(20.0));

        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(ValidationErrorIndicator), new PropertyMetadata(20.0));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ToggleButton alertToggleButton = GetTemplateChild("AlertToggleButton") as ToggleButton;
            alertToggleButton.Unchecked += AlertToggleButton_OnUnchecked;

            FrameworkElement adornerContent = GetTemplateChild("AdornerContent") as FrameworkElement;
            adornerContent.MouseDown += (s, args) => IsPopupVisibleOnFocus = false;
        }

        private void AlertToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            bool wasPopupVisibleValue = IsPopupVisibleOnMouseOver;

            IsPopupVisibleOnMouseOver = false;

            (sender as ToggleButton).Checked += AlertToggleButton_OnChecked(wasPopupVisibleValue);
            (sender as ToggleButton).MouseLeave += AlertToggleButton_OnMouseLeave(wasPopupVisibleValue);
        }

        private MouseEventHandler AlertToggleButton_OnMouseLeave(bool wasPopupVisibleValue)
        {
            return (s, args) => ResetPopupVisibility(s as ToggleButton, wasPopupVisibleValue);
        }

        private RoutedEventHandler AlertToggleButton_OnChecked(bool wasPopupVisibleValue)
        {
            return (s, args) => ResetPopupVisibility(s as ToggleButton, wasPopupVisibleValue);
        }

        private void ResetPopupVisibility(ToggleButton alertToggleButton, bool wasPopupVisibleValue)
        {
            IsPopupVisibleOnMouseOver = wasPopupVisibleValue;

            (alertToggleButton as ToggleButton).Unchecked -= AlertToggleButton_OnChecked(wasPopupVisibleValue);
            (alertToggleButton as ToggleButton).MouseLeave -= AlertToggleButton_OnMouseLeave(wasPopupVisibleValue);
        }
    }
}
