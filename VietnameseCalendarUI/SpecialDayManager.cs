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

namespace Augustine.VietnameseCalendar.UI
{
    public static class SpecialDayManager
    {
        public static Dictionary<string, SpecialDateInfo> SpecialSolarDays = new Dictionary<string, SpecialDateInfo>()
        {
            { "0101", new SpecialDateInfo("Tết Dương Lịch", "🎆") },
            { "1402", new SpecialDateInfo("Valentine", "♥") },
            { "2512", new SpecialDateInfo("Giáng Sinh", "🎄") },
        };

        public static Dictionary<string, SpecialDateInfo> SpecialLunarDays = new Dictionary<string, SpecialDateInfo>()
        {
            { "0101", new SpecialDateInfo("Tết Nguyên Đán", "🎆") },
            { "0201", new SpecialDateInfo("Mồng Hai Tết", "②") },
            { "0301", new SpecialDateInfo("Mồng Ba Tết", "③") },
            { "1501", new SpecialDateInfo("Rằm tháng Giêng", "⓯") },
            { "1003", new SpecialDateInfo("Giỗ Tổ Hùng Vương", "") },
            { "1504", new SpecialDateInfo("Phật Đản", "") },
            { "0505", new SpecialDateInfo("Tết Đoan Ngọ", "") },
            { "1507", new SpecialDateInfo("Vu Lan", "🌹") },
            { "1508", new SpecialDateInfo("Tết Trung Thu", "🎑") },
            { "2312", new SpecialDateInfo("Ông Táo Chầu Trời", "🐟") },
        };

        /// <summary>
        /// Returns special day info of a luni-solar date.
        /// Returns null if there is nothing special.
        /// </summary>
        /// <param name="luniSolarDate"></param>
        /// <returns></returns>
        public static bool GetSpecialDateInfo(this LuniSolarDate luniSolarDate, out SpecialDateInfo specialDateInfo) {
            SpecialDateInfo spInfo = null;
            var key = GetSolarKey(luniSolarDate);
            if (SpecialSolarDays.ContainsKey(key))
            {
                spInfo = SpecialSolarDays[key];
            }
            key = GetLunarKey(luniSolarDate);
            if (SpecialLunarDays.ContainsKey(key))
            {
                if (spInfo == null)
                {
                    spInfo = SpecialLunarDays[key];
                }
                else
                {
                    spInfo.Label += "/" + SpecialLunarDays[key].Label;
                    spInfo.Decorator += SpecialLunarDays[key].Decorator;
                }
            }
            specialDateInfo = spInfo;
            if (spInfo == null)
                return false;
            else
                return true;
        }

        public class SpecialDateInfo
        {
            public string Label { get; set; }
            public string Decorator { get; set; }
            public SpecialDateInfo(string label, string decorator = "")
            {
                Label = label;
                Decorator = decorator;
            }
        }

        public static string GetSolarKey(LuniSolarDate lsDate)
        {
            return string.Format("{0:ddMM}", lsDate.SolarDate);
        }

        public static string GetLunarKey(LuniSolarDate lsDate)
        {
            return string.Format("{0:00}{1:00}{2}", lsDate.Day, lsDate.Month, lsDate.IsLeapMonth ? "n" : "");
        }

        public static class SolarTermDecorator
        {
            public static readonly string VernalEquinox = "❀";
            public static readonly string AutumnalEquinox = "🍃";
            public static readonly string SummerSolstice = "☀";
            public static readonly string WinterSolstice = "❄";
        }
    }
}
