---
layout: page
title: Styles and Templates
---

# Styles and Templates

AdonisUI offers the following resource kinds:

- **Brushes** - accessible via `AdonisUI.Brushes`
- **Colors** - accessible via `AdonisUI.Colors`
- **Dimensions** - accessible via `AdonisUI.Dimensions`
- **Icons** - accessible via `AdonisUI.Icons`
- **Styles** - accessible via `AdonisUI.Styles`
- **Templates** - accessible via `AdonisUI.Templates`

For brushes and colors see [here](colors-and-brushes). For dimensions see [here](dimensions). In this article, styles, templates and icons are addressed.

## Usage

All resources of AdonisUI including styles, templates and icons can be referenced using *ComponentResourceKeys*.

Example:

```xml
<!-- xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI" -->
<Button Style="{DynamicResource {x:Static adonisUi:Styles.AccentButtonStyle}}"/>
```

## Available styles

- **`Styles.AccentButton`** - For buttons that should gain attention (accent color as background)
- **`Styles.ToolbarButton`** - For buttons being placed in a toolbar (transparent background until hovering)
- **`Styles.AccentToolbarButton`** - For buttons being placed in a toolbar that should gain attention when hovering

## Available templates

- **`Templates.ValidationErrorTemplate`** - Generic validation error template
- **`Templates.LoadingCircle`** - Loading throbber with dots running in circles
- **`Templates.LoadingBars`** - Loading throbber with three bars growing and shrinking
- **`Templates.LoadingDots`** - Loading throbber with appearing and disappearing dots

## Available Icons

- **`Icons.AdonisUI`** - Favicon of AdonisUI
- **`Icons.AdonisUIGrayscale`** - Favicon of AdonisUI in grayscale colors
- **`Icons.Error`** - Error icon used for displaying validation errors
