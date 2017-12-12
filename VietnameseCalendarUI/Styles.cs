/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System.Windows;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    public class ThemeColor
    {
        public Brush BaseBorder;
        public Brush HighlightBorder;
        public Brush BaseBackground;
        public Brush BaseForeground;
        public Brush HighlightBackground;
        public Brush HighlightForeground;
        public Brush SaturdayBackground;
        public Brush SaturdayForeground;
        public Brush SundayBackground;
        public Brush SundayForeground;
        public Brush SpecialLevel1Background;
        public Brush SpecialLevel1Foreground;
        public Brush SpecialLevel2Background;
        public Brush SpecialLevel2Foreground;
        public Brush SpecialLevel3Background;
        public Brush SpecialLevel3Foreground;
        public Brush GrayedOutBackground;
        public Brush GrayedOutForeground;
    }

    public static class ThemeColors
    {
        public static ThemeColor Light
        {
            get
            {
                ThemeColor themeColor = new ThemeColor
                {
                    BaseBackground = Brushes.White,
                    HighlightBackground = Brushes.Cornsilk,
                    SpecialLevel1Background = Brushes.Crimson,
                    SpecialLevel2Background = Brushes.LightGreen,
                    SpecialLevel3Background = Brushes.LightPink,

                    BaseForeground = Brushes.Black,
                    SaturdayForeground = Brushes.DarkBlue,
                    SundayForeground = Brushes.DarkRed,
                    SpecialLevel1Foreground = Brushes.DarkSalmon,
                    SpecialLevel2Foreground = Brushes.DarkTurquoise,
                    SpecialLevel3Foreground = Brushes.DarkViolet,
                    GrayedOutForeground = Brushes.LightGray,

                    BaseBorder = Brushes.LightGray,
                    HighlightBorder = Brushes.LightBlue
                };

                themeColor.SaturdayBackground = themeColor.BaseBackground;
                themeColor.SundayBackground = themeColor.BaseBackground;
                themeColor.GrayedOutBackground = themeColor.BaseBackground;
                themeColor.HighlightForeground = themeColor.BaseForeground;

                return themeColor;
            }
        }


        public static ThemeColor LightTransparent
        {
            get
            {
                ThemeColor themeColor = new ThemeColor
                {
                    BaseBackground = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255)),
                    HighlightBackground = Brushes.Cornsilk,
                    SpecialLevel1Background = Brushes.Crimson,
                    SpecialLevel2Background = Brushes.LightGreen,
                    SpecialLevel3Background = Brushes.LightPink,

                    BaseForeground = Brushes.Black,
                    SaturdayForeground = Brushes.DarkBlue,
                    SundayForeground = Brushes.DarkRed,
                    SpecialLevel1Foreground = Brushes.DarkSalmon,
                    SpecialLevel2Foreground = Brushes.DarkTurquoise,
                    SpecialLevel3Foreground = Brushes.DarkViolet,
                    GrayedOutForeground = Brushes.LightGray,

                    BaseBorder = Brushes.LightGray,
                    HighlightBorder = Brushes.LightBlue
                };

                themeColor.SaturdayBackground = themeColor.BaseBackground;
                themeColor.SundayBackground = themeColor.BaseBackground;
                themeColor.GrayedOutBackground = themeColor.BaseBackground;
                themeColor.HighlightForeground = themeColor.BaseForeground;

                return themeColor;
            }
        }


        public static ThemeColor Dark
        {
            get
            {
                ThemeColor themeColor = new ThemeColor()
                {
                    BaseBackground = Brushes.Black,
                    HighlightBackground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                    SpecialLevel1Background = new SolidColorBrush(Helper.ColorFromHSV(15, 1, 0.2)), // red
                    SpecialLevel2Background = new SolidColorBrush(Helper.ColorFromHSV(45, 1, 0.2)), // green
                    SpecialLevel3Background = new SolidColorBrush(Helper.ColorFromHSV(215, 1, 0.2)), // blue

                    BaseForeground = Brushes.White,
                    SaturdayForeground = new SolidColorBrush(Helper.ColorFromHSV(195, 1, 1)),
                    SundayForeground = new SolidColorBrush(Helper.ColorFromHSV(345, 1, 1)),
                    SpecialLevel1Foreground = new SolidColorBrush(Helper.ColorFromHSV(15, 1, 1)),
                    SpecialLevel2Foreground = new SolidColorBrush(Helper.ColorFromHSV(45, 1, 1)),
                    SpecialLevel3Foreground = new SolidColorBrush(Helper.ColorFromHSV(215, 1, 1)),
                    GrayedOutForeground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.3)),

                    BaseBorder = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                    HighlightBorder = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.2)),
                };
                
                themeColor.SaturdayBackground = themeColor.BaseBackground;
                themeColor.SundayBackground = themeColor.BaseBackground;
                themeColor.GrayedOutBackground = themeColor.BaseBackground;
                themeColor.HighlightForeground = themeColor.BaseForeground;

                return themeColor;
            }
        }

        public static ThemeColor DarkTransparent
        {
            get
            {
                ThemeColor themeColor = new ThemeColor()
                {
                    BaseBackground = new SolidColorBrush(Color.FromArgb(1,0,0,0)),
                    HighlightBackground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                    SpecialLevel1Background = new SolidColorBrush(Helper.ColorFromHSV(15, 1, 0.2)), // red
                    SpecialLevel2Background = new SolidColorBrush(Helper.ColorFromHSV(45, 1, 0.2)), // green
                    SpecialLevel3Background = new SolidColorBrush(Helper.ColorFromHSV(215, 1, 0.2)), // blue

                    BaseForeground = Brushes.White,
                    SaturdayForeground = new SolidColorBrush(Helper.ColorFromHSV(195, 1, 1)),
                    SundayForeground = new SolidColorBrush(Helper.ColorFromHSV(345, 1, 1)),
                    SpecialLevel1Foreground = new SolidColorBrush(Helper.ColorFromHSV(15, 1, 1)),
                    SpecialLevel2Foreground = new SolidColorBrush(Helper.ColorFromHSV(45, 1, 1)),
                    SpecialLevel3Foreground = new SolidColorBrush(Helper.ColorFromHSV(215, 1, 1)),
                    GrayedOutForeground = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.3)),

                    BaseBorder = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.1)),
                    HighlightBorder = new SolidColorBrush(Helper.ColorFromHSV(0, 0, 0.2)),
                };

                themeColor.SaturdayBackground = themeColor.BaseBackground;
                themeColor.SundayBackground = themeColor.BaseBackground;
                themeColor.GrayedOutBackground = themeColor.BaseBackground;
                themeColor.HighlightForeground = themeColor.BaseForeground;

                return themeColor;
            }
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
        public static Theme Light
        {
            get
            {
                Theme theme = new Theme()
                {
                    ThemeColor = ThemeColors.Light,
                };
                theme.DayTileStyle = new Style()
                {
                    // lower trigger -> higher priority
                    Triggers =
                        {   new Trigger() { Property = AugustineCalendarDay.IsSelectedProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(1, 1, 2, 2), },
                                                        new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(1, 1, 0, 0),},
                                                        new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.Light.HighlightBorder, },
                                                      },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Saturday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Light.SaturdayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Light.SaturdayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Sunday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Light.SundayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Light.SundayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel3,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Light.SpecialLevel3Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Light.SpecialLevel3Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel2,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Light.SpecialLevel2Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Light.SpecialLevel2Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel1,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Light.SpecialLevel1Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Light.SpecialLevel1Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.IsMouseOverProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Light.HighlightBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.GrayedOut,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Light.GrayedOutForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Light.GrayedOutBackground, },
                                                        //new Setter() { Property = AugustineCalendarDay.FontWeightProperty, Value =  FontWeights.Light, },
                                                      },
                                          },
                        },
                    Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(0, 0, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(2, 2, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.MarginProperty, Value = new Thickness(0),},
                                    new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.Light.BaseBorder, },
                                    new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value = ThemeColors.Light.BaseBackground, },
                                    new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value = ThemeColors.Light.BaseForeground, },
                                  },
                };
                return theme;
            }
        }

        public static Theme LightTransparent
        {
            get
            {
                Theme theme = new Theme()
                {
                    ThemeColor = ThemeColors.LightTransparent,
                };
                theme.DayTileStyle = new Style()
                {
                    // lower trigger -> higher priority
                    Triggers =
                        {   new Trigger() { Property = AugustineCalendarDay.IsSelectedProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(1, 1, 2, 2), },
                                                        new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(1, 1, 0, 0),},
                                                        new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.LightTransparent.HighlightBorder, },
                                                      },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Saturday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.LightTransparent.SaturdayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.LightTransparent.SaturdayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Sunday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.LightTransparent.SundayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.LightTransparent.SundayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel3,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.LightTransparent.SpecialLevel3Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.LightTransparent.SpecialLevel3Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel2,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.LightTransparent.SpecialLevel2Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.LightTransparent.SpecialLevel2Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel1,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.LightTransparent.SpecialLevel1Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.LightTransparent.SpecialLevel1Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.IsMouseOverProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.LightTransparent.HighlightBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.GrayedOut,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.LightTransparent.GrayedOutForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.LightTransparent.GrayedOutBackground, },
                                                        //new Setter() { Property = AugustineCalendarDay.FontWeightProperty, Value =  FontWeights.LightTransparent, },
                                                      },
                                          },
                        },
                    Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(0, 0, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(2, 2, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.MarginProperty, Value = new Thickness(0),},
                                    new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.LightTransparent.BaseBorder, },
                                    new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value = ThemeColors.LightTransparent.BaseBackground, },
                                    new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value = ThemeColors.LightTransparent.BaseForeground, },
                                  },
                };
                return theme;
            }
        }

        public static Theme Dark
        {
            get
            {
                Theme theme = new Theme()
                {
                    ThemeColor = ThemeColors.Dark,
                };
                theme.DayTileStyle = new Style()
                {
                    // lower trigger -> higher priority
                    Triggers =
                        {   new Trigger() { Property = AugustineCalendarDay.IsSelectedProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(1, 1, 2, 2), },
                                                        new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(1, 1, 0, 0),},
                                                        new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.Dark.HighlightBorder, },
                                                      },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Saturday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Dark.SaturdayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Dark.SaturdayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Sunday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Dark.SundayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Dark.SundayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel3,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Dark.SpecialLevel3Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Dark.SpecialLevel3Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel2,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Dark.SpecialLevel2Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Dark.SpecialLevel2Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel1,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Dark.SpecialLevel1Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Dark.SpecialLevel1Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.IsMouseOverProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Dark.HighlightBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.GrayedOut,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.Dark.GrayedOutForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.Dark.GrayedOutBackground, },
                                                        //new Setter() { Property = AugustineCalendarDay.FontWeightProperty, Value =  FontWeights.Dark, },
                                                      },
                                          },
                        },
                    Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(0, 0, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(2, 2, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.MarginProperty, Value = new Thickness(0),},
                                    new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.Dark.BaseBorder, },
                                    new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value = ThemeColors.Dark.BaseBackground, },
                                    new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value = ThemeColors.Dark.BaseForeground, },
                                  },
                };
                return theme;
            }
        }

        public static Theme DarkTransparent
        {
            get
            {
                Theme theme = new Theme()
                {
                    ThemeColor = ThemeColors.DarkTransparent,
                };
                theme.DayTileStyle = new Style()
                {
                    // lower trigger -> higher priority
                    Triggers =
                        {   new Trigger() { Property = AugustineCalendarDay.IsSelectedProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(1, 1, 2, 2), },
                                                        new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(1, 1, 0, 0),},
                                                        new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.DarkTransparent.HighlightBorder, },
                                                      },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Saturday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.DarkTransparent.SaturdayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.DarkTransparent.SaturdayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.Sunday,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.DarkTransparent.SundayForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.DarkTransparent.SundayBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel3,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.DarkTransparent.SpecialLevel3Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.DarkTransparent.SpecialLevel3Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel2,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.DarkTransparent.SpecialLevel2Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.DarkTransparent.SpecialLevel2Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.SpecialLevel1,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.DarkTransparent.SpecialLevel1Foreground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.DarkTransparent.SpecialLevel1Background, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.IsMouseOverProperty, Value = true,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.DarkTransparent.HighlightBackground, }, },
                                          },
                            new Trigger() { Property = AugustineCalendarDay.DayTypeProperty, Value = DayTypes.GrayedOut,
                                            Setters = { new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value =  ThemeColors.DarkTransparent.GrayedOutForeground, },
                                                        new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value =  ThemeColors.DarkTransparent.GrayedOutBackground, },
                                                        //new Setter() { Property = AugustineCalendarDay.FontWeightProperty, Value =  FontWeights.DarkTransparent, },
                                                      },
                                          },
                        },
                    Setters = { new Setter() { Property = AugustineCalendarDay.BorderThicknessProperty, Value = new Thickness(0, 0, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.PaddingProperty, Value = new Thickness(2, 2, 1, 1),},
                                    new Setter() { Property = AugustineCalendarDay.MarginProperty, Value = new Thickness(0),},
                                    new Setter() { Property = AugustineCalendarDay.BorderBrushProperty, Value = ThemeColors.DarkTransparent.BaseBorder, },
                                    new Setter() { Property = AugustineCalendarDay.BackgroundProperty, Value = ThemeColors.DarkTransparent.BaseBackground, },
                                    new Setter() { Property = AugustineCalendarDay.ForegroundProperty, Value = ThemeColors.DarkTransparent.BaseForeground, },
                                  },
                };
                return theme;
            }
        }
    }
}
