using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar
{
	public class LunarDate
	{
        public static readonly string[] MonthNames = 
            { "Giêng", "Hai", "Ba", "Tư", "Năm", "Sáu",
              "Bảy", "Tám", "Chín", "Mười", "Một (11)", "Chạp"};
		public static readonly string[] Stems = 
            {"Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý"};
		public static readonly string[] Branches = 
            {"Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ",
             "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi"};

		public int Year { get; private set; }
		public int Month { get; private set; }
        public bool IsLeapMonth { get; private set; }
        public int Day { get; private set; }
        public double TimeZone { get; private set; }

        internal LunarDate(int year, int month, bool isLeapMonth, int day, double timeZone)
		{
			Year = year;
			Month = month;
			IsLeapMonth = isLeapMonth;
			Day = day;
            TimeZone = timeZone;
		}
        
        public static LunarDate FromSolar(int year, int month, int day, double timeZone)
        {
            // Sample case: Solar date 24/05/2000 --- Lunar date 21/04/2000
            // previousNewMoon = 04/05/2000
            // lunarYear = previousNewMoon.Year = 2000
            // newMoon11Before = 08/12/1999
            // newMoon11After = 26/11/2000
            // (04/05/2000 - 08/12/1999) / 29 = 148 days / 29 = 5.10x

            int lunarYear;
            int lunarMonth;
            int lunarDay;
            bool isLeapMonth = false;

            var thisDay = new DateTime(year, month, day);

            var k = (int)((thisDay.AddHours(-timeZone).UniversalDateTimeToJulianDate() - 2415021.076998695)/ Astronomy.SynodicMonth + 0.5);

            var previousNewMoon =
                    Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(k)).AddHours(timeZone).Date;
            while (previousNewMoon > thisDay)
            {
                previousNewMoon =
                    Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(--k)).AddHours(timeZone).Date;
                //Console.WriteLine("."); // debug
            } 

            lunarYear = previousNewMoon.Year;

            // "previous, this/current" and "next" are not used to avoid ambiguity.
            var newMoon11Before = Astronomy.NewMoon11(lunarYear - 1, timeZone).Date;
            var newMoon11After = Astronomy.NewMoon11(lunarYear, timeZone).Date;

            var isLeapYear = (newMoon11After - newMoon11Before).TotalDays > 365.0;
            
            var monthsFromNewMoon11Before = (int) ((previousNewMoon - newMoon11Before).TotalDays / 29);

            /* Note:
             * monthsFromNewMoon11Before = 0: lunar month = 11, lunar year = previous year
             * monthsFromNewMoon11Before = 1: lunar month = 12, lunar year = previous year
             * monthsFromNewMoon11Before = 2: lunar month =  1, lunar year = this year
             * monthsFromNewMoon11Before = 3: lunar month =  2, lunar year = this year
             * and so on
             */

            //
            if (monthsFromNewMoon11Before < 2)
            {
                lunarYear = lunarYear - 1;
                lunarMonth = 11 + monthsFromNewMoon11Before;
            }
            else
            {
                lunarMonth = monthsFromNewMoon11Before - 1;
            }

            // correcting month number if this lunar year is leap year
            if (isLeapYear)
            {
                var leapMonthIndex = (new LunarYear(lunarYear, 7)).LeapMonthIndex;
                if (monthsFromNewMoon11Before >= leapMonthIndex)
                {
                    lunarMonth--;
                    if (monthsFromNewMoon11Before == leapMonthIndex)
                    {
                        isLeapMonth = true;
                    }
                }
            }
            
            lunarDay = (int)(thisDay - previousNewMoon).TotalDays + 1;

            return new LunarDate(lunarYear, lunarMonth, isLeapMonth, lunarDay, timeZone);
        }
        
        // overload method
        public static LunarDate FromSolar(DateTime solarDate, double timeZone)
        {
            return FromSolar(solarDate.Year, solarDate.Month, solarDate.Day, timeZone);
        }

        public static DateTime ToSolar(int lunarYear, int lunarMonth, bool isLeapMonth, int lunarDay, double timeZone)
        {
            //int solarDay;
            //int solarMonth;
            //int solarYear;
            throw new NotImplementedException();
        }

        #region Year
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

        #endregion

        #region Month
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
        #endregion

        public override string ToString()
		{
            return String.Format("({0:00}/{1:00}/{2:0000}) Ngày {3} tháng {4} ({5}){6} năm {7}",
                 Day, Month, Year,
                 Day, GetMonthName(), GetMonthFullName(),
                 IsLeapMonth ? " (Nhuận)" : "", GetYearName());
		}
	}
}
