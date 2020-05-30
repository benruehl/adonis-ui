---
layout: default
title: Styles and Templates
parent: Guides
---

# Styles and Templates

Adonis UI offers the following resource kinds:

- **Brushes** - accessible via `AdonisUI.Brushes`
- **Colors** - accessible via `AdonisUI.Colors`
- **Dimensions** - accessible via `AdonisUI.Dimensions`
- **Icons** - accessible via `AdonisUI.Icons`
- **Styles** - accessible via `AdonisUI.Styles`
- **Templates** - accessible via `AdonisUI.Templates`

For brushes and colors see [here](colors-and-brushes.md). For dimensions see [here](dimensions.md). In this article, styles, templates and icons are addressed.

## Usage

All resources of Adonis UI including styles, templates and icons can be referenced using *ComponentResourceKeys*.

Example:

```xml
<!-- xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI" -->
<Button Style="{DynamicResource {x:Static adonisUi:Styles.AccentButtonStyle}}"/>
```

Templates and icons are usually of type `DataTemplate`. The easiest way to use them is via a `ContentControl`:

```xml
<!-- xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI" -->
<ContentControl Content="{Binding}"
                ContentTemplate="{DynamicResource {x:Static adonisUi:Templates.LoadingCircle}}"
                Width="32"
                Height="32"
                Focusable="False"/>
```

## Available styles

- **`Styles.AccentButton`** - For buttons that should gain attention (accent color as background)
- **`Styles.AccentToolbarButton`** - For buttons being placed in a toolbar that should gain attention when hovering
- **`Styles.DefaultToAccentToggleButton`** - For toggle buttons switching to accent color when checked
- **`Styles.RippleListBoxItem`** - For list box items that release a ripple effect when clicked
- **`Styles.ToolbarButton`** - For buttons being placed in a toolbar (transparent background until hovering)
- **`Styles.ToolbarToggleButton`** - For toggle buttons being placed in a toolbar (transparent background until hovering)
- **`Styles.AccentComboBox`** - For combo boxes that should gain attention (accent color as background)
- **`Styles.AccentComboBoxItem`** - Matching combo box item style for `AccentComboBox`
- **`Styles.ToggleSwitch`** - Alternative look for check boxes and toggle buttons
- **`Styles.WindowButton`** - For buttons placed in the window's title bar
- **`Styles.WindowToggleButton`** - For toggle buttons placed in the window's title bar

## Available templates

- **`Templates.DatePickerDropDownButton`** - Button in date pickers that opens the calender popup
- **`Templates.Expander`** - Small expander triangle that is used in combo boxes, tree views, etc.
- **`Templates.LoadingCircle`** - Loading throbber with dots running in circles
- **`Templates.LoadingBars`** - Loading throbber with three bars growing and shrinking
- **`Templates.LoadingDots`** - Loading throbber with appearing and disappearing dots
- **`Templates.ValidationErrorTemplate`** - Generic validation error template

## Available Icons

- **`Icons.AdonisUI`** - Favicon of Adonis UI
- **`Icons.AdonisUIGrayscale`** - Favicon of Adonis UI in grayscale colors
- **`Icons.Error`** - Error icon used for displaying validation errors
- **`Icons.WindowMinimize`** - Minimize icon used for `AdonisWindow`'s minimize button
- **`Icons.WindowMaximize`** - Maximize icon used for `AdonisWindow`'s maximize button
- **`Icons.WindowRestore`** - Restore icon used for `AdonisWindow`'s restore button
- **`Icons.WindowClose`** - Close icon used for `AdonisWindow`'s close button
