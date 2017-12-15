using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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

        public ThemeSize FontSizes;

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

        public Theme(ThemeColor themeColor, ThemeSize themeSize, TextAndShadow textAndShadow)
        {
            ThemeColor = themeColor;
            FontSizes = themeSize;
            TextAndShadow = textAndShadow;
        }

        public static Style CreateDayTileStyle(ThemeColor themeColor)
        {
            var style = new Style();
            // Triggers
            style.Triggers.Add(new Trigger
            {
                Property = AugustineCalendarDay.IsSelectedProperty,
                Value = true,
                Setters = {
                    new Setter(AugustineCalendarDay.PaddingProperty, new Thickness(1, 1, 0, 0)),
                    new Setter(AugustineCalendarDay.BorderThicknessProperty, new Thickness(1, 1, 1, 1)),
                    new Setter(AugustineCalendarDay.BorderBrushProperty, themeColor.SelectedBorder),
                },
            });
            style.Triggers.Add(CreateDayTypeTrigger(DayTypes.Saturday, themeColor.SaturdayBackground, themeColor.SaturdayForeground));
            style.Triggers.Add(CreateDayTypeTrigger(DayTypes.Sunday, themeColor.SundayBackground, themeColor.SundayForeground));
            style.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel3, themeColor.SpecialLevel3Background, themeColor.SpecialLevel3Foreground));
            style.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel2, themeColor.SpecialLevel2Background, themeColor.SpecialLevel2Foreground));
            style.Triggers.Add(CreateDayTypeTrigger(DayTypes.SpecialLevel1, themeColor.SpecialLevel1Background, themeColor.SpecialLevel1Foreground));
            style.Triggers.Add(new Trigger
            {
                Property = AugustineCalendarDay.IsMouseOverProperty,
                Value = true,
                Setters = { new Setter(AugustineCalendarDay.BackgroundProperty, themeColor.MouseOverBackground), },
            });
            style.Triggers.Add(CreateDayTypeTrigger(DayTypes.GrayedOut, themeColor.GrayedOutBackground, themeColor.GrayedOutForeground));
            // Setters
            style.Setters.Add(new Setter(AugustineCalendarDay.PaddingProperty, new Thickness(2, 2, 0, 0)));
            style.Setters.Add(new Setter(AugustineCalendarDay.MarginProperty, new Thickness(0)));
            style.Setters.Add(new Setter(AugustineCalendarDay.BorderThicknessProperty, new Thickness(0, 0, 1, 1)));
            style.Setters.Add(new Setter(AugustineCalendarDay.BorderBrushProperty, themeColor.Border));
            style.Setters.Add(new Setter(AugustineCalendarDay.BackgroundProperty, themeColor.NormalBackground));
            style.Setters.Add(new Setter(AugustineCalendarDay.ForegroundProperty, themeColor.Foreground));
            return style;
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
