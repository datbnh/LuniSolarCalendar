/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core.Astronomy;
using System;
using System.Collections.Generic;

namespace Augustine.VietnameseCalendar.Core.LuniSolarCalendar
{
    public class LuniSolarDate<T> where T:ILocalInfoProvider
    {
        //public new static string[] MonthNames
        //    = new string[]{"Giêng", "Hai", "Ba", "Tư", "Năm", "Sáu",
        //        "Bảy", "Tám", "Chín", "Mười", "Một", "Chạp"};
        //public new static string[] Stems
        //    = new string[]{"Giáp", "Ất", "Bính", "Đinh", "Mậu",
        //        "Kỷ", "Canh", "Tân", "Nhâm", "Quý"};
        //public new static string[] Branches
        //    = new string[]{"Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ",
        //     "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi"};
        //public new static string[] SolarTermsVietnamese
        //    = new string[]{"Xuân Phân", "Thanh Minh",
        //        "Cốc Vũ", "Lập Hạ",
        //        "Tiểu Mãn", "Mang Chủng",
        //        "Hạ Chí", "Tiểu Thử",
        //        "Đại Thử", "Lập Thu",
        //        "Xử Thử", "Bạch Lộ",
        //        "Thu Phân", "Hàn Lộ",
        //        "Sương Giáng", "Lập Đông",
        //        "Tiểu Tuyết", "Đại Tuyết",
        //        "Đông Chí", "Tiểu Hàn",
        //        "Đại Hàn", "Lập Xuân",
        //        "Vũ Thủy", "Kinh Trập",
        //    };


        public int Year { get; private set; }
        public int Month { get; private set; }
        public bool IsLeapMonth { get; private set; }
        public int Day { get; private set; }
        public DateTime SolarDate { get; private set;}
        public bool IsTermBeginThisDay => GetSolarTermIndex(SolarDate.AddDays(-1)) != GetSolarTermIndex(SolarDate);
        public int SolarTermIndex => GetSolarTermIndex(SolarDate);

        #region String properties

        public string YearName => LuniSolarYear<T>.GetYearName(Year);

        /// <summary>
        /// Returns "Giêng, Hai, Ba..."
        /// </summary>
        /// <returns></returns>
        public string MonthName => GetMonthName(Month);

        /// <summary>
        /// Returns "Giêng, Hai, Ba..." or "Giêng nhuận, Hai nhuận, Ba nhuận..." (if applicable)
        /// </summary>
        /// <returns></returns>
        public string MonthShortName => GetMonthShortName(Month, IsLeapMonth);

        /// <summary>
        /// Returns "Can Chi" or "Can Chi nhuận"
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isLeapMonth"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public string MonthLongName => GetMonthLongName(Year, Month, IsLeapMonth);

        /// <summary>
        /// Returns "Name (Can Chi)" or "Name (Can Chi) nhuận".
        /// E.g. "Bảy (Bính Ngọ)", "Năm (Nhâm Ngọ) nhuận".
        /// </summary>
        /// <returns></returns>
        public string MonthFullName => GetMonthFullName(Year, Month, IsLeapMonth);

        public string DayName => GetDayName(SolarDate.Year, SolarDate.Month, SolarDate.Day);

        public string SolarTerm => GetSolarTermName(SolarDate);

        public string FullDayInfo => string.Format("Ngày {0} tháng {1} năm {2}" + Environment.NewLine +
                " Tháng {3}" + Environment.NewLine +
                " Ngày {4}" + Environment.NewLine +
                " Tiết {5}",
                Day, MonthShortName, YearName,
                MonthLongName,
                DayName,
                SolarTerm);

        //internal int CelestialStemIndex { get => GetYearCelestialStemIndex(Year); }
        //internal int TerrestrialBranchIndex { get => GetYearTerrestrialBranchIndex(Year); }
        //internal string CelestialStemName { get => GetYearCelestialStemName(Year); }
        //internal string TerrestrialBranchName { get => GetYearTerrestrialBranchName(Year); }

        //internal int MonthCelestialStemIndex { get => GetMonthCelestialStemIndex(Year, Month); }
        //internal int MonthTerrestrialBranchIndex { get => GetMonthTerrestrialIndex(Month); }
        //internal string MonthCelestialStemName { get => GetMonthCelestialStemName(Year, Month); }
        //internal string MonthTerrestrialBranchName { get => GetMonthTerrestrialBranchName(Month); }

        #endregion

        internal LuniSolarDate(int year, int month, bool isLeapMonth, int day, DateTime solarDate)
        {
            Year = year;
            Month = month;
            IsLeapMonth = isLeapMonth;
            Day = day;
            SolarDate = solarDate;
        }

        public override string ToString() => string.Format("Ngày {0} tháng {1} năm {2}", Day, MonthFullName, YearName);

        #region === Static methods ===

        internal static LuniSolarDate<T> CalculateLuniSolarDateFromSolarDate(int year, int month, int day)
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

            var timeZone = ILocalInfoProvider.GetLocalInfoProvider<T>().TimeZone;

            int lunarYear;
            int lunarMonth;
            int lunarDay;
            bool isLeapMonth = false;

            var thisDay = new DateTime(year, month, day);

            var k = (int)((thisDay.AddHours(-timeZone).UniversalDateTimeToJulianDate() - 2415021.076998695)/ Constants.SynodicMonth + 0.5);

            var previousNewMoon =
                    JulianDateConverter.JulianDateToUniversalDateTime(Moon.GetNewMoon(k)).AddHours(timeZone).Date;
            while (previousNewMoon > thisDay)
            {
                previousNewMoon =
                    JulianDateConverter.JulianDateToUniversalDateTime(Moon.GetNewMoon(--k)).AddHours(timeZone).Date;
            }

            // "previous, this/current" and "next" are not used to avoid ambiguity.
            var newMoon11Before = Moon.GetNewMoon11(previousNewMoon.Year - 1, timeZone).Date;
            var newMoon11After = Moon.GetNewMoon11(previousNewMoon.Year, timeZone).Date;

            // correcting for such cases as case 3
            if (newMoon11After < previousNewMoon)
            {
                newMoon11Before = newMoon11After;
                newMoon11After = Moon.GetNewMoon11(previousNewMoon.Year + 1, timeZone).Date;
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
                var leapMonthIndex = LuniSolarCalendar<VietnameseLocalInfoProvider>.GetLunarYear(newMoon11After.Year).LeapMonthIndex;
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

            return new LuniSolarDate<T>(lunarYear, lunarMonth, isLeapMonth, lunarDay, thisDay);
        }

        #region Month
        public static int GetMonthCelestialStemIndex(int year, int month)
            => (year * 12 + month + 3) % 10;

        public static int GetMonthTerrestrialIndex(int month)
            => month > 10 ? month - 11 : month + 1;

        public static string GetMonthCelestialStemName(int year, int month)
        {
            if (month < 1 || month > ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames.Length)
                return "undefined";
            return ILocalInfoProvider.GetLocalInfoProvider<T>().StemNames[GetMonthCelestialStemIndex(year, month)];
        }

        public static string GetMonthTerrestrialBranchName(int month)
        {
            if (month < 1 || month > ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames.Length)
                return "undefined";
            return ILocalInfoProvider.GetLocalInfoProvider<T>().BranchNames[GetMonthTerrestrialIndex(month)];
        }

        /// <summary>
        /// Returns "Name (Can Chi)" or "Name (Can Chi) nhuận".
        /// E.g. "Bảy (Bính Ngọ)", "Năm (Nhâm Ngọ) nhuận".
        /// </summary>
        /// <returns></returns>
		public static string GetMonthFullName(int year, int month, bool isLeapMonth = false)
        {
            if (month < 1 || month > ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames.Length)
                return "undefined";
            return String.Format("{0} ({1} {2}){3}", GetMonthName(month), GetMonthCelestialStemName(year, month), GetMonthTerrestrialBranchName(month), (isLeapMonth ? " nhuận" : "").ToString());
        }

        /// <summary>
        /// Returns "Giêng, Hai, Ba..."
        /// </summary>
        /// <returns></returns>
		public static string GetMonthName(int month)
        {
            if (month < 1 || month > ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames.Length)
                return "undefined";
            return ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames[month - 1];
        }

        /// <summary>
        /// Returns "Giêng, Hai, Ba..." or "Giêng nhuận, Hai nhuận, Ba nhuận..." (if applicable)
        /// </summary>
        /// <returns></returns>
		public static string GetMonthShortName(int month, bool isLeapMonth)
        {
            if (month < 1 || month > ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames.Length)
                return "undefined";
            return ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames[month - 1] + (isLeapMonth ? " nhuận" : "");
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
            if (month < 1 || month > ILocalInfoProvider.GetLocalInfoProvider<T>().MonthNames.Length)
                return "undefined";
            return string.Format("{0} {1}{2}",
                GetMonthCelestialStemName(year, month),
                GetMonthTerrestrialBranchName(month),
                (isLeapMonth ? " nhuận" : "").ToString());
        }


        ///// <summary>
        ///// 0 -> 12, 1 -> 1, 2 -> 2, 3 -> 3... 11 -> 11
        ///// </summary>
        ///// <param name="monthIndex"></param>
        ///// <returns></returns>
        //public static int GetMonth(int monthIndex) { return monthIndex == 0 ? 12 : monthIndex; }

        #endregion

        #region Day
        
        public static string GetSolarTermName(DateTime date)
        {
            return ILocalInfoProvider.GetLocalInfoProvider<T>().SolarTerms[GetSolarTermIndex(date)];
        }

        public static string GetDayCelestialStemName(int solarYear, int solarMonth, int solarDay)
            => ILocalInfoProvider.GetLocalInfoProvider<T>().StemNames[GetDayCelestialStemIndex(solarYear, solarMonth, solarDay)];

        public static string GetDayTerrestrialBranchName(int solarYear, int solarMonth, int solarDay)
            => ILocalInfoProvider.GetLocalInfoProvider<T>().BranchNames[GetDayTerrestrialIndex(solarYear, solarMonth, solarDay)];

        public static string GetDayName(int solarYear, int solarMonth, int solarDay)
            => GetDayCelestialStemName(solarYear, solarMonth, solarDay) + " " + GetDayTerrestrialBranchName(solarYear, solarMonth, solarDay);

        public static int GetSolarTermIndex(DateTime date)
            => Sun.GetSolarTermIndex(date.AddHours(-ILocalInfoProvider.GetLocalInfoProvider<T>().TimeZone).AddHours(24));

        public static int GetDayCelestialStemIndex(int solarYear, int solarMonth, int solarDay)
        {
            var julianDateNumber
                = JulianDateConverter.UniversalDateToJulianDayNumber(solarYear, solarMonth, solarDay);
            return (julianDateNumber + 9) % 10;
        }

        public static int GetDayTerrestrialIndex(int solarYear, int solarMonth, int solarDay)
        {
            var julianDateNumber
                = JulianDateConverter.UniversalDateToJulianDayNumber(solarYear, solarMonth, solarDay);
            return (julianDateNumber + 1) % 12;
        }

        #endregion

        #endregion
    }
}
