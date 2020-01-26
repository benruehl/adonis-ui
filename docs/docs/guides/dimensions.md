---
layout: default
title: Dimensions
parent: Guides
---

# Dimensions

Dimensions are a set of values that specify sizes or distances in Adonis UI. Overriding the values will change them for all controls at once. To override them, new values have to be assigned to their keys *after* including Adonis UI in you application.

```xml
<!-- xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI" -->
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/AdonisUI;component/ColorSchemes/Light.xaml"/>
            <ResourceDictionary Source="pack://application:,,,/AdonisUI.ClassicTheme;component/Resources.xaml"/>
        </ResourceDictionary.MergedDictionaries>

        <!-- Override dimensions as you like -->
        <CornerRadius x:Key="{x:Static adonisUi:Dimensions.CornerRadius}">2</CornerRadius>
        <Thickness x:Key="{x:Static adonisUi:Dimensions.BorderThickness}">1</Thickness>

    </ResourceDictionary>
</Application.Resources>
```

## Available dimensions

- **`Dimensions.BorderThickness`** - BorderThickness of every control styled by Adonis UI
- **`Dimensions.CornerRadius`** - CornerRadius of every control styled by Adonis UI
- **`Dimensions.CursorSpotlightRelativeSize`** - see [Cursor Spotlight](cursor-spotlight.md)
- **`Dimensions.HorizontalSpace`** - horizontal space factor that is applied when using [`AdonisUI.Space`](space.md)
- **`Dimensions.VerticalSpace`** - vertical space factor that is applied when using [`AdonisUI.Space`](space.md)

### Corner Radius

In addition to the global value the corner radius can be set on each individual control using the `CornerRadiusExtension`.

```xml
<!-- xmlns:adonisExtensions="clr-namespace:AdonisUI.Extensions;assembly=AdonisUI" -->
<Button adonisExtensions:CornerRadiusExtension.CornerRadius="4"/>
```

This is supported by all controls that have a default style shipping with Adonis UI like Buttons, TextBoxes, GroupBoxes and even controls like ListBoxItems and GridViewHeaders.
