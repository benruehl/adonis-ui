---
layout: default
title: Dimensions
parent: Guides
---

# Dimensions

Dimensions are a set of values that specify sizes or distances in AdonisUI. Overriding the values will change them for all controls at once. To override them, new values have to be assigned to their keys *after* including AdonisUI in you application.

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="{x:Static adonisUi:ResourceLocator.LightColorScheme}"/>
            <ResourceDictionary Source="{x:Static adonisUi:ResourceLocator.ClassicTheme}"/>
        </ResourceDictionary.MergedDictionaries>

        <!-- Override dimensions as you like -->
        <Color x:Key="{x:Static adonisUi:Dimensions.CornerRadius}">2</Color>
        <Color x:Key="{x:Static adonisUi:Dimensions.BorderThickness}">2</Color>

    </ResourceDictionary>
</Application.Resources>
```

## Available dimensions

- **`Dimensions.BorderThickness`** - BorderThickness of every control styled by AdonisUI
- **`Dimensions.CornerRadius`** - CornerRadius of every control styled by AdonisUI
- **`Dimensions.CursorSpotlightRelativeSize`** - see [Cursor Spotlight](cursor-spotlight.md)

### Corner Radius

In addition to the global value the corner radius can be set on each individual control using the `CornerRadiusExtension`.

```xml
<!-- xmlns:adonisExtensions="clr-namespace:AdonisUI.Extensions;assembly=AdonisUI" -->
<Button adonisExtensions:CornerRadiusExtension.CornerRadius="4"/>
```

This is supported by all controls that have a default style shipping with AdonisUI like Buttons, TextBoxes, GroupBoxes and even controls like ListBoxItems and GridViewHeaders.
