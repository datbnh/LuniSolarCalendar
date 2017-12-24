/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;


namespace Augustine.VietnameseCalendar.UI
{
    [DataContract]
    public class Theme
    {
        [DataMember]
        public TextAndShadow TextAndShadow;
        [DataMember]
        public ThemeColor ThemeColor;
        [DataMember]
        public TextSize TextSize;

        private Style dayTileStyle;
        public Style DayTileStyle
        {
            get
            {
                if (dayTileStyle == null)
                    dayTileStyle = CreateDayTileStyle(ThemeColor);
                return dayTileStyle;
            }
        }

        public Theme(ThemeColor themeColor, TextSize themeSize, TextAndShadow textAndShadow)
        {
            ThemeColor = themeColor ?? ThemeColors.Light;
            TextSize = themeSize ?? new TextSize();
            TextAndShadow = textAndShadow;
        }

        public static Style CreateDayTileStyle(ThemeColor themeColor)
        {
            var style = new Style();
            // Triggers
            style.Triggers.Add(new Trigger
            {
                Property = DayTile.IsSelectedProperty,
                Value = true,
                Setters = {
                    new Setter(DayTile.PaddingProperty, new Thickness(1, 1, 0, 0)),
                    new Setter(DayTile.BorderThicknessProperty, new Thickness(1, 1, 1, 1)),
                    new Setter(DayTile.BorderBrushProperty, themeColor.SelectedBorder),
                },
            });
            style.Triggers.Add(CreateDayTypeTrigger(DayType.Saturday, themeColor.SaturdayBackground, themeColor.SaturdayForeground));
            style.Triggers.Add(CreateDayTypeTrigger(DayType.Sunday, themeColor.SundayBackground, themeColor.SundayForeground));
            style.Triggers.Add(CreateDayTypeTrigger(DayType.SpecialLevel3, themeColor.SpecialLevel3Background, themeColor.SpecialLevel3Foreground));
            style.Triggers.Add(CreateDayTypeTrigger(DayType.SpecialLevel2, themeColor.SpecialLevel2Background, themeColor.SpecialLevel2Foreground));
            style.Triggers.Add(CreateDayTypeTrigger(DayType.SpecialLevel1, themeColor.SpecialLevel1Background, themeColor.SpecialLevel1Foreground));
            style.Triggers.Add(new Trigger
            {
                Property = DayTile.IsMouseOverProperty,
                Value = true,
                Setters = { new Setter(DayTile.BackgroundProperty, themeColor.MouseOverBackground), },
            });
            style.Triggers.Add(CreateDayTypeTrigger(DayType.GrayedOut, themeColor.GrayedOutBackground, themeColor.GrayedOutForeground));
            // Setters
            style.Setters.Add(new Setter(DayTile.PaddingProperty, new Thickness(2, 2, 0, 0)));
            style.Setters.Add(new Setter(DayTile.MarginProperty, new Thickness(0)));
            style.Setters.Add(new Setter(DayTile.BorderThicknessProperty, new Thickness(0, 0, 1, 1)));
            style.Setters.Add(new Setter(DayTile.BorderBrushProperty, themeColor.Border));
            style.Setters.Add(new Setter(DayTile.BackgroundProperty, themeColor.NormalBackground));
            style.Setters.Add(new Setter(DayTile.ForegroundProperty, themeColor.Foreground));
            return style;
        }

        private static Trigger CreateDayTypeTrigger(DayType dayType, Brush background, Brush foreground)
        {
            return new Trigger()
            {
                Property = DayTile.DayTypeProperty,
                Value = dayType,
                Setters =
                {
                    new Setter() { Property = DayTile.ForegroundProperty, Value =  foreground, },
                    new Setter() { Property = DayTile.BackgroundProperty, Value =  background, },
                },
            };
        }
    }
}
