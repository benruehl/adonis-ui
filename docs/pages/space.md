---
layout: default
title: Space
---

# Space

Space between controls is typically controlled with margins, paddings and grid rows and columns. To make sure the space is consistent in every spot, a fixed size can be chosen that is used everywhere (or a multiple of it). AdonisUI provides a system supporting you in doing so. By default, the base value for space is `8`, but this can be adjusted for horizontal and vertical space separately.

## Usage

Space can be applied like so:

```xml
<!-- xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI" -->

<RowDefinition Height="{adonisUi:Space 1}"/> <!-- equals Height="8" -->
<RowDefinition Height="{adonisUi:Space 2.5}"/> <!-- equals Height="20" -->
<RowDefinition Height="{adonisUi:Space 2.5+1}"/> <!-- equals Height="21" -->
<RowDefinition Height="{adonisUi:Space 2.5-1}"/> <!-- equals Height="19" -->
```

The same works also for thicknesses like margin and padding:

```xml
<Button Margin="{adonisUi:Space 1}"/> <!-- equals Margin="8,8,8,8" -->
<Button Margin="{adonisUi:Space 1, 2}"/> <!-- equals Margin="8,16,8,16" -->
<Button Margin="{adonisUi:Space 1, 1+2, 2, 3}"/> <!-- equals Margin="8,10,16,24" -->
```

In case there is a `+` or `-` in the expression, the second value is an absolute offset while the first value is always the factor which is multiplied with the base value of `8`.

Sometimes it is not clear to the system whether the horizontal or vertical space should be applied. This happens when properties are not of type `Thickness` and do not contain certain keywords like 'height', 'width', 'horizontal' or 'vertical' in their names. In that case, the orientation can be applied manually.

```xml
<TextBlock SomeProperty="{adonisUi:Space 1, Orientation=Vertical}"/>
```

## Customization

The following resources can be placed in the application resources right after including AdonisUI in order to override the default base space values.

```xml
<system:Double x:Key="{x:Static adonisUi:Dimensions.HorizontalSpace}">8</system:Double>
<system:Double x:Key="{x:Static adonisUi:Dimensions.VerticalSpace}">8</system:Double>
```
