using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdonisUI
{
    public static class Dimensions
    {
        public static ComponentResourceKey CornerRadius => new ComponentResourceKey(typeof(Dimensions), "CornerRadius");

        public static ComponentResourceKey BorderThickness => new ComponentResourceKey(typeof(Dimensions), "BorderThickness");

        public static ComponentResourceKey CursorSpotlightRelativeSize => new ComponentResourceKey(typeof(double), "CursorSpotlightRelativeSize");
    }
}
