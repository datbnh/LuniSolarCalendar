/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Collections.Generic;

namespace Augustine.VietnameseCalendar.UI
{
    public static class SpecialDayManager
    {
        public static List<SpecialDate> SpecialSolarDays = new List<SpecialDate>()
        {
             new SpecialDate( 1,  1, "Tết Dương Lịch"),
             new SpecialDate(12, 24, "Giáng Sinh") ,
        };

        public static List<SpecialDate> SpecialLunarDays = new List<SpecialDate>()
        {
            new SpecialDate( 1,  1, "Tết Nguyên Đán"),
            new SpecialDate( 1,  2, "Mồng Hai Tết"),
            new SpecialDate( 1,  3, "Mồng Ba Tết"),
            new SpecialDate( 1, 15, "Rằm tháng Giêng"),
            new SpecialDate( 3, 10, "Giỗ Tổ Hùng Vương"),
            new SpecialDate( 4, 15, "Phật Đản"),
            new SpecialDate( 5,  5, "Tết Đoan Ngọ"),
            new SpecialDate( 7, 15, "Vu Lan"),
            new SpecialDate( 8, 15, "Tết Trung Thu"),
            new SpecialDate(12, 23, "Ông Táo về trời"),
        };

        public static string GetSpecialSolarDateInfo(this DateTime date) {
            //if (date.Day == 29 && date.Month == 2)
            //    return "";
            //var key = new DateTime(1, date.Month, date.Day);
            //if (SpecialSolarDays.Contains())
            //{
            //    return SpecialSolarDays[key];
            //} else
            //{
            //    return "";
            //}
            return "";
        }

        public class SpecialDate
        {
            public int Day { get; set; }
            public int Month { get; set; }
            public string Label { get; set; }
            public string Decorator { get; set; }
            public SpecialDate(int month, int day, string label, string decorator = "")
            {
                Month = month;
                Day = day;
                Label = label;
                Decorator = decorator;
            }
        }
    }
}
