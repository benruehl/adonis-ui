---
layout: default
title: Introduction
tagline: Get started with AdonisUI
---

# Introduction

Get started with AdonisUI, a lightweight UI toolkit for WPF applications offering classic but enhanced windows visuals. Including AdonisUI in your project gives you a set of default styles for all major WPF controls. The visual design aims to be similar to the built-in counterparts, but brings a visual overhaul to create a more consistent modern look.

Additionally, AdonisUI offers the following features:

- **Theming** including theme switching at runtime
- Additional **styles** and few **custom controls** for common use cases
- **Data validation** support
- Optional **layering system**

# Getting started

1. Reference `AdonisUI` and `AdonisUI.ClassicTheme` in your WPF project. It is available via [NuGet](https://www.nuget.org/packages/AdonisUI.ClassicTheme/) or [manual download](https://github.com/benruehl/adonis-ui/releases). Currently it requires at least .NET 4.0.
2. Add resources to your application in your `App.xaml` like so:

```xml
<Application xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="{x:Static adonisUi:ResourceLocator.LightColorScheme}"/>
                <ResourceDictionary Source="{x:Static adonisUi:ResourceLocator.ClassicTheme}"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

3. Derive your window's style from the default style of AdonisUI like so:

```xml
<Window.Style>
    <Style TargetType="Window" BasedOn="{StaticResource {x:Type Window}}"/>
</Window.Style>
```

You are now automatically using the default styles of AdonisUI. If you wish to use the dark theme instead, exchange `ResourceLocator.LightColorScheme` with `ResourceLocator.DarkColorScheme`.

# Docs

- [Colors and Brushes](./pages/colors-and-brushes)
- [Cursor spotlight](./pages/cursor-spotlight)
- [Custom controls](./pages/custom-controls)
- [Data validation](./pages/data-validation)
- [Dimensions](./pages/dimensions)
- [Layers](./pages/layers)
- [Ripple effect](./pages/ripple)
- [Space](./pages/space)
- [Styles, templates and icons](./pages/styles-and-templates)
- [Watermarks](./pages/watermark)
