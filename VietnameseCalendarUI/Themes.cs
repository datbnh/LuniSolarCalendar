/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

namespace Augustine.VietnameseCalendar.UI
{
    public static class Themes
    {
        public static Theme Light {
            get => new Theme(ThemeColors.Light, TextSize.DefaultTextSize(), new TextAndShadow() {
                IsDropShadow = false,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Ideal, }); }
        public static Theme Dark {
            get => new Theme(ThemeColors.Dark, TextSize.DefaultTextSize(), new TextAndShadow() {
                IsDropShadow = false,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Display, }); }
        public static Theme LightSemiTransparent {
            get => new Theme(ThemeColors.LightSemiTransparent, TextSize.DefaultTextSize(), new TextAndShadow() {
                IsDropShadow = true,
                ShadowDepth = 2,
                ShadowRadius = 3,
                ShadowColor = System.Windows.Media.Colors.Black,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Display, }); }
        public static Theme DarkSemiTransparent {
            get => new Theme(ThemeColors.DarkSemiTransparent, TextSize.DefaultTextSize(), new TextAndShadow() {
                IsDropShadow = true,
                ShadowDepth = 2,
                ShadowRadius = 3,
                ShadowColor = System.Windows.Media.Colors.Gray,
                TextFormattingMode = System.Windows.Media.TextFormattingMode.Display, }); }
    }
}
