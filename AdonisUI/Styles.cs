using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdonisUI.Properties;

namespace AdonisUI
{
    public static class Styles
    {
        public static ComponentResourceKey AccentButton => new ComponentResourceKey(typeof(Styles), "AccentButton");

        public static ComponentResourceKey ToolbarButton => new ComponentResourceKey(typeof(Styles), "ToolbarButton");

        public static ComponentResourceKey AccentToolbarButton => new ComponentResourceKey(typeof(Styles), "AccentToolbarButton");

        public static ComponentResourceKey ToolbarToggleButton => new ComponentResourceKey(typeof(Styles), "ToolbarToggleButton");
    }
}
