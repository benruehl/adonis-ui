---
layout: page
title: Custom controls
---

# Custom controls

All custom control live in the namespace `AdonisUI.Controls`.

## SplitButton

A button that has a small arrow part on the right side which opens a menu on click.

[SplitButton collapsed](../img/adonis-demo-splitbutton-collapsed-light.png)

[SplitButton expanded](../img/adonis-demo-splitbutton-expanded-light.png)

```xml
<adonisControls:SplitButton Content="Split Button"
                            Command="{Binding DefaultCommand}">
    <adonisControls:SplitButton.SplitMenu>
        <ContextMenu MinWidth="{Binding PlacementTarget.ActualWidth, RelativeSource={RelativeSource Self}}">
            <MenuItem Header="Command 1" Command="{Binding Command1}"/>
            <MenuItem Header="Command 2" Command="{Binding Command2}"/>
            <MenuItem Header="Command 3" Command="{Binding Command3}"/>
        </ContextMenu>
    </adonisControls:SplitButton.SplitMenu>
</adonisControls:SplitButton>
```
