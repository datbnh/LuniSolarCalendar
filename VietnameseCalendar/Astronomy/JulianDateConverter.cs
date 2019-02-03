/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2019      //      *
 * // Melbourne, February 2019                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;

namespace Augustine.VietnameseCalendar.Core.Astronomy
{
    /// <summary>
    /// Provides conversion methods between Junian Date (Number) and other types of Calendar Date
    /// Universal Date (Time), Gregorian Date.
    /// </summary>
    public static class JulianDateConverter
    {
        /* + INTRODUCTION +
         * ****************
         */
        /* Abbreviations
         * =============
         * JD  : Julian Date
         * JDN : Julian Day Number
         * TT  : Terrestrial Time (roughly equivalent to Cordinated UT [UTC])
         * UT  : Universal Time
         */
        /* About Julian Date
         * =================
         * Julian date is the number of days since noon 1st January 4713 BC on the Julian Calendar, or
         * 24th November, 4714 BC on the Gregorian Calendar.
         * 
         * The name is derived from Joseph Justus Scaliger (1540-1609) who proposed this system.
         * This has nothing to do with Julius Ceasar.
         * 
         * One Julian Period is an interval of 7980 years. That means, after 7980 years, the Julian day 
         * number statrs at 1 again.
         * 
         * 
         * Note that when 1 BC ends, 1 AD begins. Thus,
         *     year    1 AD = year 1 (astronomical year numbering),
         *     year    1 BC = year  0, 
         *	   year    2 BC = year -1, and so on, so
         *     year 4713 BC = year -4712
         */
        /* Conversion algorithms
         * =====================
         * L. E. Doggett, Ch. 12, "Calendars", p. 604, in Seidelmann 1992
         * https://archive.org/stream/131123ExplanatorySupplementAstronomicalAlmanac/131123-explanatory-supplement-astronomical-almanac_djvu.txt
         * 
         * --- Quotation begins ---
         * 
         * 12.92 Converting between Gregorian Calendar Date and Julian Day Number
         *       ----------------------------------------------------------------
         * These algorithms by Fliegel and Van Flandern (1968) are valid for all Gregorian 
         * calendar dates corresponding to JD > 0, i.e., dates after —4713 November 23. 
         * 
         * Given: Y, M, D. Compute: JD. 
         * 
         * JD = (1461 x (Y + 4800 + (M - 14) / 12)) / 4 + (367 x (M - 2 - 12 x ((M - 14)/ 12))) / 12 
         *      - (3 x ((Y + 4900 + (M - 14) / 12) / 100)) / 4 + D - 32075                 (12.92-1)
         *      
         * Given: JD. Computre: Y, M, D.
         * L = JD + 68569
         * N = (4 x L) / 146O97
         * L = L - (146097 x N + 3) / 4
         * I = (4000 x (L + 1)) / 1461001
         * L = L - (1461 x I) / 4 + 31
         * J = (80 x L) / 2447
         * D = L - (2447 x J) / 80
         * L = J / 11
         * M = J + 2 - 12 x L
         * Y = 100 x (N - 49) + I + L                                                      (12.92-2)
         * 
         * 12.95 Converting between Julian Calendar Date and Julian Day Number
         *       -------------------------------------------------------------
         * These algorithms are valid for all values of Y > 4712, i.e., for all dates with JD >
         * 0. The formula lbr computing JD from I, M, D was constructed by Fliegel (1990) as
         * an entry in "The Great Julian Day Contest," held at the Jet Propulsion Laboratory
         * in 1970. Given: I, M, D. Compute: JD.
         * 
         * [ blah blah blah (see code implementation) - D.B. ]
         * --- Quatation ends ---
         * 
         * Another algorithm
         * =================
         * 
         * a = (14 - M) / 12
         * y = Y + 4800 - a
         * m = M + 12 * a - 3
         * 
         * y and m are years and ms since 4801BC/-4800.Mar.1
         * 
         *      From -4800   March 1 Julian Calendar Date
         *        to -4712 January 1 Julian Calendar Date
         * there are    88 years - (31 + 28) days
         *         =    88 * 365 - (31 + 28) days + 88 / 4 leap years
         *         = 32083 days
         * 
         * Gregorian Calendar has been applied from 1582 October 15
         * (Julian Day Number 2299161).
         * The date before this day is 1582 October 4 Julian Calendar Date.
         * 
         * Gregorian Calendar Date (date after 1582 October 15) --> Julian Day Number:
         *      135 * m + 2               y       y       y
         * D + ------------- + 365 * y + --- - (----- - -----) - 32045                  (1)
         *           5                    4      100     400 
         * 
         * Julian Calendar Date --> Julian Day Number:
         *      135 * m + 2               y                     y       y
         * D + ------------- + 365 * y + --- - 32083 = (1) + (----- - -----) - 38       (2)
         *           5                    4                    100     400 
         * 
         * (153 * m + 2) / 5 gives the number of days since March 1 and comes from 
         * the repetition of days in the m from March in groups of five:
         *    Mar–Jul: 31 30 31 30 31
         *    Aug–Dec: 31 30 31 30 31
         *    Jan–Feb: 31 28
         *    
         * For further information, look at
         * The Calendar FAQ, at 
         * http://www.tondering.dk/claus/cal/julperiod.php.
         * 
		 * Handy tool(s):
		 * http://numerical.recipes/julian.html
		 * 
		 * 
         */

        #region Calendar Date to Julian Day Number and Julian Date

        // core methods
        // Algorithm of Hồ Ngọc Đức
        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        internal static int JulianCalendarDateToJulianDayNumber(int year, int month, int day)
        {
            return 367 * year
                - 7 * (year + 5001 + (month - 9) / 7) / 4
                + 275 * month / 9
                + day
                + 1729777;
        }

        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        internal static int GregorianCalendarDateToJulianDayNumber(int year, int month, int day)
        {
            return 367 * year
                    - 7 * (year + (month + 9) / 12) / 4
                    - 3 * ((year + (month - 9) / 7) / 100 + 1) / 4
                    + 275 * month / 9
                    + day
                    + 1721029;
        }

        // Altenative algorithm (introduced above in the introduction)
        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        internal static int JulianCalendarDateToJulianDayNumber1(int year, int month, int day)
        {
            return 367 * year
                - (7 * (year + 5001 + (month - 9) / 7)) / 4
                + (275 * month) / 9
                + day
                + 1729777;
        }

        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        internal static int GregorianCalendarDateToJulianDayNumber1(int year, int month, int day)
        {
            return (1461 * (year + 4800 + (month - 14) / 12)) / 4
                + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12
                - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day
                - 32075;
        }

        // public methods
        /// <summary>
        /// <para></para>
        /// Returns the Julian Day Number regarding the given date.
        /// <para></para> IMPORTANT NOTE:
        /// If the given <paramref name="universalDate"/> is before 4th October 1582, 
        /// it is considered as Julian calendar date; otherwise (after 15th October 1582), 
        /// it is considered as Gregorian calendar date.
        /// Dates between 5th and 14th October 1582 are invalid.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int UniversalDateToJulianDayNumber(int year, int month, int day)
        {
            // Gregorian Calendar Date
            if (year == 1582 && month == 10 && (day > 4 && day < 15))
            {
                throw new ArgumentOutOfRangeException("No such date from 5th to 14th October 1582.");
            }
            if (year > 1582
                || (year == 1582 && month > 10)
                || (year == 1582 && month == 10 && day > 14))
            {
                return GregorianCalendarDateToJulianDayNumber(year, month, day);
            }
            // Julian Calendar Date
            else if (year >= -4712)
            {
                return JulianCalendarDateToJulianDayNumber(year, month, day);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Input date cannot be before 1st January 4713 BC on the Julian Calendar.");
            }
        }

        /// <summary>
        /// <para></para>
        /// Returns the Julian Day Number regarding the given date.
        /// <para></para> IMPORTANT NOTE:
        /// If the given <paramref name="universalDate"/> is before 4th October 1582, 
        /// it is considered as Julian calendar date; otherwise (after 15th October 1582), 
        /// it is considered as Gregorian calendar date.
        /// Dates between 5th and 14th October 1582 are invalid.
        /// <para></para>
        /// Note that the time part will be ignored.
        /// Use UniversalDateTimeToJulianDate to include the time in the conversion.
        /// </summary>
        /// <param name="universalDate"></param>
        /// <returns></returns>
        public static int UniversalDateToJulianDayNumber(this DateTime universalDate)
        {
            return UniversalDateToJulianDayNumber(universalDate.Year, universalDate.Month, universalDate.Day);
        }

        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millisecond"></param>
        /// <returns></returns>
        public static double UniversalDateTimeToJulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            double fraction = (double)(hour - 12) / 24 + (double)minute / 1440 +
                (second + (double)millisecond / 1000) / 86400;
            return UniversalDateToJulianDayNumber(year, month, day) + fraction;
        }

        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static double UniversalDateTimeToJulianDate(int year, int month, int day, double hours)
        {
            return UniversalDateTimeToJulianDate(year, month, day, 0, 0, 0, 0) + hours / 24;
        }

        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="universalDateTime"></param>
        /// <returns></returns>
        public static double UniversalDateTimeToJulianDate(this DateTime universalDateTime)
        {
            return UniversalDateTimeToJulianDate(universalDateTime.Year,
                universalDateTime.Month,
                universalDateTime.Day,
                universalDateTime.Hour,
                universalDateTime.Minute,
                universalDateTime.Second,
                universalDateTime.Millisecond);
        }
        #endregion

        #region Julian Day Number and Julian Date to Calendar Date

        /* Richards, E. G. (2013). Calendars. In S. E. Urban & P. K. Seidelmann, eds. 
		 * Explanatory Supplement to the Astronomical Almanac, 3rd ed. (pp. 585–624). 
		 * Mill Valley, Calif.: University Science Books. ISBN 978-1-89138-985-6
		 * 
		 * Algorithm parameters for Gregorian calendar
		 * variable	 value  variable value
		 *    y      4716      v     3
		 *    j      1401      u     5
		 *    m      2         s     153
		 *    n      12        w     2
		 *    r      4         B     274277
		 *    p      1461      C     −38
		 * 
		 * For Julian calendar:
		 * 1. f = J + j
		 * 
		 * For Gregorian calendar:
		 * 1. f = J + j + (((4 × J + B) div 146097) × 3) div 4 + C
		 * 
		 * For Julian or Gregorian, continue:
		 * 2. e = r × f + v
		 * 3. g = mod(e, p) div r
		 * 4. h = u × g + w
		 * 5. D = (mod(h, s)) div u + 1
		 * 6. M = mod(h div s + m, n) + 1
		 * 7. Y = (e div p) - y + (n + m - M) div n
		 * 
		 * D, M, and Y are the numbers of the day, month, and year respectively for the afternoon at 
		 * the beginning of the given Julian day.
		 * 
		 */

        /// <summary>
        /// Note that JDN does not contain the time information; therefore, the returned value does not 
        /// have parts relating to time (i.e. hour, minute, second...).
        /// </summary>
        /// <param name="jdn"></param>
        /// <returns></returns>
        public static int[] JulianDayNumberToUniversalDate(int jdn)
        {
            if (jdn < 0)
                throw new ArgumentOutOfRangeException("This method is only valid for jdn >= 0.");

            int B = 274277;
            int C = -38;
            int j = 1401;
            int m = 2;
            int n = 12;
            int p = 1461;
            int r = 4;
            int s = 153;
            int u = 5;
            int v = 3;
            int w = 2;
            int y = 4716;

            int f;
            // If after 5th October 1582, Gregorian calendar
            if (jdn > 2299160)
            {
                f = jdn + j + (((4 * jdn + B) / 146097) * 3) / 4 + C;
            }
            else
            // Julian calendar
            {
                f = jdn + j;
            }
            int e = r * f + v;
            int g = (e % p) / r;
            int h = u * g + w;
            int D = ((h % s)) / u + 1;
            int M = (h / s + m) % n + 1;
            int Y = (e / p) - y + (n + m - M) / n;
            return new int[] { Y, M, D };
        }

        /// <summary>
        /// TODO: add an appropriate summary.
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static DateTime JulianDateToUniversalDateTime(double jd)
        {
            if (jd < 1721423.5)
                throw new ArgumentOutOfRangeException("This method currently supports jdn >= 1721423.5.");

            //TODO: robustness around 2299160 and 2299161

            int jdn = (int)jd;
            double time = jdn - jd;
            if (time < 0)
            {
                time = 0.5 - time;
            }
            int[] DateTime = JulianDayNumberToUniversalDate(jdn);
            return new DateTime(DateTime[0], DateTime[1], DateTime[2]).AddDays(time);
        }

        #endregion
    }
}
