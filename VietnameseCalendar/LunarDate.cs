using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar
{
	public class LunarDate
	{
		public static string[] MonthNames = { "Giêng", "Hai", "Ba", "Tư",
											"Năm", "Sáu", "Bảy", "Tám",
											"Chín", "Mười", "Một (11)", "Chạp"};
		public static string[] Stems = {"Giáp", "Ất", "Bính", "Đinh", "Mậu",
										 "Kỷ", "Canh", "Tân", "Nhâm", "Quý"};
		public static string[] Branches = {"Tý", "Sửu", "Dần", "Mão",
											 "Thìn", "Tỵ", "Ngọ", "Mùi",
											 "Thân", "Dậu", "Tuất", "Hợi"};

		public int Year;
		public int Month;
		public bool LeapMonth;
		public int Day;
        public double TimeZone;

		public LunarDate(int year, int month, bool leapMonth, int day)
		{
			Year = year;
			Month = month;
			LeapMonth = leapMonth;
			Day = day;
		}


        // Not using year cache
        public static LunarDate SolarToLunar(int year, int month, int day, double timeZone)
        {
            // case: 24/05/2000 (Solar) --- 21/04/2000 (Lunar)

            int lunarYear;
            int lunarMonth;
            int lunarDay;
            bool leapMonth = false;

            var thisDay = new DateTime(year, month, day);

            var k = (int)((thisDay.AddHours(-timeZone).UniversalDateTimeToJulianDate() - 2415021.076998695)/ Astronomy.SynodicMonth + 0.5);
            //Console.WriteLine((thisDay.AddHours(-timeZone).UniversalDateTimeToJulianDate() - 2415021.076998695) / Astronomy.SynodicMonth); // debug

            // 04/05/2000
            var previousNewMoon =
                    Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(k)).AddHours(timeZone).Date;
            while (previousNewMoon > thisDay)
            {
                previousNewMoon =
                    Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(--k)).AddHours(timeZone).Date;
                //Console.WriteLine("."); // debug
            } 

            // 2000
            lunarYear = previousNewMoon.Year;

            // "previous, this/current" and "next" are not used to avoid ambiguity.
            // newMoon11Before = 08/12/1999
            // newMoon11After = 26/11/2000
            var newMoon11Before = Astronomy.NewMoon11(lunarYear - 1, timeZone).Date;
            var newMoon11After = Astronomy.NewMoon11(lunarYear, timeZone).Date;

            var isLeapYear = (newMoon11After - newMoon11Before).TotalDays > 365.0;

            // 04/05/2000 - 08/12/1999 = 148 days
            var daysFromNewMoon11BeforeToPreviousNewMoon = (previousNewMoon - newMoon11Before).TotalDays;
            
            // = (int)(5.10) = 5
            var monthsFromNewMoon11Before = (int) (daysFromNewMoon11BeforeToPreviousNewMoon / 29);

            // monthsFromNewMoon11Before = 0: lunar month = 11, lunar year = previous year
            // monthsFromNewMoon11Before = 1: lunar month = 12, lunar year = previous year
            // monthsFromNewMoon11Before = 2: lunar month =  1, lunar year = this year
            // monthsFromNewMoon11Before = 3: lunar month =  2, lunar year = this year
            if (monthsFromNewMoon11Before < 2)
            {
                lunarYear = lunarYear - 1;
                lunarMonth = 11 + monthsFromNewMoon11Before;
            }
            else
            {
                lunarMonth = monthsFromNewMoon11Before - 1;
            }

            if (isLeapYear)
            {
                var leapMonthIndex = (new LunarYear(lunarYear, 7)).LeapMonthIndex;
                if (monthsFromNewMoon11Before >= leapMonthIndex)
                {
                    lunarMonth--;
                    if (monthsFromNewMoon11Before == leapMonthIndex)
                    {
                        leapMonth = true;
                    }
                }
            }
            
            // 
            lunarDay = (int)(thisDay - previousNewMoon).TotalDays + 1;

            return new LunarDate(lunarYear, lunarMonth, leapMonth, lunarDay);
            //var k, dayNumber, monthStart, a11, b11, lunarDay, lunarMonth, lunarYear, lunarLeap;
            //dayNumber = jdFromDate(dd, mm, yy);
            //k = INT((dayNumber - 2415021.076998695) / 29.530588853);
            //monthStart = getNewMoonDay(k + 1, timeZone);
            //if (monthStart > dayNumber)
            //{
            //    monthStart = getNewMoonDay(k, timeZone);
            //}
            //a11 = getLunarMonth11(yy, timeZone);
            //b11 = a11;
            //if (a11 >= monthStart)
            //{
            //    lunarYear = yy;
            //    a11 = getLunarMonth11(yy - 1, timeZone);
            //}
            //else
            //{
            //    lunarYear = yy + 1;
            //    b11 = getLunarMonth11(yy + 1, timeZone);
            //}
            //lunarDay = dayNumber - monthStart + 1;
            //diff = INT((monthStart - a11) / 29);
            //lunarLeap = 0;
            //lunarMonth = diff + 11;
            //if (b11 - a11 > 365)
            //{
            //    leapMonthDiff = getLeapMonthOffset(a11, timeZone);
            //    if (diff >= leapMonthDiff)
            //    {
            //        lunarMonth = diff + 10;
            //        if (diff == leapMonthDiff)
            //        {
            //            lunarLeap = 1;
            //        }
            //    }
            //}
            //if (lunarMonth > 12)
            //{
            //    lunarMonth = lunarMonth - 12;
            //}
            //if (lunarMonth >= 11 && diff < 4)
            //{
            //    lunarYear -= 1;
            //}
        }

        public static int[] LunarToSolar(int lunarYear, int lunarMonth, int lunarDay, bool isLeapMonth, double timeZone)
        {
            return null;
        }




		// Year
		/// <summary>
		/// Get zero-based stem index (0: Giáp, 1: Ất...) of the current year.
		/// </summary>
		/// <returns></returns>
		public int GetStemIndex()
		{
			return (Year + 6) % 10;
		}

		/// <summary>
		/// Get zero-based branch index (0: Tý, 1: Sửu...) of the current year.
		/// </summary>
		/// <returns></returns>
		public int GetBranchIndex()
		{
			return (Year + 8) % 12;
		}
		public string GetStemName()
		{
			return Stems[GetStemIndex()];
		}
		public string GetBranchName()
		{
			return Branches[GetBranchIndex()];
		}
		public string GetYearName()
		{
			return GetStemName() + " " + GetBranchName();
		}
		// Month
		public int GetMonthStemIndex()
		{
			return (Year * 12 + Month + 3) % 10;
		}
		public int GetMonthBranchNumber()
		{
			switch (Month)
			{
				case 11:
					return 0;
				case 12:
					return 1;
				default:
					return Month + 1;
			}
		}
		public string GetMonthStemName()
		{
            if (Month < 0 || Month > MonthNames.Length)
                return "Undefined";
            return Stems[GetMonthStemIndex()];
		}
		public string GetMonthBranchName()
		{
            if (Month < 0 || Month > MonthNames.Length)
                return "Undefined";
            return Branches[GetMonthBranchNumber()];
		}
		public string GetMonthFullName()
		{
            if (Month < 0 || Month > MonthNames.Length)
                return "Undefined";
            return GetMonthStemName() + " " + GetMonthBranchName();
		}
		public string GetMonthName()
		{
            if (Month < 1 || Month > MonthNames.Length)
                return "Undefined";
			return MonthNames[Month - 1];
		}
		public string IsLeapString()
		{
			return (LeapMonth ? " (Nhuận)" : "");
		}

		public override string ToString()
		{
            return String.Format("({0:00}/{1:00}/{2:0000}) Ngày {3} tháng {4} ({5}){6} năm {7}",
                 Day, Month, Year,
                 Day, GetMonthName(), GetMonthFullName(), 
                 IsLeapString(), GetYearName());
		}
	}

}
