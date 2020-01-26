---
layout: default
title: Configuration
tagline: Adapt Adonis UI to your style
parent: Getting started
---

# Configuration

Adonis UI works out of the box without configuration. Nevertheless, most aspects can be configured to meet personal needs. The following is only an overview, more detailed options are discussed in the corresponding guides.

## How it works

Adonis UI offers resources like colors, brushes, styles and templates. Each of them has a unique resource key assigned which works as a name under which the resource can be used. For example, the accent color has the key `Colors.AccentColor`.

In order to change a resource, a new resource simply needs to be assigned to the corresponding key. This can be done in multiple ways, e.g.:

- Include Adonis UI in your `App.xaml`'s resources. Then add a new resource with the same key like the one you want to override to your application resources **afterwards**.
- Include Adonis UI in your `App.xaml`'s resources. Then create a new `ResourceDictionary` and include your resources. Add it to your application resources.
- Create a custom `ResourceDictionary` and include Adonis UI in it. Override resources as you like and add it to your application resources. This is the recommended way if you want to override resources depending on the current color scheme. See the [colors and brushes guide](/docs/guides/colors-and-brushes#custom-color-schemes) for more info.

### Named resources

Adonis UI offers the following resource kinds:

- [**Brushes**](/docs/guides/colors-and-brushes) - brushes for each color
- [**Colors**](/docs/guides/colors-and-brushes) - background, accent, foreground colors and more
- [**Dimensions**](/docs/guides/dimensions) - border thickness, corner radius and more
- [**Icons**](/docs/guides/styles-and-templates) - icons that are displayed by Adonis UI
- [**Styles**](/docs/guides/styles-and-templates) - accent button, window button and more
- [**Templates**](/docs/guides/styles-and-templates) - loading throbbers and more

Override resources by assigning the appropriate resource key and using the correct resource type.

```xml
<!-- xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI" -->
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/AdonisUI;component/ColorSchemes/Light.xaml"/>
            <ResourceDictionary Source="pack://application:,,,/AdonisUI.ClassicTheme;component/Resources.xaml"/>
        </ResourceDictionary.MergedDictionaries>

        <!-- Override colors as you like -->
        <Color x:Key="{x:Static adonisUi:Colors.AccentColor}">#0BAC08</Color>

        <!-- Override dimensions as you like -->
        <CornerRadius x:Key="{x:Static adonisUi:Dimensions.CornerRadius}">2</CornerRadius>
        <Thickness x:Key="{x:Static adonisUi:Dimensions.BorderThickness}">1</Thickness>

    </ResourceDictionary>
</Application.Resources>
```

### Default styles

While changing resources like brushes affects all controls, you can also change all instances of a particular control type only. If you want to change the background of all buttons for example, you can override the default button style to achieve that. Follow the same pattern like when overriding named resources but use the control type as resource key. Don't forget to set `BasedOn` if you don't want to create a style from scratch.

```xml
<!-- xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI" -->
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/AdonisUI;component/ColorSchemes/Light.xaml"/>
            <ResourceDictionary Source="pack://application:,,,/AdonisUI.ClassicTheme;component/Resources.xaml"/>
        </ResourceDictionary.MergedDictionaries>

        <!-- Override styles as you like -->
        <Style x:Key="{x:Type Button}"
               TargetType="Button"
               BasedOn="{StaticResource {x:Type Button}}">
            <Setter Property="BorderThickness" Value="0"/>
        </Style>

    </ResourceDictionary>
</Application.Resources>
```
