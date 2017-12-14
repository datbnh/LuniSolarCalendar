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
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    [DataContract]
    public class ThemeColor : INotifyPropertyChanged, ICloneable
    {

        private Brush border;
        [DataMember(Name = "Border")]
        internal string SBorder {
            get => GetColorStringFromBrush(Border);
            set => Border = GetBrushFromColorString(value); }

        private Brush selectedBorder;
        [DataMember(Name = "SelectedBorder")]
        internal string SSelectedBorder
        {
            get => GetColorStringFromBrush(SelectedBorder);
            set => SelectedBorder = GetBrushFromColorString(value);
        }

        private Brush background;
        [DataMember(Name = "Background")]
        internal string SBackground {
            get => GetColorStringFromBrush(Background);
            set => Background = GetBrushFromColorString(value); }

        private Brush foreground;
        [DataMember(Name = "Foreground")]
        internal string SForeground
        {
            get => GetColorStringFromBrush(Foreground);
            set => Foreground = GetBrushFromColorString(value);
        }

        private Brush normalBackground;
        [DataMember(Name = "NormalBackground")]
        internal string SNormalBackground
        {
            get => GetColorStringFromBrush(NormalBackground);
            set => NormalBackground = GetBrushFromColorString(value);
        }

        private Brush mouseOverBackground;
        [DataMember(Name = "MouseOverBackground")]
        internal string SMouseOverBackground
        {
            get => GetColorStringFromBrush(MouseOverBackground);
            set => MouseOverBackground = GetBrushFromColorString(value);
        }

        private Brush highlightForeground;
        [DataMember(Name = "HighlightForeground")]
        internal string SHighlightForeground
        {
            get => GetColorStringFromBrush(HighlightForeground);
            set => HighlightForeground = GetBrushFromColorString(value);
        }

        private Brush saturdayBackground;
        [DataMember(Name = "SaturdayBackground")]
        internal string SSaturdayBackground
        {
            get => GetColorStringFromBrush(SaturdayBackground);
            set => SaturdayBackground = GetBrushFromColorString(value);
        }

        private Brush saturdayForeground;
        [DataMember(Name = "SaturdayForeground")]
        internal string SSaturdayForeground
        {
            get => GetColorStringFromBrush(SaturdayForeground);
            set => SaturdayForeground = GetBrushFromColorString(value);
        }

        private Brush sundayBackground;
        [DataMember(Name = "SundayBackground")]
        internal string SSundayBackground
        {
            get => GetColorStringFromBrush(SundayBackground);
            set => SundayBackground = GetBrushFromColorString(value);
        }

        private Brush sundayForeground;
        [DataMember(Name = "SundayForeground")]
        internal string SSundayForeground
        {
            get => GetColorStringFromBrush(SundayForeground);
            set => SundayForeground = GetBrushFromColorString(value);
        }

        private Brush specialLevel1Background;
        [DataMember(Name = "SpecialLevel1Background")]
        internal string SSpecialLevel1Background
        {
            get => GetColorStringFromBrush(SpecialLevel1Background);
            set => SpecialLevel1Background = GetBrushFromColorString(value);
        }

        private Brush specialLevel1Foreground;
        [DataMember(Name = "SpecialLevel1Foreground")]
        internal string SSpecialLevel1Foreground
        {
            get => GetColorStringFromBrush(SpecialLevel1Foreground);
            set => SpecialLevel1Foreground = GetBrushFromColorString(value);
        }

        private Brush specialLevel2Background;
        [DataMember(Name = "SpecialLevel2Background")]
        internal string SSpecialLevel2Background
        {
            get => GetColorStringFromBrush(SpecialLevel2Background);
            set => SpecialLevel2Background = GetBrushFromColorString(value);
        }

        private Brush specialLevel2Foreground;
        [DataMember(Name = "SpecialLevel2Foreground")]
        internal string SSpecialLevel2Foreground
        {
            get => GetColorStringFromBrush(SpecialLevel2Foreground);
            set => SpecialLevel2Foreground = GetBrushFromColorString(value);
        }

        private Brush specialLevel3Background;
        [DataMember(Name = "SpecialLevel3Background")]
        internal string SSpecialLevel3Background
        {
            get => GetColorStringFromBrush(SpecialLevel3Background);
            set => SpecialLevel3Background = GetBrushFromColorString(value);
        }

        private Brush specialLevel3Foreground;
        [DataMember(Name = "SpecialLevel3Foreground")]
        internal string SSpecialLevel3Foreground
        {
            get => GetColorStringFromBrush(SpecialLevel3Foreground);
            set => SpecialLevel3Foreground = GetBrushFromColorString(value);
        }

        private Brush grayedOutBackground;
        [DataMember(Name = "GrayedOutBackground")]
        internal string SGrayedOutBackground
        {
            get => GetColorStringFromBrush(GrayedOutBackground);
            set => GrayedOutBackground = GetBrushFromColorString(value);
        }

        private Brush grayedOutForeground;
        [DataMember(Name = "GrayedOutForeground")]
        internal string SGrayedOutForeground
        {
            get => GetColorStringFromBrush(GrayedOutForeground);
            set => GrayedOutForeground = GetBrushFromColorString(value);
        }

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

        public ThemeColor()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        public object Clone() 
            => this.MemberwiseClone();

        internal static ThemeColor LoadFromFile(string fileName)
        {
            ThemeColor deserialized;
            try { deserialized = Serializer.DeSerializeObject<ThemeColor>(fileName); }
            catch (Exception ex) { throw new Exception("Cannot parse configuration from file.", ex); }
            return deserialized;
        }

        internal void SaveToFile(string fileName)
        {
            try { Serializer.SerializeObject<ThemeColor>(this, fileName); }
            catch (Exception ex) { throw new Exception("Cannot save current configuration to file.", ex); }
        }

        internal string GetColorStringFromBrush(Brush brush)
        {
            if (brush == null || !(brush is SolidColorBrush))
                return "#00FFFFFF";
            return ((SolidColorBrush)brush).Color.ToString();
        }

        private SolidColorBrush GetBrushFromColorString(string color)
        {
            Color c;
            try { c = (Color)ColorConverter.ConvertFromString(color); }
            catch (Exception) { c = Color.FromArgb(0, 255, 255, 255); }
            return new SolidColorBrush(c);
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
                    SBackground              = "#FFFFFFFF",
                    SBorder                  = "#00FFFFFF",
                    SForeground              = "#FF000000",
                    SGrayedOutBackground     = "#00000000",
                    SGrayedOutForeground     = "#40808080",
                    SHighlightForeground     = "#FFFFFFFF",
                    SMouseOverBackground     = "#10000000",
                    SNormalBackground        = "#0A000000",
                    SSaturdayBackground      = "#0A000000",
                    SSaturdayForeground      = "#FF2196F3",
                    SSelectedBorder          = "#69696969",
                    SSpecialLevel1Background = "#FFFCE4EC",
                    SSpecialLevel1Foreground = "#FFE91E63",
                    SSpecialLevel2Background = "#FFFCE4EC",
                    SSpecialLevel2Foreground = "#FFE91E63",
                    SSpecialLevel3Background = "#FFFCE4EC",
                    SSpecialLevel3Foreground = "#FFE91E63",
                    SSundayBackground        = "#0A000000",
                    SSundayForeground        = "#FFF44336",
                };
                return themeColor;
            }
        }


        public static ThemeColor LightSemiTransparent
        {
            get
            {
                ThemeColor themeColor = new ThemeColor
                {
                    SBackground              = "#60000000",
                    SBorder                  = "#00FFFFFF",
                    SForeground              = "#FFFFFFFF",
                    SGrayedOutBackground     = "#00000000",
                    SGrayedOutForeground     = "#40808080",
                    SHighlightForeground     = "#FFFFFFFF",
                    SMouseOverBackground     = "#20FFFFFF",
                    SNormalBackground        = "#0AFFFFFF",
                    SSaturdayBackground      = "#0AFFFFFF",
                    SSaturdayForeground      = "#FF2196F3",
                    SSelectedBorder          = "#69696969",
                    SSpecialLevel1Background = "#10F57F17",
                    SSpecialLevel1Foreground = "#FFFFEB3B",
                    SSpecialLevel2Background = "#10F57F17",
                    SSpecialLevel2Foreground = "#FFFFEB3B",
                    SSpecialLevel3Background = "#10F57F17",
                    SSpecialLevel3Foreground = "#FFFFEB3B",
                    SSundayBackground        = "#0AFFFFFF",
                    SSundayForeground        = "#FFF44336",
                };
                return themeColor;
            }
        }


        public static ThemeColor Dark
        {
            get
            {
                ThemeColor themeColor = new ThemeColor()
                {
                    SBackground              = "#FF000000",
                    SBorder                  = "#00FFFFFF",
                    SForeground              = "#FFFFFFFF",
                    SGrayedOutBackground     = "#00000000",
                    SGrayedOutForeground     = "#40808080",
                    SHighlightForeground     = "#FFFFFFFF",
                    SMouseOverBackground     = "#20FFFFFF",
                    SNormalBackground        = "#0AFFFFFF",
                    SSaturdayBackground      = "#0AFFFFFF",
                    SSaturdayForeground      = "#FF2196F3",
                    SSelectedBorder          = "#69696969",
                    SSpecialLevel1Background = "#10F57F17",
                    SSpecialLevel1Foreground = "#FFFFEB3B",
                    SSpecialLevel2Background = "#10F57F17",
                    SSpecialLevel2Foreground = "#FFFFEB3B",
                    SSpecialLevel3Background = "#10F57F17",
                    SSpecialLevel3Foreground = "#FFFFEB3B",
                    SSundayBackground        = "#0AFFFFFF",
                    SSundayForeground        = "#FFF44336",
                };
                return themeColor;
            }
        }

        public static ThemeColor DarkSemiTransparent
        {
            get
            {
                ThemeColor themeColor = new ThemeColor()
                {
                    SBackground              = "#10FFFFFF",
                    SBorder                  = "#00FFFFFF",
                    SForeground              = "#FF000000",
                    SGrayedOutBackground     = "#00000000",
                    SGrayedOutForeground     = "#40808080",
                    SHighlightForeground     = "#FFFFFFFF",
                    SMouseOverBackground     = "#10000000",
                    SNormalBackground        = "#0A000000",
                    SSaturdayBackground      = "#0A000000",
                    SSaturdayForeground      = "#FF1A237E",
                    SSelectedBorder          = "#69696969",
                    SSpecialLevel1Background = "#10FFECB3",
                    SSpecialLevel1Foreground = "#FFFF6F00",
                    SSpecialLevel2Background = "#10FFECB3",
                    SSpecialLevel2Foreground = "#FFFF6F00",
                    SSpecialLevel3Background = "#10FFECB3",
                    SSpecialLevel3Foreground = "#FFFF6F00",
                    SSundayBackground        = "#0A000000",
                    SSundayForeground        = "#FFB71C1C",
                };
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

        public static Theme CreateTheme(ThemeColor themeColor, ThemeSize themeSize)
        {
            Theme theme = new Theme()
            {
                ThemeColor = themeColor,
                FontSizes = themeSize,
            };

            theme.DayTileStyle = new Style();
            // Triggers
            theme.DayTileStyle.Triggers.Add(new Trigger
            {
                Property = AugustineCalendarDay.IsSelectedProperty,
                Value = true,
                Setters = {
                    new Setter(AugustineCalendarDay.PaddingProperty, new Thickness(1, 1, 0, 0)),
                    new Setter(AugustineCalendarDay.BorderThicknessProperty, new Thickness(1, 1, 1, 1)),
                    new Setter(AugustineCalendarDay.BorderBrushProperty, theme.ThemeColor.SelectedBorder),
                },
            });
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.Saturday, theme.ThemeColor.SaturdayBackground, theme.ThemeColor.SaturdayForeground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.Sunday, theme.ThemeColor.SundayBackground, theme.ThemeColor.SundayForeground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel3, theme.ThemeColor.SpecialLevel3Background, theme.ThemeColor.SpecialLevel3Foreground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel2, theme.ThemeColor.SpecialLevel2Background, theme.ThemeColor.SpecialLevel2Foreground));
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel1, theme.ThemeColor.SpecialLevel1Background, theme.ThemeColor.SpecialLevel1Foreground));
            theme.DayTileStyle.Triggers.Add(new Trigger
            {
                Property = AugustineCalendarDay.IsMouseOverProperty,
                Value = true,
                Setters = { new Setter(AugustineCalendarDay.BackgroundProperty, theme.ThemeColor.MouseOverBackground), },
            });
            theme.DayTileStyle.Triggers.Add(CreateDayTypeTrigger(DayTypes.GrayedOut, theme.ThemeColor.GrayedOutBackground, theme.ThemeColor.GrayedOutForeground));
            // Setters
            theme.DayTileStyle.Setters.Add(new Setter(AugustineCalendarDay.PaddingProperty, new Thickness(2, 2, 0, 0)));
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

    public static class Themes
    {
        
        public static Theme Light { get => Theme.CreateTheme(ThemeColors.Light, null); }
        public static Theme Dark { get => Theme.CreateTheme(ThemeColors.Dark, null); }
        public static Theme LightSemiTransparent { get => Theme.CreateTheme(ThemeColors.LightSemiTransparent, null); }
        public static Theme DarkSemiTransparent { get => Theme.CreateTheme(ThemeColors.DarkSemiTransparent, null); }
      
    }
}
