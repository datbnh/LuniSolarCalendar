/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    public class ThemeColor : INotifyPropertyChanged
    {
        private Brush border;
        private Brush selectedBorder;
        private Brush background;
        private Brush foreground;
        private Brush normalBackground;
        private Brush mouseOverBackground;
        private Brush highlightForeground;
        private Brush saturdayBackground;
        private Brush saturdayForeground;
        private Brush sundayBackground;
        private Brush sundayForeground;
        private Brush specialLevel1Background;
        private Brush specialLevel1Foreground;
        private Brush specialLevel2Background;
        private Brush specialLevel2Foreground;
        private Brush specialLevel3Background;
        private Brush specialLevel3Foreground;
        private Brush grayedOutBackground;
        private Brush grayedOutForeground;

        public Brush Border { get => border; set { if (value != border) { border = value; OnPropertyChanged("Border"); } } }
        public Brush SelectedBorder { get => selectedBorder; set { if (value != selectedBorder) { selectedBorder = value; OnPropertyChanged("SelectedBorder"); } } }
        public Brush Background { get => background; set { if (value != background) { background = value; OnPropertyChanged("Background"); } } }
        public Brush Foreground { get => foreground; set { if (value != foreground) { foreground = value; OnPropertyChanged("Foreground"); } } }
        public Brush NormalBackground { get => normalBackground; set { if (value != normalBackground) { normalBackground = value; OnPropertyChanged("NormalBackground"); } } }
        public Brush MouseOverBackground { get => mouseOverBackground; set { if (value != mouseOverBackground) { mouseOverBackground = value; OnPropertyChanged("MouseOverBackground"); } } }
        public Brush HighlightForeground { get => highlightForeground; set { if (value != highlightForeground) { highlightForeground = value; OnPropertyChanged("HighlightForeground"); } } }
        public Brush SaturdayBackground { get => saturdayBackground; set { if (value != saturdayBackground) { saturdayBackground = value; OnPropertyChanged("SaturdayBackground"); } } }
        public Brush SaturdayForeground { get => saturdayForeground; set { if (value != saturdayForeground) { saturdayForeground = value; OnPropertyChanged("SaturdayForeground"); } } }
        public Brush SundayBackground { get => sundayBackground; set { if (value != sundayBackground) { sundayBackground = value; OnPropertyChanged("SundayBackground"); } } }
        public Brush SundayForeground { get => sundayForeground; set { if (value != sundayForeground) { sundayForeground = value; OnPropertyChanged("SundayForeground"); } } }
        public Brush SpecialLevel1Background { get => specialLevel1Background; set { if (value != specialLevel1Background) { specialLevel1Background = value; OnPropertyChanged("SpecialLevel1Background"); } } }
        public Brush SpecialLevel1Foreground { get => specialLevel1Foreground; set { if (value != specialLevel1Foreground) { specialLevel1Foreground = value; OnPropertyChanged("SpecialLevel1Foreground"); } } }
        public Brush SpecialLevel2Background { get => specialLevel2Background; set { if (value != specialLevel2Background) { specialLevel2Background = value; OnPropertyChanged("SpecialLevel2Background"); } } }
        public Brush SpecialLevel2Foreground { get => specialLevel2Foreground; set { if (value != specialLevel2Foreground) { specialLevel2Foreground = value; OnPropertyChanged("SpecialLevel2Foreground"); } } }
        public Brush SpecialLevel3Background { get => specialLevel3Background; set { if (value != specialLevel3Background) { specialLevel3Background = value; OnPropertyChanged("SpecialLevel3Background"); } } }
        public Brush SpecialLevel3Foreground { get => specialLevel3Foreground; set { if (value != specialLevel3Foreground) { specialLevel3Foreground = value; OnPropertyChanged("SpecialLevel3Foreground"); } } }
        public Brush GrayedOutBackground { get => grayedOutBackground; set { if (value != grayedOutBackground) { grayedOutBackground = value; OnPropertyChanged("GrayedOutBackground"); } } }
        public Brush GrayedOutForeground { get => grayedOutForeground; set { if (value != grayedOutForeground) { grayedOutForeground = value; OnPropertyChanged("GrayedOutForeground"); } } }

        public ThemeColor ()
        {
            Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
            NormalBackground = new SolidColorBrush(Color.FromArgb(24, 0, 0, 0));
            MouseOverBackground = new SolidColorBrush(Helper.ColorFromAHSV(16, 0, 0, 0));
            SpecialLevel1Background = new SolidColorBrush(Helper.ColorFromAHSV(32, 0, 0.50, 0.95));
            SpecialLevel2Background = new SolidColorBrush(Helper.ColorFromAHSV(32, 0, 0.50, 0.55));
            SpecialLevel3Background = new SolidColorBrush(Helper.ColorFromAHSV(32, 0, 0.50, 0.35));

            Foreground = Brushes.White;
            SaturdayForeground = new SolidColorBrush(Helper.ColorFromHSV(206, .85, .94));
            SundayForeground = new SolidColorBrush(Helper.ColorFromHSV(13, .86, .99));
            SpecialLevel1Foreground = new SolidColorBrush(Helper.ColorFromHSV(43, 0.90, 0.99));
            SpecialLevel2Foreground = new SolidColorBrush(Helper.ColorFromHSV(43, 0.90, 0.99));
            SpecialLevel3Foreground = new SolidColorBrush(Helper.ColorFromHSV(43, 0.90, 0.99));
            GrayedOutForeground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.6));

            Border = null; //new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
            SelectedBorder = new SolidColorBrush(Helper.ColorFromAHSV(128, 0, 0, 0.2));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public static class ThemeColors
    {
        public static ThemeColor Light
        {
            get
            {
                ThemeColor themeColor = new ThemeColor
                {
                    Background = Brushes.White,
                    MouseOverBackground = Brushes.Cornsilk,
                    SpecialLevel1Background = Brushes.Crimson,
                    SpecialLevel2Background = Brushes.LightGreen,
                    SpecialLevel3Background = Brushes.LightPink,

                    Foreground = Brushes.Black,
                    SaturdayForeground = Brushes.DarkBlue,
                    SundayForeground = Brushes.DarkRed,
                    SpecialLevel1Foreground = Brushes.DarkSalmon,
                    SpecialLevel2Foreground = Brushes.DarkTurquoise,
                    SpecialLevel3Foreground = Brushes.DarkViolet,
                    GrayedOutForeground = Brushes.LightGray,

                    Border = Brushes.LightGray,
                    SelectedBorder = Brushes.LightBlue
                };

                themeColor.SaturdayBackground = themeColor.Background;
                themeColor.SundayBackground = themeColor.Background;
                themeColor.GrayedOutBackground = themeColor.Background;
                themeColor.HighlightForeground = themeColor.Foreground;

                return themeColor;
            }
        }


        public static ThemeColor LightSemiTransparent
        {
            get
            {
                ThemeColor themeColor = new ThemeColor
                {
                    Background = new SolidColorBrush(Color.FromArgb(32, 255, 255, 255)),
                    MouseOverBackground = Brushes.Cornsilk,
                    SpecialLevel1Background = Brushes.Crimson,
                    SpecialLevel2Background = Brushes.LightGreen,
                    SpecialLevel3Background = Brushes.LightPink,

                    Foreground = Brushes.Black,
                    SaturdayForeground = Brushes.DarkBlue,
                    SundayForeground = Brushes.DarkRed,
                    SpecialLevel1Foreground = Brushes.DarkSalmon,
                    SpecialLevel2Foreground = Brushes.DarkTurquoise,
                    SpecialLevel3Foreground = Brushes.DarkViolet,
                    GrayedOutForeground = Brushes.LightGray,

                    Border = null, //Brushes.LightGray,
                    SelectedBorder = Brushes.LightBlue
                };

                themeColor.SaturdayBackground = themeColor.Background;
                themeColor.SundayBackground = themeColor.Background;
                themeColor.GrayedOutBackground = themeColor.Background;
                themeColor.HighlightForeground = themeColor.Foreground;

                return themeColor;
            }
        }


        public static ThemeColor Dark
        {
            get
            {
                ThemeColor themeColor = new ThemeColor()
                {
                    Background = Brushes.Black,
                    MouseOverBackground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                    SpecialLevel1Background = new SolidColorBrush(Helper.ColorFromHSV(15, 1, 0.2)), // red
                    SpecialLevel2Background = new SolidColorBrush(Helper.ColorFromHSV(45, 1, 0.2)), // green
                    SpecialLevel3Background = new SolidColorBrush(Helper.ColorFromHSV(215, 1, 0.2)), // blue

                    Foreground = Brushes.White,
                    SaturdayForeground = new SolidColorBrush(Helper.ColorFromHSV(195, 1, 1)),
                    SundayForeground = new SolidColorBrush(Helper.ColorFromHSV(345, 1, 1)),
                    SpecialLevel1Foreground = new SolidColorBrush(Helper.ColorFromHSV(15, 1, 1)),
                    SpecialLevel2Foreground = new SolidColorBrush(Helper.ColorFromHSV(45, 1, 1)),
                    SpecialLevel3Foreground = new SolidColorBrush(Helper.ColorFromHSV(215, 1, 1)),
                    GrayedOutForeground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.3)),

                    Border = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                    SelectedBorder = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.2)),
                };
                
                themeColor.SaturdayBackground = themeColor.Background;
                themeColor.SundayBackground = themeColor.Background;
                themeColor.GrayedOutBackground = themeColor.Background;
                themeColor.HighlightForeground = themeColor.Foreground;

                return themeColor;
            }
        }

        public static ThemeColor DarkSemiTransparent
        {
            get
            {
                ThemeColor themeColor = new ThemeColor()
                {
                    Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0)),
                    NormalBackground = new SolidColorBrush(Color.FromArgb(24, 0, 0, 0)),
                    MouseOverBackground = new SolidColorBrush(Helper.ColorFromAHSV(16, 0, 0, 0)),
                    SpecialLevel1Background = new SolidColorBrush(Helper.ColorFromAHSV(32, 0, 0.50, 0.95)),
                    SpecialLevel2Background = new SolidColorBrush(Helper.ColorFromAHSV(32, 0, 0.50, 0.55)), 
                    SpecialLevel3Background = new SolidColorBrush(Helper.ColorFromAHSV(32, 0, 0.50, 0.35)), 

                    Foreground = Brushes.White,
                    SaturdayForeground = new SolidColorBrush(Helper.ColorFromHSV(206, .85, .94)),
                    SundayForeground = new SolidColorBrush(Helper.ColorFromHSV(13, .86, .99)),
                    SpecialLevel1Foreground = new SolidColorBrush(Helper.ColorFromHSV(43, 0.90, 0.99)),
                    SpecialLevel2Foreground = new SolidColorBrush(Helper.ColorFromHSV(43, 0.90, 0.99)),
                    SpecialLevel3Foreground = new SolidColorBrush(Helper.ColorFromHSV(43, 0.90, 0.99)),
                    GrayedOutForeground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.6)),

                    Border = null, //new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                    SelectedBorder = new SolidColorBrush(Helper.ColorFromAHSV(128, 0, 0, 0.2)),
                };

                themeColor.SaturdayBackground = themeColor.NormalBackground;
                themeColor.SundayBackground = themeColor.NormalBackground;
                themeColor.GrayedOutBackground = null;//themeColor.Background;
                themeColor.HighlightForeground = themeColor.Foreground;

                return themeColor;
            }
        }

        public static ThemeColor CreateSemiTransparentThemeColor(byte baseAlpha, 
            double normalForegroundHue = 0, 
            double saturdayForegroundHue = 206, 
            double sundayForegroundHue = 13,
            double specialBackgroundHue = 0, 
            double specialForegroundHue = 43)
        {
            byte mouseOverAlpha = (byte)(baseAlpha * 2 / 3 % 255);
            byte specialAlpha = (byte)(baseAlpha * 2 % 255);
            ThemeColor themeColor = new ThemeColor()
            {
                Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0)),
                NormalBackground = new SolidColorBrush(Color.FromArgb(baseAlpha, 0, 0, 0)),
                MouseOverBackground = new SolidColorBrush(Helper.ColorFromAHSV(mouseOverAlpha, 0, 0, 0)),
                SpecialLevel1Background = new SolidColorBrush(Helper.ColorFromAHSV(specialAlpha, 0, specialBackgroundHue, 0.95)),
                SpecialLevel2Background = new SolidColorBrush(Helper.ColorFromAHSV(specialAlpha, 0, specialBackgroundHue, 0.55)),
                SpecialLevel3Background = new SolidColorBrush(Helper.ColorFromAHSV(specialAlpha, 0, specialBackgroundHue, 0.35)),

                Foreground = Brushes.White,
                SaturdayForeground = new SolidColorBrush(Helper.ColorFromHSV(saturdayForegroundHue, .85, .94)),
                SundayForeground = new SolidColorBrush(Helper.ColorFromHSV(sundayForegroundHue, .86, .99)),
                SpecialLevel1Foreground = new SolidColorBrush(Helper.ColorFromHSV(specialForegroundHue, 0.90, 0.99)),
                SpecialLevel2Foreground = new SolidColorBrush(Helper.ColorFromHSV(specialForegroundHue, 0.90, 0.99)),
                SpecialLevel3Foreground = new SolidColorBrush(Helper.ColorFromHSV(specialForegroundHue, 0.90, 0.99)),
                GrayedOutForeground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.6)),

                Border = null, //new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                SelectedBorder = new SolidColorBrush(Helper.ColorFromAHSV(128, 0, 0, 0.2)),
            };

            themeColor.SaturdayBackground = themeColor.NormalBackground;
            themeColor.SundayBackground = themeColor.NormalBackground;
            themeColor.GrayedOutBackground = null;//themeColor.Background;
            themeColor.HighlightForeground = themeColor.Foreground;

            return themeColor;
        }
    }

    public class ThemeSize
    {
        private double solarFontSize;
        private double lunarFontSize;
        private double labelFontSize;
        private double monthLabelFontSize;
        private double defaultFontSize;
        private FontWeight dayFontWeight;
    }

    public class Theme
    {
        public ThemeColor ThemeColor;
        public ThemeSize FontSizes;
        public Style DayTileStyle;
    }

    public static class Themes
    {
        
        public static Theme Light { get => CreateTheme(ThemeColors.Light, null); }
        public static Theme Dark { get => CreateTheme(ThemeColors.Dark, null); }
        public static Theme LightSemiTransparent { get => CreateTheme(ThemeColors.LightSemiTransparent, null); }
        public static Theme DarkSemiTransparent { get => CreateTheme(ThemeColors.DarkSemiTransparent, null); }

        public static Theme CreateTheme(ThemeColor themeColor, ThemeSize themeSize)
        {
            Theme theme = new Theme()
            {
                ThemeColor = themeColor,
                FontSizes = themeSize,
            };

            theme.DayTileStyle = new Style();
            // Triggers
            theme.DayTileStyle.Triggers.Add(new Trigger {
                Property = AugustineCalendarDay.IsSelectedProperty, Value = true,
                Setters = { new Setter(AugustineCalendarDay.BorderBrushProperty, theme.ThemeColor.SelectedBorder), },
            });
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.Saturday, theme.ThemeColor.SaturdayBackground, theme.ThemeColor.SaturdayForeground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.Sunday, theme.ThemeColor.SundayBackground, theme.ThemeColor.SundayForeground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel3, theme.ThemeColor.SpecialLevel3Background, theme.ThemeColor.SpecialLevel3Foreground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel2, theme.ThemeColor.SpecialLevel2Background, theme.ThemeColor.SpecialLevel2Foreground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel1, theme.ThemeColor.SpecialLevel1Background, theme.ThemeColor.SpecialLevel1Foreground));
            theme.DayTileStyle.Triggers.Add(new Trigger
            {
                Property = AugustineCalendarDay.IsMouseOverProperty, Value = true,
                Setters = { new Setter(AugustineCalendarDay.BackgroundProperty, theme.ThemeColor.MouseOverBackground), },
            });
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.GrayedOut, theme.ThemeColor.GrayedOutBackground, theme.ThemeColor.GrayedOutForeground));
            // Setters
            theme.DayTileStyle.Setters.Add(new Setter(AugustineCalendarDay.PaddingProperty, new Thickness(0)));
            theme.DayTileStyle.Setters.Add(new Setter(AugustineCalendarDay.MarginProperty, new Thickness(0)));
            theme.DayTileStyle.Setters.Add(new Setter(AugustineCalendarDay.BorderThicknessProperty, new Thickness(0, 0, 1, 1)));
            theme.DayTileStyle.Setters.Add(new Setter(AugustineCalendarDay.BorderBrushProperty, theme.ThemeColor.Border));
            theme.DayTileStyle.Setters.Add(new Setter(AugustineCalendarDay.BackgroundProperty, theme.ThemeColor.NormalBackground));
            theme.DayTileStyle.Setters.Add(new Setter(AugustineCalendarDay.ForegroundProperty, theme.ThemeColor.Foreground));

            return theme;
        }

        private static Trigger CreateDayTypeTrigger(DayTypes dayType, Brush background, Brush foreground)
        {
            return new Trigger()
            {
                Property = AugustineCalendarDay.DayTypeProperty,
                Value = dayType,
                Setters =
                {
                    new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  foreground, },
                    new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  background, },
                },
            };
        }
    }
}
