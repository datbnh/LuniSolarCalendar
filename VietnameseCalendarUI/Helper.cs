/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    public static class Helper
    {
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

        public static Color GetBackgroundFromHue(double h)
        {
            return ColorFromHSV(h, BACKGROUND_S, BACKGROUND_V);
        }

        public static Color GetForegroundFromHue(double h)
        {
            return ColorFromHSV(h, FOREGROUND_S, FOREGROUND_V);
        }

        public static readonly double BACKGROUND_S = 0.1;
        public static readonly double BACKGROUND_V = 1.0;
        public static readonly double FOREGROUND_S = 1.0;
        public static readonly double FOREGROUND_V = 0.3;

        public static readonly double RED_HUE = 0;
        public static readonly double GREEN_HUE = 120;
        public static readonly double BLUE_HUE = 240;
    }
}
