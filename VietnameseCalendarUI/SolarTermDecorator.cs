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
    public static class SolarTermDecorator
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
        public static readonly int[] Hues = { 359, 339, 319, 300, 280, 260, 240, 220, 200, 180, 160, 140, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 };
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
                                                 "Vũ Thủy", "Kinh Trập",
                                             };
        private static readonly string CURRENT_TERM_LABEL = "current";

        /// <summary>
        /// (solarTermIndex + HueOffset) % 24 = hueIndex
        /// </summary>
        private static int HueOffset = 12;

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

            DateTime dateTime = new DateTime(year, 1, 1);
            var todaySolarTermIdx = LunarDate.GetSolarTermIndex(DateTime.Today, 7);

            for (int i = 0; i < 24; i++)
            {
                var idx = (24 + i - 5) % 24;
                dateTime = FindSolarTermMoment(idx, year).AddHours(timeZone);
                var nextDay = dateTime.Date.AddDays(1);
                var thisDay = dateTime.Date;
                var prevDay = dateTime.Date.AddDays(-1);
                var description = String.Format("{0:dd/MM/yyyy HH:mm UTC+07} = {3}\r\n" +
                                                "{1:dd/MM/yyyy HH:mm UTC+07} = {4}\r\n" +
                                                "{2:dd/MM/yyyy HH:mm UTC+07} = {5}\r\n",
                                                prevDay.AddHours(23).AddMinutes(59),
                                                thisDay.AddHours(23).AddMinutes(59),
                                                nextDay.AddHours(23).AddMinutes(59),
                     LunarDate.GetSolarTermIndex(prevDay, 7),
                     LunarDate.GetSolarTermIndex(thisDay, 7),
                     LunarDate.GetSolarTermIndex(nextDay, 7));
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                var rec = CreateRectangle(idx, dateTime, description);
                if (todaySolarTermIdx == idx)
                    rec.Name = CURRENT_TERM_LABEL;
                Grid.SetColumn(rec, i);
                grid.Children.Add(rec);
            }

            uc.Content = grid;

            return uc;
        }


        public static Rectangle CreateRectangle(int solarTermIndex, DateTime date, string description)
        {
            if (solarTermIndex >= 24)
                solarTermIndex = solarTermIndex % 24;

            int H = GetHueValue(solarTermIndex);
            Color normalBackground = ColorFromHSV(H, 0.6, 1);
            Color zeroAlphaBackground = Color.FromArgb(0, normalBackground.R, normalBackground.G, normalBackground.B);
            Color mouseOverBakground = ColorFromHSV(H, 0.6, 0.8);

            GradientStopCollection gradient = new GradientStopCollection
            {
                new GradientStop(Colors.White, 0),
                new GradientStop(Colors.White, 0.3),
                new GradientStop(normalBackground, 0.3),
                new GradientStop(normalBackground, 0.7),
                new GradientStop(Colors.White, 0.7),
                new GradientStop(Colors.White, 1)
            };
            LinearGradientBrush gradientBrush = new LinearGradientBrush(gradient, 90);

            //GradientStopCollection gradient1 = new GradientStopCollection
            //{
            //    new GradientStop(Colors.White, 0),
            //    new GradientStop(Colors.White, 0.2),
            //    new GradientStop(normalBackground, 0.2),
            //    new GradientStop(normalBackground, 0.8),
            //    new GradientStop(Colors.White, 0.8),
            //    new GradientStop(Colors.White, 1)
            //};
            //LinearGradientBrush gradientBrush1 = new LinearGradientBrush(gradient1, 90);

            SolidColorBrush currentBrush = new SolidColorBrush(normalBackground);

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
                        Property = Rectangle.FillProperty, Value = currentBrush, }, }, },
                    new Trigger() {
                    Property = Rectangle.IsMouseOverProperty, Value = true,
                    Setters = { new Setter() {
                        Property = Rectangle.FillProperty, Value = hoverBrush, }, }, }, },

                Setters = { new Setter() {
                    Property = Rectangle.FillProperty, Value = gradientBrush, } },
            };

            return rectangle;
        }

        public static ToolTip CreateToolTip(int solarTermIndex, DateTime date, string description)
        {
            if (solarTermIndex >= 24)
                solarTermIndex = solarTermIndex % 24;

            int H = GetHueValue(solarTermIndex);
            Color background = ColorFromHSV(H, 0.1, 1); // very light color
            Color foreground = ColorFromHSV(H, 0.5, 0.3); // dark color
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

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            byte v = (byte)Convert.ToInt32(value);
            byte p = (byte)Convert.ToInt32(value * (1 - saturation));
            byte q = (byte)Convert.ToInt32(value * (1 - f * saturation));
            byte t = (byte)Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static DateTime FindSolarTermMoment(int termIndex, int year)
        {
            int[] trialMonths =
                { 3,  4,  4,  5,  5,  6,
                  6,  7,  7,  8,  8,  9,
                  9,  10, 10, 11, 11, 12,
                  12, 1,  1,  2,  2,  3 };
            int[] trialDays =
                { 21, 5, 20,  6, 21,  6,
                  21, 7, 23,  7, 23,  8,
                  23, 8, 23,  7, 22,  7,
                  22, 6, 21,  4, 19,  5 };

            double desiredSunLong = termIndex * (Math.PI / 12);

            DateTime estimatedDateTime = new DateTime(year, trialMonths[termIndex], trialDays[termIndex]);
            double currentSunLong = Astronomy.GetSunLongitudeAtJulianDate(
                estimatedDateTime.UniversalDateTimeToJulianDate());

            //Console.WriteLine("========================================");
            //Console.WriteLine("Desired: {0} radians = {1} degrees", desiredSunLong, desiredSunLong.ToDegrees());

            //double offset = (desireSunLongitude - currentSunLongitude).ToDegrees() / degreePerDay;
            currentSunLong = Astronomy.GetSunLongitudeAtJulianDate(
                estimatedDateTime.UniversalDateTimeToJulianDate());

            var error = (currentSunLong - desiredSunLong).ToNormalizedArc();
            var direction = 1;
            if (error > Math.PI)
                direction = -1;

            //Console.WriteLine("Current: {0} radians = {1} degrees, direction = {2}",
            //    currentSunLong, currentSunLong.ToDegrees(), direction);

            // crossing-zero check: crossing zero if
            // |   increasing: currentSunLong - previousSunLong < 0
            // |   decreasing: currentSunLong - previousSunLong > 0
            // +-< both cases: (currentSunLong - previousSunLong)*direction < 0
            // if zero-crossing occurs: modify the longtitude to do the comparison in the next step
            //     currentSunLong = currentSunLong + 360*direction;
            //
            // exit condition
            // |   increasing:
            // |       if currentSunLong < desiredSunLong
            // |           continue increasing
            // |       else
            // |           change the direction
            // |   decreasing:
            // |       if currentSunLong > desiredSunLong
            // |           continue decreasing
            // |       else
            // |           change the direction
            // | / generalized (both increasing and decreasing cases):
            // | |     if (currentSunLong - desiredSunLong)*direction < 0
            // +-|         continue the current direction
            //   |     else
            //   \         change the direction

            double resolution = 1; // days
            double previousSunLong = currentSunLong;
            var count = 0;
            do
            {
                estimatedDateTime = estimatedDateTime.AddDays(resolution * direction);
                currentSunLong = Astronomy.GetSunLongitudeAtJulianDate(estimatedDateTime.UniversalDateTimeToJulianDate());
                // C < D
                double error1 = ToNormalizedArc(currentSunLong - desiredSunLong);
                double error2 = ToNormalizedArc(desiredSunLong + 2 * Math.PI - currentSunLong);

                if (error1 > error && error2 > error)
                {
                    direction = direction * (-1);
                    resolution = resolution / 2;
                }

                error = error1;
                if (error2 > error1)
                    error = error1;
                else
                    error = error2;

                //Console.WriteLine("----C = {0:0.0000} deg = {1:0.0000} rad | direction = {8} | resolution = {9} \r\n" +
                //                  "   E1 = {2:0.0000} deg = {3:0.0000} rad \r\n" +
                //                  "   E2 = {4:0.0000} deg = {5:0.0000} rad \r\n" +
                //                  "    E = {6:0.0000} deg = {7:0.0000} rad \r\n",
                //        currentSunLong.ToDegrees(), currentSunLong,
                //        error1.ToDegrees(), error1,
                //        error2.ToDegrees(), error2,
                //        error.ToDegrees(), error, direction, resolution);

                //do
                //{
                //    estimatedDateTime = estimatedDateTime.AddDays(resolution * direction);
                //    currentSunLong = Astronomy.GetSunLongitudeAtJulianDate(estimatedDateTime.UniversalDateTimeToJulianDate());

                //    // crossing-zero (full circle) check
                //    Console.WriteLine("    Current = {0:0.0000} deg | Previous = {1:0.0000} deg | direction = {2} | IsCrossingZero = {3}",
                //            currentSunLong.ToDegrees(), previousSunLong.ToDegrees(), direction, (currentSunLong - previousSunLong) * direction < 0);
                //    if ((currentSunLong - previousSunLong) * direction < 0)
                //    {
                //        previousSunLong = currentSunLong; // take snapshot before modifying
                //        currentSunLong = currentSunLong + Math.PI * 2 * direction;
                //    }
                //    else
                //    {
                //        previousSunLong = currentSunLong;
                //    }

                //    Console.WriteLine("    Current = {0:0.0000} deg | Current - Desired = {1:0.0000} deg | direction = {2} | resolution = {3}",
                //            currentSunLong.ToDegrees(), (currentSunLong - desiredSunLong).ToDegrees(), direction, resolution);
                //}
                //while ((currentSunLong - desiredSunLong) * direction < 0);
                //direction = direction * (-1); // change direction
                //resolution = resolution / 10;
                count++;
            } while (resolution > (1f / 86400) && Math.Abs(error) > 0.00001);
            //Console.WriteLine(count);
            //Console.WriteLine("    Current: {0} radians = {1} degrees, direction = {2}",
            //    currentSunLong, currentSunLong.ToDegrees(), direction);
            return estimatedDateTime;
        }

        public static double ToNormalizedArc(this double arc)
        {
            // e.g: arc =  2 deg -> thisArc = 2 deg, returns 2
            //      arc = -2 deg -> thisArc = 2 deg, returns 2
            //      arc = 362 deg -> thisArc 
            double thisAcr = Math.Abs(arc);
            while (thisAcr > (2 * Math.PI))
                thisAcr -= (2 * Math.PI);
            return thisAcr;
        }
    }
}
