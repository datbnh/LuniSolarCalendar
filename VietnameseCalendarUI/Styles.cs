﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VietnameseCalendarUI
{
    public static class BorderStyles
    {
        public static BorderStyle Normal
        {
            get
            {
                return new BorderStyle
                {
                    BorderColor = Brushes.DarkGray,
                    BorderThickness = new Thickness(0, 0, 1, 1),
                    Padding = new Thickness(2, 2, 1, 1),
                };
            }
        }

        public static BorderStyle Selected
        {
            get
            {
                return new BorderStyle
                {
                    BorderColor = Brushes.DodgerBlue,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    Padding = new Thickness(1, 1, 0, 0),
                };
            }
        }

        public static BorderStyle HotTrack
        {
            get
            {
                return new BorderStyle
                {
                    BorderColor = Brushes.LightBlue,
                    BorderThickness = new Thickness(1, 1, 2, 2),
                    Padding = new Thickness(1, 1, 0, 0),
                };
            }
        }
    }

    public static class FaceStyles
    {
        public static FaceStyle Normal
        {
            get
            {
                return new FaceStyle
                {
                    Foreground = Brushes.Black,
                    Backcolor = Brushes.White,

                    FontWeight = FontWeights.Medium,
                    SolarFontSize = 24f,
                    LunarFontSize = 16f,
                    LabelFontSize = 12f,
                };
            }
        }

        public static FaceStyle Saturday
        {
            get
            {
                FaceStyle style = Normal;
                style.Foreground = Brushes.DarkBlue;
                return style;
            }
        }

        public static FaceStyle Sunday
        {
            get
            {
                FaceStyle style = Normal;
                style.Foreground = Brushes.DarkRed;
                return style;
            }
        }

        public static FaceStyle Today
        {
            get
            {
                FaceStyle style = Normal;
                style.Backcolor = Brushes.LightGreen;
                style.FontWeight = FontWeights.SemiBold;
                return style;
            }
        }

        public static FaceStyle Special
        {
            get
            {
                FaceStyle style = Normal;
                style.Backcolor = Brushes.MistyRose;
                style.Foreground = Brushes.Crimson;
                return style;
            }
        }

        public static FaceStyle GrayedOut
        {
            get
            {
                FaceStyle style = Normal;
                style.Foreground = Brushes.DarkGray;
                style.FontWeight = FontWeights.Light;
                style.SolarFontSize = 20f;
                style.LunarFontSize = 14f;
                style.LabelFontSize = 10f;
                return style;
            }
        }
    }

    public class FaceStyle
    {
        public Brush Foreground { get; internal set; }
        public Brush Backcolor { get; internal set; }
        public FontWeight FontWeight { get; internal set; }
        public double SolarFontSize { get; internal set; }
        public double LunarFontSize { get; internal set; }
        public double LabelFontSize { get; internal set; }
    }

    public class BorderStyle
    {
        public Brush BorderColor { get; internal set; }
        public Thickness BorderThickness { get; internal set; }
        public Thickness Padding { get; internal set; }
    }
}