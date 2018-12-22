---
layout: default
title: Dimensions
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
- **`Dimensions.CursorSpotlightRelativeSize`** - see [Cursor Spotlight](cursor-spotlight)
