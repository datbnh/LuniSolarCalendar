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
        /// <summary>
        /// hue form 0 to 360, saturation from 0 to 1, value from 0 to 1.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            return ColorFromAHSV(255, hue, saturation, value);
        }

        /// <summary>
        /// alpha from 0 to 255, hue form 0 to 360, saturation from 0 to 1, value from 0 to 1.
        /// </summary>
        /// <param name="alpha">0 to 255</param>
        /// <param name="hue">0 to 360</param>
        /// <param name="saturation">0 to 1</param>
        /// <param name="value">0 to 1</param>
        /// <returns></returns>
        public static Color ColorFromAHSV(byte alpha, double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            byte v = (byte)Convert.ToInt32(value);
            byte p = (byte)Convert.ToInt32(value * (1 - saturation));
            byte q = (byte)Convert.ToInt32(value * (1 - f * saturation));
            byte t = (byte)Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(alpha, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(alpha, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(alpha, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(alpha, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(alpha, t, p, v);
            else
                return Color.FromArgb(alpha, v, p, q);
        }

        public static void ColorToAHSV(Color color, out byte alpha, out double hue, out double saturation, out double value)
        {
            System.Drawing.Color sysColor =
                System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            alpha = color.A;
            hue = sysColor.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
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
