---
layout: default
title: Watermark
---

# Watermark

A pale rendered text that can be set on some controls. It disappears as soon as the users enters input.

![Watermark examples](../img/adonis-demo-watermark-light.png)

Available for following controls:
- `ComboBox`
- `DatePicker`
- `PasswordBox`
- `TextBox`

Can be set using `AdonisUI.Extensions.WatermarkExtension`:

```xml
<!-- xmlns:adonisExtensions="clr-namespace:AdonisUI.Extensions;assembly=AdonisUI" -->

<TextBox adonisExtensions:WatermarkExtension.Watermark="Search ..."/>
```
