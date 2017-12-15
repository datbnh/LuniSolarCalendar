using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    public static class Themes
    {
        public static Theme Light {
            get => new Theme(ThemeColors.Light, null, new TextAndShadow() {
                IsDropShadow = false,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Display, }); }
        public static Theme Dark {
            get => new Theme(ThemeColors.Dark, null, new TextAndShadow() {
                IsDropShadow = false,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Display, }); }
        public static Theme LightSemiTransparent {
            get => new Theme(ThemeColors.LightSemiTransparent, null, new TextAndShadow() {
                IsDropShadow = true,
                ShadowDepth = 2,
                ShadowRadius = 3,
                ShadowColor = Colors.White,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Ideal, }); }
        public static Theme DarkSemiTransparent {
            get => new Theme(ThemeColors.DarkSemiTransparent, null, new TextAndShadow() {
                IsDropShadow = true,
                ShadowDepth = 2,
                ShadowRadius = 3,
                ShadowColor = Colors.Black,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Ideal, }); }
    }
}
