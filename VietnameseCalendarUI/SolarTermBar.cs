/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Augustine.VietnameseCalendar.UI
{
    public static class SolarTermBar
    {
        // Non-linear hue interpolation: the values in [brackets] are selected value for each season.
        // Other values are linearly interpolated by segment.
        //                  339   300   260   220   180   140   110   90    70    
        //        Hue:  [359]  319   280  [240]  200   160  [120]  100   80   [60]50 40 30 20 10/[0=359]
        // Term Index:   0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23/ 0
        //               ^-winter solstice ^-spring equinox  ^-summer solstice ^-autumn equinox / ^-winter solstice
        //           ...________/\________________/\________________/\________________/\________/__...
        //               winter        spring            summer            autumn        winter

        /// <summary>
        /// Hue pallet for solar terms.
        /// 0: Spring equinox,... 6: Summer solstice,... 12: Autumn equinox,... 18: Winter solstice...
        /// </summary>
        public static readonly int[] Hues = {120, 110, 100,  90,  80,  70,
                                              60,  50,  40,  30,  20,  10,
                                             359, 339, 319, 300, 280, 260,
                                             240, 220, 200, 180, 160, 140 };
        public static readonly string[] SolarTermsVietnamese = {"Xuân Phân", "Thanh Minh",
                                                 "Cốc Vũ", "Lập Hạ",
                                                 "Tiểu Mãn", "Mang Chủng",
                                                 "Hạ Chí", "Tiểu Thử",
                                                 "Đại Thử", "Lập Thu",
                                                 "Xử Thử", "Bạch Lộ",
                                                 "Thu Phân", "Hàn Lộ",
                                                 "Sương Giáng", "Lập Đông",
                                                 "Tiểu Tuyết", "Đại Tuyết",
                                                 "Đông Chí", "Tiểu Hàn",
                                                 "Đại Hàn", "Lập Xuân",
                                                 "Vũ Thủy", "Kinh Trập"};
        private static readonly string CURRENT_TERM_LABEL = "current";

        /// <summary>
        /// (solarTermIndex + HueOffset) % 24 = hueIndex
        /// </summary>
        private static int HueOffset = 0;

        /// <summary>
        /// Return hue value (0..359) from hue pallet according to the solar term index.
        /// 0: Spring equinox,... 6: Summer solstice,... 12: Autumn equinox,... 18: Winter solstice...
        /// </summary>
        /// <param name="solarTermIndex"></param>
        /// <returns></returns>
        public static int GetHueValue(int solarTermIndex) { return Hues[(solarTermIndex + HueOffset) % 24]; }

        public static UserControl CreateSolarTermBar(int year, double timeZone)
        {
            UserControl uc = new UserControl();
            Grid grid = new Grid();
            grid.SnapsToDevicePixels = true;
            DateTime dateTime = new DateTime(year, 1, 1);
            var todaySolarTermIdx = LuniSolarDate.GetSolarTermIndex(DateTime.Today, 7);

            for (int i = 0; i < 24; i++)
            {
                var idx = (24 + i - 5) % 24;
                dateTime = Astronomy.GetDateTimeOfSolarTerm(idx, year).AddHours(timeZone);
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                var rec = CreateRectangle(idx, dateTime, "");
                if (DateTime.Today.Year == year && todaySolarTermIdx == idx)
                    rec.Name = CURRENT_TERM_LABEL;
                Grid.SetColumn(rec, i);
                grid.Children.Add(rec);
            }

            uc.Content = grid;

            return uc;
        }

        private static Rectangle CreateRectangle(int solarTermIndex, DateTime date, string description)
        {
            if (solarTermIndex >= 24)
                solarTermIndex = solarTermIndex % 24;

            int H = GetHueValue(solarTermIndex);
            Color normalBackground = Helper.ColorFromHSV(H, 0.6, 1);
            Color zeroAlphaBackground = Color.FromArgb(0, normalBackground.R, normalBackground.G, normalBackground.B);
            Color mouseOverBakground = Helper.ColorFromHSV(H, 0.6, 0.8);

            GradientStopCollection normalTermGradient = new GradientStopCollection
            {
                new GradientStop(zeroAlphaBackground, 0),
                new GradientStop(zeroAlphaBackground, 0.45),
                new GradientStop(normalBackground, 0.45),
                new GradientStop(normalBackground, 0.65),
                new GradientStop(zeroAlphaBackground, 0.65),
                new GradientStop(zeroAlphaBackground, 1)
            };
            LinearGradientBrush normalTermBrush = new LinearGradientBrush(normalTermGradient, 90);

            GradientStopCollection currentTermGradient = new GradientStopCollection
            {
                new GradientStop(zeroAlphaBackground, 0),
                new GradientStop(zeroAlphaBackground, 0.35),
                new GradientStop(normalBackground, 0.35),
                new GradientStop(normalBackground, 0.75),
                new GradientStop(zeroAlphaBackground, 0.75),
                new GradientStop(zeroAlphaBackground, 1)
            };
            LinearGradientBrush currentTermBrush = new LinearGradientBrush(currentTermGradient, 90);

            SolidColorBrush hoverBrush = new SolidColorBrush(mouseOverBakground);

            Rectangle rectangle = new Rectangle()
            {
                Height = 10,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                ToolTip = CreateToolTip(solarTermIndex, date, description),
            };
            rectangle.Style = new Style()
            {
                Triggers = { new Trigger() {
                    Property = Rectangle.NameProperty, Value = CURRENT_TERM_LABEL,
                    Setters = { new Setter() {
                        Property = Rectangle.FillProperty, Value = currentTermBrush, }, }, },
                    new Trigger() {
                    Property = Rectangle.IsMouseOverProperty, Value = true,
                    Setters = { new Setter() {
                        Property = Rectangle.FillProperty, Value = hoverBrush, }, }, }, },

                Setters = { new Setter() {
                    Property = Rectangle.FillProperty, Value = normalTermBrush, } },
            };

            return rectangle;
        }

        private static ToolTip CreateToolTip(int solarTermIndex, DateTime date, string description)
        {
            if (solarTermIndex >= 24)
                solarTermIndex = solarTermIndex % 24;

            int H = GetHueValue(solarTermIndex);
            Color background = Helper.ColorFromHSV(H, 0.1, 1); // very light color
            Color foreground = Helper.ColorFromHSV(H, 0.5, 0.3); // dark color
            ToolTip toolTip = new ToolTip
            {
                MaxWidth = 200,
                Padding = new Thickness(3),
                Background = new SolidColorBrush(background),
                Foreground = new SolidColorBrush(foreground)
            };
            StackPanel stackPanel = new StackPanel();
            TextBlock headerTextBlock = new TextBlock
            {
                Text = SolarTermsVietnamese[solarTermIndex],
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.SemiBold,
            };
            TextBlock subHeaderTextBlock = new TextBlock
            {
                Text = (date != null ? date.ToString("dd/MM/yyyy HH:mm UTC+07") : ""),
                TextWrapping = TextWrapping.Wrap,
            };
            TextBlock contentTextBlock = new TextBlock
            {
                Text = description,//"Description (to be added).",
                TextWrapping = TextWrapping.Wrap,
            };
            stackPanel.Children.Add(headerTextBlock);
            stackPanel.Children.Add(subHeaderTextBlock);
            stackPanel.Children.Add(new Separator());
            stackPanel.Children.Add(contentTextBlock);
            toolTip.Content = stackPanel;
            return toolTip;
        }
    }
}
