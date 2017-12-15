﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar.UI
{
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
}
