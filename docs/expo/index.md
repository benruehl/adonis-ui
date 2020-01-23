---
layout: expo
title: Expo
tagline: Inspiring uses of Adonis UI
nav_exclude: true
---

# Adonis UI Expo

Showcase of applications using Adonis UI.

{%- assign exhibits = site.expo | sort:"order" -%}
{% for exhibit in exhibits %}
<div class="expo-item">
    <div class="expo-item-title">
        <h2 id="{{ exhibit.name | replace: ' ', '-' }}">{{ exhibit.name }}</h2>
        {{ exhibit.author }} | {{ exhibit.date_visited }}
    </div>
    <p>{{ exhibit.tagline }}</p>
    {{ exhibit.content }}
    <p>
        <center>
            {% include button.html content="Visit Project" url=exhibit.project_url %}
        </center>
    </p>
</div>
{% endfor %}

## Suggest a project

To submit a site suggestion, please [open an issue](https://github.com/benruehl/adonis-ui/issues/new/) or create a pull request on [GitHub](https://github.com/benruehl/adonis-ui/).
