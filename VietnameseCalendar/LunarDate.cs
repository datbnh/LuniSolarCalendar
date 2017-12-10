/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;

namespace Augustine.VietnameseCalendar.Core
{
    public class LunarDate
    {
        public static readonly string[] MonthNames =
            { "Giêng", "Hai", "Ba", "Tư", "Năm", "Sáu",
              "Bảy", "Tám", "Chín", "Mười", "Mười Một", "Chạp"};
        public static readonly string[] Stems =
            {"Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý"};
        public static readonly string[] Branches =
            {"Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ",
             "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi"};
        public static readonly string[] SolarTermsVietnamese = {"Xuân Phân", "Thanh Minh",
                                                 "Cốc Vũ", "Lập Hạ",
                                                 "Tiểu Mãn", "Mang Chủng",
                                                 "Hạ Chí", "Tiểu Thử",
                                                 "Đại Thử", "Lập Thu",
                                                 "Xử Thử", "Bạch Lộ",
                                                 "Thu Phân", "Hàn Lộ",
                                                 "Sương Giáng", "Lập Đông",
                                                 "Tiểu Tuyết", "Đại Tuyết",
                                                 "Đông Chí", "Tiểu Hàn",
                                                 "Đại Hàn", "Lập Xuân",
                                                 "Vũ Thủy", "Kinh Trập",
                                             };

        public int Year { get; private set; }
        public int Month { get; private set; }
        public bool IsLeapMonth { get; private set; }
        public int Day { get; private set; }
        public double TimeZone { get; private set; }
        public bool IsTermBegin { get => GetSolarTermIndex(SolarDate.AddDays(-1), TimeZone) != GetSolarTermIndex(SolarDate, TimeZone); }

        public int CelestialStemIndex { get => GetYearCelestialStemIndex(Year); }
        public int TerrestrialBranchIndex { get => GetYearTerrestrialBranchIndex(Year); }
        public string CelestialStemName { get => GetYearCelestialStemName(Year); }
        public string TerrestrialBranchName { get => GetYearTerrestrialBranchName(Year); }
        public string YearName { get => GetYearName(Year); }

        public int MonthCelestialStemIndex { get => GetMonthCelestialStemIndex(Year, Month); }
        public int MonthTerrestrialBranchIndex { get => GetMonthTerrestrialIndex(Month); }
        public string MonthCelestialStemName { get => GetMonthCelestialStemName(Year, Month); }
        public string MonthTerrestrialBranchName { get => GetMonthTerrestrialBranchName(Month); }

        public DateTime SolarDate { get; private set;}

        /// <summary>
        /// Returns "Giêng, Hai, Ba..."
        /// </summary>
        /// <returns></returns>
        public string MonthName { get => GetMonthName(Month); }
        
        /// <summary>
        /// Returns "Giêng, Hai, Ba..." or "Giêng nhuận, Hai nhuận, Ba nhuận..." (if applicable)
        /// </summary>
        /// <returns></returns>
        public string MonthShortName { get => GetMonthShortName(Month, IsLeapMonth); }
        
        /// <summary>
        /// Returns "Can Chi" or "Can Chi nhuận"
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isLeapMonth"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public string MonthLongName { get => GetMonthLongName(Year, Month, IsLeapMonth); }
        
        /// <summary>
        /// Returns "Name (Can Chi)" or "Name (Can Chi) nhuận".
        /// E.g. "Bảy (Bính Ngọ)", "Năm (Nhâm Ngọ) nhuận".
        /// </summary>
        /// <returns></returns>
        public string MonthFullName { get => GetMonthFullName(Year, Month, IsLeapMonth); }

        public string DayName { get => GetDayName(SolarDate.Year, SolarDate.Month, SolarDate.Day); }

        public string SolarTerm { get => GetSolarTermName(SolarDate, TimeZone); }

        public string FullDayInfo
        {
            get => String.Format("Ngày {0} tháng {1} năm {2}" + Environment.NewLine +
                " Tháng {3}" + Environment.NewLine +
                " Ngày {4}" + Environment.NewLine +
                " Tiết {5}",
                Day, MonthShortName, YearName,
                MonthLongName,
                DayName,
                SolarTerm);
        }

        public LunarDate(int year, int month, bool isLeapMonth, int day, double timeZone)
        {
            Year = year;
            Month = month;
            IsLeapMonth = isLeapMonth;
            Day = day;
            TimeZone = timeZone;
            SolarDate = ToSolar(Year, Month, IsLeapMonth, Day, TimeZone);
        }

        internal LunarDate(int year, int month, bool isLeapMonth, int day, DateTime solarDate, double timeZone)
		{
			Year = year;
			Month = month;
			IsLeapMonth = isLeapMonth;
			Day = day;
            TimeZone = timeZone;
            this.SolarDate = solarDate;
		}

        public override string ToString()
		{
            return String.Format("Ngày {0} tháng {1} năm {2}", Day, MonthFullName, YearName);
		}

        #region === Static methods ===
        
        public static LunarDate FromSolar(int year, int month, int day, double timeZone)
        {
            // Sample case 1: Solar date 24/05/2000 --- Lunar date 21/04/2000
            //                  previousNewMoon = 04/05/2000
            // lunarYear = previousNewMoon.Year = 2000
            //                  newMoon11Before = 08/12/1999
            //                   newMoon11After = 26/11/2000
            //   (04/05/2000 - 08/12/1999) / 29 = 148 days / 29 
            //                                  = 5.10x
            // --------------------------------------------------------------
            // Sample case 2: Solar date 01/01/2014 --- Lunar date 01/12/2013
            //                  previousNewMoon = 01/01/2014
            //             previousNewMoon.Year = 2014
            //                        lunarYear = 2013
            //                  newMoon11Before = 03/12/2013
            //                   newMoon11After = 26/11/2000
            //   (01/01/2014 - 03/12/2013) / 29 = 29 days / 29 
            //                                  = 1
            // --------------------------------------------------------------
            // Sample case 3: Solar date 22/12/2033 --- Lunar date 01/11i/2033
            //                  previousNewMoon = 22/12/2033
            // lunarYear = previousNewMoon.Year = 2033
            //                  newMoon11Before = 03/12/2032 
            //                   newMoon11After = 22/11/2033

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

            // "previous, this/current" and "next" are not used to avoid ambiguity.
            var newMoon11Before = Astronomy.NewMoon11(previousNewMoon.Year - 1, timeZone).Date;
            var newMoon11After = Astronomy.NewMoon11(previousNewMoon.Year, timeZone).Date;

            // correcting for such cases as case 3
            if (newMoon11After < previousNewMoon)
            {
                newMoon11Before = newMoon11After;
                newMoon11After = Astronomy.NewMoon11(previousNewMoon.Year + 1, timeZone).Date;
            }

            var isLeapYear = (newMoon11After - newMoon11Before).TotalDays > 365.0;
            
            var monthsFromNewMoon11Before = (int) ((previousNewMoon - newMoon11Before).TotalDays / 29);

            /* Note:
             * monthsFromNewMoon11Before = 0: lunar month = 11, lunar year = previous year
             * monthsFromNewMoon11Before = 1: lunar month = 12, lunar year = previous year
             * monthsFromNewMoon11Before = 2: lunar month =  1, lunar year = this year
             * monthsFromNewMoon11Before = 3: lunar month =  2, lunar year = this year
             * and so on
             */

            // month 11 and 12 belong to lunar year of the previous year.
            if (monthsFromNewMoon11Before < 2)
            {
                lunarYear = newMoon11After.Year - 1;
                lunarMonth = 11 + monthsFromNewMoon11Before;
            }
            else
            {
                lunarYear = newMoon11After.Year;
                lunarMonth = monthsFromNewMoon11Before - 1;
            }

            // correcting month number if this lunar year is leap year
            if (isLeapYear)
            {
                var leapMonthIndex = LunarYear.GetLunarYear(newMoon11After.Year, 7).LeapMonthIndex;
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

            return new LunarDate(lunarYear, lunarMonth, isLeapMonth, lunarDay, thisDay, timeZone);
        }

        // overload method
        public static LunarDate FromSolar(DateTime solarDate, double timeZone)
        {
            return FromSolar(solarDate.Year, solarDate.Month, solarDate.Day, timeZone);
        }

        public static DateTime ToSolar(int lunarYear, int lunarMonth, bool isLeapMonth, int lunarDay, double timeZone)
        {
            LunarYear thisLunarYear;
            if (lunarMonth >= 11) {
                thisLunarYear = LunarYear.GetLunarYear(lunarYear + 1, 7);
            } else
            {
                thisLunarYear = LunarYear.GetLunarYear(lunarYear, 7);
            }

            var monthIndex = lunarMonth - 11;
            if (monthIndex < 0)
            {
                monthIndex += 12;
            }

            //              year | 2000    2001
            //             month | 11  12  1   2   3   4   4*  5   6   7   8   9   10  11
            //               idx | 0   1   2   3   4   5  [6]  7   8   9   10  11  12  13
            //                   |                         ^
            //                   |       LeapMonthIndex ---+
            // month - 11 (+ 12) | 0   1   2   3   4   5  [5] [6   7   8   9   10  11  12]
            //                   |                         A  \----------- B-------------/
            // ------------------+--------------------------------------------------------
            //              year | 2223    2224
            //             month | 11  11* 12  1   2   3   4   5   6   7   8   9   10  11
            //               idx | 0  [1  ]2   3   4   5   6   7   8   9   10  11  12  13
            //                   |     ^
            //                   |     +--- LeapMonthIndex
            // month - 11 (+ 12) |[0] [0   1   2   3   4   5   5   6   7   8   9   10  11]
            //                   | A  \----------------------- B ------------------------/

            if (thisLunarYear.IsLeapYear)
            {
                if (((monthIndex == thisLunarYear.LeapMonthIndex - 1) && (isLeapMonth)) // case A
                    || (monthIndex > thisLunarYear.LeapMonthIndex - 1)) // case B
                {
                    monthIndex++;
                }
            }

            DateTime monthBeginningDate = thisLunarYear.Months[monthIndex].Item1;
            return monthBeginningDate.AddDays(lunarDay - 1);
            
            // TODO: validate input/output
        }

        public static DateTime ToSolar(LunarDate lunarDate)
        {
            return ToSolar(lunarDate.Year, lunarDate.Month, lunarDate.IsLeapMonth, lunarDate.Day, lunarDate.TimeZone);
        }

        #region Year

        /// <summary>
        /// Get zero-based stem index (0: Giáp, 1: Ất...) of the current year.
        /// </summary>
        /// <returns></returns>
        public static int GetYearCelestialStemIndex(int year) { return (year + 6) % 10; }
        
        /// <summary>
		/// Get zero-based branch index (0: Tý, 1: Sửu...) of the current year.
		/// </summary>
		/// <returns></returns>
		public static int GetYearTerrestrialBranchIndex(int year) { return (year + 8) % 12; }

        public static string GetYearCelestialStemName(int year) { return Stems[GetYearCelestialStemIndex(year)]; }

        public static string GetYearTerrestrialBranchName(int year) { return Branches[GetYearTerrestrialBranchIndex(year)]; }

        public static string GetYearName(int year) { return GetYearCelestialStemName(year) + " " + GetYearTerrestrialBranchName(year); }
        
        #endregion
        
        #region Month
        public static int GetMonthCelestialStemIndex(int year, int month) { return (year * 12 + month + 3) % 10; }

        public static int GetMonthTerrestrialIndex(int month) { return month > 10 ? month - 11 : month + 1; }

        public static string GetMonthCelestialStemName(int year, int month)
		{
            if (month < 1 || month > MonthNames.Length)
                return "undefined";
            return Stems[GetMonthCelestialStemIndex(year, month)];
		}

        public static string GetMonthTerrestrialBranchName(int month)
		{
            if (month < 1 || month > MonthNames.Length)
                return "undefined";
            return Branches[GetMonthTerrestrialIndex(month)];
		}

        /// <summary>
        /// Returns "Name (Can Chi)" or "Name (Can Chi) nhuận".
        /// E.g. "Bảy (Bính Ngọ)", "Năm (Nhâm Ngọ) nhuận".
        /// </summary>
        /// <returns></returns>
		public static string GetMonthFullName(int year, int month, bool isLeapMonth = false)
		{
            if (month < 1 || month > MonthNames.Length)
                return "undefined";
            return String.Format("{0} ({1} {2}){3}", GetMonthName(month), GetMonthCelestialStemName(year, month), GetMonthTerrestrialBranchName(month), (isLeapMonth ? " nhuận" : "").ToString());
		}

        /// <summary>
        /// Returns "Giêng, Hai, Ba..."
        /// </summary>
        /// <returns></returns>
		public static string GetMonthName(int month)
		{
            if (month < 1 || month > MonthNames.Length)
                return "undefined";
			return MonthNames[month - 1];
		}

        /// <summary>
        /// Returns "Giêng, Hai, Ba..." or "Giêng nhuận, Hai nhuận, Ba nhuận..." (if applicable)
        /// </summary>
        /// <returns></returns>
		public static string GetMonthShortName(int month, bool isLeapMonth)
        {
            if (month < 1 || month > MonthNames.Length)
                return "undefined";
            return MonthNames[month - 1] + (isLeapMonth ? " nhuận" : "");
        }

        /// <summary>
        /// Returns "Can Chi" or "Can Chi nhuận"
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isLeapMonth"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static string GetMonthLongName(int year, int month, bool isLeapMonth)
        {
            if (month < 1 || month > MonthNames.Length)
                return "undefined";
            return String.Format("{0} {1}{2}", 
                GetMonthCelestialStemName(year, month), 
                GetMonthTerrestrialBranchName(month), 
                (isLeapMonth ? " nhuận" : "").ToString());
        }
        
        /// <summary>
        /// 0 -> 12, 1 -> 1, 2 -> 2, 3 -> 3... 11 -> 11
        /// </summary>
        /// <param name="monthIndex"></param>
        /// <returns></returns>
        public static int GetMonth(int monthIndex) { return monthIndex == 0 ? 12 : monthIndex; }

        #endregion

        #region Day
        public static int GetDayCelestialStemIndex(int solarYear, int solarMonth, int solarDay)
        {
            var julianDateNumber = Astronomy.UniversalDateToJulianDayNumber(solarYear, solarMonth, solarDay);
            return (julianDateNumber + 9) % 10;
        }

        public static int GetDayTerrestrialIndex(int solarYear, int solarMonth, int solarDay)
        {
            var julianDateNumber = Astronomy.UniversalDateToJulianDayNumber(solarYear, solarMonth, solarDay);
            return (julianDateNumber + 1) % 12;
        }

        public static string GetDayCelestialStemName(int solarYear, int solarMonth, int solarDay)
        {
            return Stems[GetDayCelestialStemIndex(solarYear, solarMonth, solarDay)];
        }

        public static string GetDayTerrestrialBranchName(int solarYear, int solarMonth, int solarDay)
        {
            return Branches[GetDayTerrestrialIndex(solarYear, solarMonth, solarDay)];
        }

        public static string GetDayName(int solarYear, int solarMonth, int solarDay)
        {
            return GetDayCelestialStemName(solarYear, solarMonth, solarDay) + " " +
                GetDayTerrestrialBranchName(solarYear, solarMonth, solarDay);
        }

        public static int GetSolarTermIndex(DateTime date, double timeZone)
        {
            var julianDate = Astronomy.UniversalDateTimeToJulianDate(date.AddHours(-timeZone).AddHours(24));
            var termIndex = (int)(Astronomy.GetSunLongitudeAtJulianDate(julianDate) / Math.PI * 12);
            return termIndex;
        }

        public static string GetSolarTermName(DateTime date, double timeZone)
        {
            return SolarTermsVietnamese[GetSolarTermIndex(date, timeZone)];
        }


        #endregion

        #endregion
    }
}
