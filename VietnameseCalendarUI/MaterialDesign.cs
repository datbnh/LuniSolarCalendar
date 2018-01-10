using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Augustine.VietnameseCalendar.UI
{
    public static class MaterialDesign
    {
        public static class Shadows
        {
            public static DropShadowEffect zDepth1 = new DropShadowEffect()
            {
                BlurRadius = 5,
                ShadowDepth = 1,
                Direction = 270,
                Color = (Color)ColorConverter.ConvertFromString("#CCCCCC"),
            };

            public static DropShadowEffect zDepth2 = new DropShadowEffect()
            {
                BlurRadius = 8,
                ShadowDepth = 2.5,
                Direction = 270,
                Color = (Color)ColorConverter.ConvertFromString("#BBBBBB"),
            };

            public static DropShadowEffect zDepth3 = new DropShadowEffect()
            {
                BlurRadius = 14,
                ShadowDepth = 4.5,
                Direction = 270,
                Color = (Color)ColorConverter.ConvertFromString("#BBBBBB"),
            };

            public static DropShadowEffect zDepth4 = new DropShadowEffect()
            {
                BlurRadius = 25,
                ShadowDepth = 8,
                Direction = 270,
                Color = (Color)ColorConverter.ConvertFromString("#BBBBBB"),
            };

            public static DropShadowEffect zDepth5 = new DropShadowEffect()
            {
                BlurRadius = 35,
                ShadowDepth = 13,
                Direction = 270,
                Color = (Color)ColorConverter.ConvertFromString("#BBBBBB"),
            };
        }

        public static class Colors
        {
            public static string Grey    = "#9E9E9E";
            public static string Grey50  = "#FAFAFA";
            public static string Grey100 = "#F5F5F5";
            public static string Grey200 = "#EEEEEE";
            public static string Grey300 = "#E0E0E0";
            public static string Grey400 = "#BDBDBD";
            public static string Grey500 = "#9E9E9E";
            public static string Grey600 = "#757575";
            public static string Grey700 = "#616161";
            public static string Grey800 = "#424242";
            public static string Grey900 = "#212121";

        }
    }
}
