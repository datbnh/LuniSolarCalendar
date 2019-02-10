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
using System.Linq;
using System.Text;

namespace Augustine.VietnameseCalendar.Core.LuniSolarCalendar
{
    public class LuniSolarYear<T> where T:ILocalInfoProvider
    {
        private DateTime _month11LastYear;
        private DateTime _month11ThisYear;

        internal LuniSolarYear(int year)
        {
            Year = year;
            //TimeZone = timeZone;
            var timeZone = ILocalInfoProvider.GetLocalInfoProvider<T>().TimeZone;

            // at 00:00:00 local
            _month11LastYear = Moon.GetNewMoon11(Year - 1, timeZone).Date;
            _month11ThisYear = Moon.GetNewMoon11(Year, timeZone).Date;

            // Convert the local date to UTC date time by adding AddHours(-TimeZone).
            double jdMonth11LastYear = _month11LastYear.AddHours(-timeZone).UniversalDateTimeToJulianDate();
            double jdMonth11ThisYear = _month11ThisYear.AddHours(-timeZone).UniversalDateTimeToJulianDate();

            int k = (int)(0.5 + (jdMonth11LastYear - 2415021.076998695) / 29.530588853);

            IsLeapYear = (jdMonth11ThisYear - jdMonth11LastYear) > 365.0;

            if (!IsLeapYear)
            {
                InitNonLeapYear(k, timeZone);
                Console.WriteLine("None Leap {0}", year);
            }
            else
            {
                InitLeapYear(k, timeZone);
                Console.WriteLine("Leap {0}", year);
            }
        }

        /// <summary>
        /// Get zero-based stem index (0: Giáp, 1: Ất...) of a year.
        /// </summary>
        /// <returns></returns>
        public static int GetYearCelestialStemIndex(int year)
            => (year + 6) % 10;

        /// <summary>
        /// Get zero-based branch index (0: Tý, 1: Sửu...) of a year.
        /// </summary>
        /// <returns></returns>
        public static int GetYearTerrestrialBranchIndex(int year)
            => (year + 8) % 12;

        public static string GetYearCelestialStemName(int year)
            => ILocalInfoProvider.GetLocalInfoProvider<T>().StemNames[GetYearCelestialStemIndex(year)];

        public static string GetYearTerrestrialBranchName(int year)
            => ILocalInfoProvider.GetLocalInfoProvider<T>().BranchNames[GetYearTerrestrialBranchIndex(year)];

        public static string GetYearName(int year)
            => GetYearCelestialStemName(year) + " " + GetYearTerrestrialBranchName(year);

        /// <summary>
        /// Lunar year number
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Information (month number, month starting day, is leap month) of months 
        /// from "tháng Một (11)" last lunar year to this lunar year.
        /// <para>Month numbers are 1 to 11 for "tháng Giêng" (1) to "tháng Một" (11)
        /// and 12 for "tháng Chạp" (12).</para>
        /// The Months tuple array starts with month of number 11;
        /// that means, the numbers should be 11,  12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 
        /// with respect to month 11, 12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 for a
        /// non-leap year.
        /// </summary>
        public Tuple<int, DateTime, bool>[] Months { get; private set; }

        public bool IsLeapYear { get; private set; }

        /// <summary>
        /// Zero-based index. To be used in combination with month number (Item1) in Months tuple array.
        /// Negative 1 (-1) if the year is not a leap year.
        /// <example>
        /// <code>
        ///              year | 2000    2001
        ///             month | 11  12  1   2   3   4   4*  5   6   7   8   9   10  11
        ///               idx | 0   1   2   3   4   5  [6]  7   8   9   10  11  12  13
        ///                   |                         ^
        ///                   |       LeapMonthIndex ---+
        /// month - 11 (+ 12) | 0   1   2   3   4   5  [5] [6   7   8   9   10  11  12]
        ///                   |                         A  \----------- B-------------/
        /// ------------------+--------------------------------------------------------
        ///              year | 2223    2224
        ///             month | 11  11* 12  1   2   3   4   5   6   7   8   9   10  11
        ///               idx | 0  [1  ]2   3   4   5   6   7   8   9   10  11  12  13
        ///                   |     ^
        ///                   |     +--- LeapMonthIndex
        /// month - 11 (+ 12) |[0] [0   1   2   3   4   5   5   6   7   8   9   10  11]
        ///                   | A  \----------------------- B ------------------------/
        /// </code>
        /// </example>
        /// </summary>
        public int LeapMonthIndex { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Lunar year: {0} {1} {2}", Year, IsLeapYear ? "(Leap year)" : "", IsLeapYear ? LeapMonthIndex.ToString() : ""));
            for (int i = 0; i < Months.Length; i++)
            {
                sb.AppendLine(string.Format("Month {0,2}{1}: {2}",
                    Months[i].Item1 == 0 ? 12 : Months[i].Item1,
                    Months[i].Item3 ? "*" : " ",
                    Months[i].Item2));

            }
            return sb.ToString();
        }

        private void InitNonLeapYear(int k, float timeZone)
        {
            int numberOfMonths = 13;

            Months = new Tuple<int, DateTime, bool>[numberOfMonths];
            Months[0] = new Tuple<int, DateTime, bool>(11, _month11LastYear, false);
            Months[numberOfMonths - 1] = new Tuple<int, DateTime, bool>(11, _month11ThisYear, false);
            for (int i = 1; i < numberOfMonths - 1; i++)
            {
                var newMoon =
                    JulianDateConverter.JulianDateToUniversalDateTime(Moon.GetNewMoon(k + i)).AddHours(timeZone).Date;
                Months[i] = new Tuple<int, DateTime, bool>(MonthIdxToMonthNumber((i + 11) % 12), newMoon, false);
            }

            LeapMonthIndex = -1;
        }

        private void InitLeapYear(int k, float timeZone)
        {
            int numberOfMonths = 14;
            Months = new Tuple<int, DateTime, bool>[numberOfMonths];
            DateTime[] newMoons = new DateTime[numberOfMonths];
            int[] majorTermAtMonthBeginnings = new int[numberOfMonths];

            // get all the new moons, local date
            newMoons[0] = _month11LastYear;
            newMoons[numberOfMonths - 1] = _month11ThisYear;
            for (int i = 1; i < numberOfMonths - 1; i++)
            {
                newMoons[i] =
                    JulianDateConverter.JulianDateToUniversalDateTime(Moon.GetNewMoon(k + i)).AddHours(timeZone).Date;
            }

            // determine leap month
            bool found = false;

            majorTermAtMonthBeginnings[0] = (int)(Sun.GetSunLongitudeAtJulianDate(
                newMoons[0].AddHours(-timeZone).UniversalDateTimeToJulianDate()) * 6 / Math.PI);
            for (int i = 0; i < numberOfMonths - 1; i++)
            {
                // If major term at the beginning of this month is same as
                // major term at the beginning of next month, i.e. this month 
                // does not have major term, this month is leap month.
                // 
                // Note that only the first month which does not have major term
                // is leap month (as a Lunar year has maximum one leap month).
                if (found)
                {
                    Months[i] = new Tuple<int, DateTime, bool>(MonthIdxToMonthNumber((i - 1 + 11) % 12), newMoons[i], false);
                    continue;
                }

                double julianDateAtNextMonthBeginning = 
                    newMoons[i + 1].AddHours(-timeZone).UniversalDateTimeToJulianDate();

                majorTermAtMonthBeginnings[i + 1] = (int)(Sun.
                    GetSunLongitudeAtJulianDate(julianDateAtNextMonthBeginning) * 6 / Math.PI);

                // In this algorithm, comparisons will happen with updated element only.
                // Yet be careful: initial value of elements of an int array are zeros.
                // This may confuse the debugging process!
                if (majorTermAtMonthBeginnings[i] == majorTermAtMonthBeginnings[i + 1])
                {
                    found = true;
                    LeapMonthIndex = i;
                    Months[i] = new Tuple<int, DateTime, bool>(MonthIdxToMonthNumber((i - 1 + 11) % 12), newMoons[i], true);
                } else
                {
                    Months[i] = new Tuple<int, DateTime, bool>(MonthIdxToMonthNumber((i + 11) % 12), newMoons[i], false);
                }
            }
            Months[numberOfMonths - 1] =
                new Tuple<int, DateTime, bool>(11, newMoons[13], false);
        }

        private int MonthIdxToMonthNumber(int monthIdx) => monthIdx == 0 ? 12 : monthIdx;
    }
}
