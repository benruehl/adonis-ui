using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdonisUI.Properties;

namespace AdonisUI
{
    public static class Brushes
    {
        public static ComponentResourceKey ForegroundBrush => new ComponentResourceKey(typeof(Styles), "ForegroundBrush");

        public static ComponentResourceKey AccentBrush => new ComponentResourceKey(typeof(Styles), "AccentBrush");
        public static ComponentResourceKey AccentDarkerBrush => new ComponentResourceKey(typeof(Styles), "AccentDarkerBrush");
        public static ComponentResourceKey AccentLighterBrush => new ComponentResourceKey(typeof(Styles), "AccentLighterBrush");
        public static ComponentResourceKey AccentForegroundBrush => new ComponentResourceKey(typeof(Styles), "AccentForegroundBrush");

        public static ComponentResourceKey Level0BackgroundBrush => new ComponentResourceKey(typeof(Styles), "Level0BackgroundBrush");
        public static ComponentResourceKey Level0BorderBrush => new ComponentResourceKey(typeof(Styles), "Level0BorderBrush");

        public static ComponentResourceKey Level1BackgroundBrush => new ComponentResourceKey(typeof(Styles), "Level1BackgroundBrush");
        public static ComponentResourceKey Level1BorderBrush => new ComponentResourceKey(typeof(Styles), "Level1BorderBrush");
        public static ComponentResourceKey Level1HightlightBrush => new ComponentResourceKey(typeof(Styles), "Level1HightlightBrush");

        public static ComponentResourceKey Level2BackgroundBrush => new ComponentResourceKey(typeof(Styles), "Level2BackgroundBrush");
        public static ComponentResourceKey Level2BorderBrush => new ComponentResourceKey(typeof(Styles), "Level2BorderBrush");

        public static ComponentResourceKey Level3BackgroundBrush => new ComponentResourceKey(typeof(Styles), "Level3BackgroundBrush");
        public static ComponentResourceKey Level3BorderBrush => new ComponentResourceKey(typeof(Styles), "Level3BorderBrush");

        public static ComponentResourceKey DisabledForegroundBrush => new ComponentResourceKey(typeof(Styles), "DisabledForegroundBrush");
    }
}
