using Augustine.VietnameseCalendar.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar.UI
{
    public static class SpecialDayManager
    {
        public static Dictionary<DateTime, string> SpecialSolarDays = new Dictionary<DateTime, string>()
        {
             { new DateTime(1,1,1), "Tết Dương Lịch"},
             { new DateTime(1,12,25), "Giáng Sinh"},
        };

        public static Dictionary<LunarDate, string> SpecialLunarDays = new Dictionary<LunarDate, string>()
        {
            { new LunarDate(1, 1, false, 1, 7), "Tết Nguyên Đán" },
            { new LunarDate(1, 1, false, 2, 7), "Mồng Hai Tết" },
            { new LunarDate(1, 1, false, 3, 7), "Mồng Ba Tết" },
            { new LunarDate(1, 1, false, 15, 7), "Rằm tháng Giêng" },
            { new LunarDate(1, 3, false, 10, 7), "Giỗ Tổ Hùng Vương" },
            { new LunarDate(1, 4, false, 15, 7), "Phật Đản" },
            { new LunarDate(1, 5, false, 5, 7), "Tết Đoan Ngọ" },
            { new LunarDate(1, 7, false, 15, 7), "Vu Lan" },
            { new LunarDate(1, 8, false, 15, 7), "Tết Trung Thu" },
            { new LunarDate(1, 12, false, 23, 7), "Ông Táo về trời" },
        };

        public static string GetSpecialSolarDateInfo(this DateTime date) {
            var key = new DateTime(1, date.Month, date.Day);
            if (SpecialSolarDays.Keys.Contains(key))
            {
                return SpecialSolarDays[key];
            } else
            {
                return "";
            }
        }
    }
}
