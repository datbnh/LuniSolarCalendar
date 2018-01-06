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
    }
}
