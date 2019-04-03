---
layout: default
title: Colors and Brushes
---

# Colors and Brushes

AdonisUI tries to get along with as few colors as possible. The color definitions go hand in hand with the [layering system](./layers). For each layer there are a total of nine colors except for the base layer which needs only two. For each color there is also a brush with the same name.

- `Background` - background color of each control on that layer
- `Border` - border color of each control on that layer
- `Highlight` - background color when hovering over a control
- `HighlightBorder` - border color when hovering over a control
- `IntenseHighlight` - background color for [cursor spotlight](./cursor-spotlight)
- `IntenseHighlightBorder` - border color for [cursor spotlight](./cursor-spotlight)
- `Interaction` - background color when [clicking a control](./ripple)
- `InteractionBorder` - border color when [clicking a control](./ripple)
- `InteractionForeground` - foreground color when [clicking a control](./ripple)

These colors and brushes are used by every style of AdonisUI. Changing the basic background color for example will affect the background of every button, text box, radio button and so on.

Colors and brushes can be assigned using the according *ComponentResourceKeys*.
- Using a color: `Color="{DynamicResource {x:Static adonisUi:Colors.Layer1BackgroundColor}}"`
- Using a brush: `Background="{DynamicResource {x:Static adonisUi:Brushes.Layer1BackgroundBrush}}"`

***Note:** AdonisUI must be included as namespace in Xaml: `xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI"`*

## Accent color

While relying on uniform colors for background areas and borders, an accent color can be used for visual highlighting of important spots. By default, both color schemes use blue as their accent color. This can be changed by overriding the accent color values as described [here](#overriding-colors).

## Available colors

Each color has a related brush with the same name that just ends with *"Brush"* instead of *"Color"*.

- Layer 0
  - `Layer0BackgroundColor`
  - `Layer0BorderColor`
- Layer 1
  - `Layer1BackgroundColor`
  - `Layer1BorderColor`
  - `Layer1HighlightColor`
  - `Layer1HighlightBorderColor`
  - `Layer1IntenseHighlightColor`
  - `Layer1IntenseHighlightBorderColor`
  - `Layer1InteractionColor`
  - `Layer1InteractionBorderColor`
  - `Layer1InteractionForegroundColor`
- Layer 2
  - `Layer2BackgroundColor`
  - `Layer2BorderColor`
  - `Layer2HighlightColor`
  - `Layer2HighlightBorderColor`
  - `Layer2IntenseHighlightColor`
  - `Layer2IntenseHighlightBorderColor`
  - `Layer2InteractionColor`
  - `Layer2InteractionBorderColor`
  - `Layer2InteractionForegroundColor`
- Layer 3
  - `Layer3BackgroundColor`
  - `Layer3BorderColor`
  - `Layer3HighlightColor`
  - `Layer3HighlightBorderColor`
  - `Layer3IntenseHighlightColor`
  - `Layer3IntenseHighlightBorderColor`
  - `Layer3InteractionColor`
  - `Layer3InteractionBorderColor`
  - `Layer3InteractionForegroundColor`
- Layer 4
  - `Layer4BackgroundColor`
  - `Layer4BorderColor`
  - `Layer4HighlightColor`
  - `Layer4HighlightBorderColor`
  - `Layer4IntenseHighlightColor`
  - `Layer4IntenseHighlightBorderColor`
  - `Layer4InteractionColor`
  - `Layer4InteractionBorderColor`
  - `Layer4InteractionForegroundColor`
- Accent
  - `AccentColor`
  - `AccentForegroundColor`
  - `AccentHighlightColor`
  - `AccentIntenseHighlightColor`
  - `AccentIntenseHighlightBorderColor`
  - `AccentInteractionColor`
  - `AccentInteractionBorderColor`
  - `AccentInteractionForegroundColor`
- Misc
  - `ForegroundColor`
  - `DisabledForegroundColor`
  - `DisabledAccentForegroundColor`
  - `ErrorColor`
  - `AlertColor`
  - `SuccessColor`

## Overriding colors

To override single colors with custom values simply assign a new value to the corresponding resource key **after** including AdonisUI in your `App.xaml`.

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="{x:Static adonisUi:ResourceLocator.LightColorScheme}"/>
            <ResourceDictionary Source="{x:Static adonisUi:ResourceLocator.ClassicTheme}"/>
        </ResourceDictionary.MergedDictionaries>

        <!-- Override colors as you like -->
        <Color x:Key="{x:Static adonisUi:Colors.AccentColor}">#0BAC08</Color>

    </ResourceDictionary>
</Application.Resources>
```

Because each brush references a color, they will point to the new color values automatically and do not have to be adjusted.

## Switching color schemes at runtime

To switch a color scheme the currently loaded colors have to be removed from the application resources and be replaced with new resources that use the identical resource keys. AdonisUI brings a helper method which does that for you that can be called inside a button click handler for example.

```csharp
private bool _isDark;

private void ChangeTheme(object sender, RoutedEventArgs e)
{
    ResourceLocator.SetColorScheme(Application.Current.Resources, _isDark ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);

    _isDark = !_isDark;
}
```

The `ResourceLocator` is part of AdonisUI living directly at the root namespace `AdonisUI.ResourceLocator`. The first parameter is a reference to the `ResourceDictionary` which holds the colors. By default this should be the application resources but if the color scheme has been added to the resources of a window for example, that window's `ResourceDictionary` must be given here. The second parameter is an `Uri` to the replacing `ResourceDictionary`.

The `ResourceLocator` is capable of removing the current color scheme from the application resources on its own as long as it is one of the built-in color schemes. If you switch from a custom color scheme you need to specify the `Uri` to this as a third parameter.

```csharp
Uri replacedColorSchemeUri = new Uri("pack://application:,,,/MyApp;component/ColorSchemes/CustomColorScheme1.xaml", UriKind.Absolute)
Uri replacingColorSchemeUri = new Uri("pack://application:,,,/MyApp;component/ColorSchemes/CustomColorScheme2.xaml", UriKind.Absolute)

AdonisUI.ResourceLocator.SetColorScheme(Application.Current.Resources, replacingColorSchemeUri, replacedColorSchemeUri);
```

## Custom color schemes

Custom color schemes need to be defined the same way as the built-in color schemes. A single `ResourceDictionary` file should hold all colors and brushes that use the resource keys of AdonisUI. This `ResourceDictionary` can be included into the application resources instead of a built-in color scheme. To make sure every key has a color or brush assigned it is recommended to copy the contents of an existing color scheme file and set the preferred values.
