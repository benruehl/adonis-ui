using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdonisUI.Properties;

namespace AdonisUI
{
    public static class Colors
    {
        public static ComponentResourceKey ForegroundColor => new ComponentResourceKey(typeof(Styles), "ForegroundColor");

        public static ComponentResourceKey AccentColor => new ComponentResourceKey(typeof(Styles), "AccentColor");
        public static ComponentResourceKey AccentDarkerColor => new ComponentResourceKey(typeof(Styles), "AccentDarkerColor");
        public static ComponentResourceKey AccentLighterColor => new ComponentResourceKey(typeof(Styles), "AccentLighterColor");
        public static ComponentResourceKey AccentForegroundColor => new ComponentResourceKey(typeof(Styles), "AccentForegroundColor");

        public static ComponentResourceKey Level0BackgroundColor => new ComponentResourceKey(typeof(Styles), "Level0BackgroundColor");
        public static ComponentResourceKey Level0BorderColor => new ComponentResourceKey(typeof(Styles), "Level0BorderColor");

        public static ComponentResourceKey Level1BackgroundColor => new ComponentResourceKey(typeof(Styles), "Level1BackgroundColor");
        public static ComponentResourceKey Level1BorderColor => new ComponentResourceKey(typeof(Styles), "Level1BorderColor");
        public static ComponentResourceKey Level1HighlightColor => new ComponentResourceKey(typeof(Styles), "Level1HighlightColor");

        public static ComponentResourceKey Level2BackgroundColor => new ComponentResourceKey(typeof(Styles), "Level2BackgroundColor");
        public static ComponentResourceKey Level2BorderColor => new ComponentResourceKey(typeof(Styles), "Level2BorderColor");

        public static ComponentResourceKey Level3BackgroundColor => new ComponentResourceKey(typeof(Styles), "Level3BackgroundColor");
        public static ComponentResourceKey Level3BorderColor => new ComponentResourceKey(typeof(Styles), "Level3BorderColor");

        public static ComponentResourceKey DisabledForegroundColor => new ComponentResourceKey(typeof(Styles), "DisabledForegroundColor");
    }
}
