---
layout: default
title: Introduction
tagline: Get started with Adonis UI
parent: Getting started
nav_order: 0
---

# Introduction

Get started with Adonis UI, a lightweight UI toolkit for WPF applications. Including Adonis UI in your project gives you a set of default styles for all major WPF controls.

## Quick start

1. Reference `AdonisUI` and `AdonisUI.ClassicTheme` in your WPF project. It is available via [NuGet](https://www.nuget.org/packages/AdonisUI.ClassicTheme/) or [manual download](https://github.com/benruehl/adonis-ui/releases). Currently it requires at least .NET 4.5 or .NET Core 3.0.
2. Add resources to your application in your `App.xaml`:

    ```xml
    <Application xmlns:adonisUi="clr-namespace:AdonisUI;assembly=AdonisUI">
        <Application.Resources>
            <ResourceDictionary>
                <ResourceDictionary.MergedDictionaries>
                    <ResourceDictionary Source="pack://application:,,,/AdonisUI;component/ColorSchemes/Light.xaml"/>
                    <ResourceDictionary Source="pack://application:,,,/AdonisUI.ClassicTheme;component/Resources.xaml"/>
                </ResourceDictionary.MergedDictionaries>
            </ResourceDictionary>
        </Application.Resources>
    </Application>
    ```

3. Derive your window's style from the default style of Adonis UI:

    ```xml
    <Window.Style>
        <Style TargetType="Window" BasedOn="{StaticResource {x:Type Window}}"/>
    </Window.Style>
    ```
    
    You are now automatically using the default styles of Adonis UI. If you wish to use the dark theme instead, exchange `Light.xaml` with `Dark.xaml`.

4. (Optional) If you want your window's title bar to be themed as well, you need to derive your window from `AdonisWindow`. This is primarily beneficial if you make use of the dark color scheme as the title bar becomes dark here, too. See the [window guide](../guides/window.md) for further explanation.

> In case Adonis UI's resources cannot be included in the application's resources for some reason but instead are inserted in the window's resources or the resources of some other control, further steps need to be made.
> See [here](../guides/space.md#remarks-when-adonisui-is-not-included-in-the-application-resources) for further info.
