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

    public static class Astronomy
	{
		public static readonly double DraconicMonth = 27.212220817d; // days
		public static readonly double SiderealMonth = 27.321661547d; // days
		public static readonly double AnomalisticMonth = 27.554549878d; // days
		public static readonly double SynodicMonth = 29.530588853d; // days
		public static readonly double EclipseYear = 346.620076d; // days
		public static readonly double TropicalYear = 365.24219879d; // days
		public static readonly double JulianCentury = 36525.00d; // days
		public static readonly double SolarCentury = 36524.219879d; //days
		
		/// <summary>
		/// 01-01-1900 12:00:00 UTC
		/// </summary>
		public static readonly DateTime J1900UtDateTime = new DateTime(1900, 1, 1, 12, 0, 0);
		
		/// <summary>
		/// 01-01-2000 12:00:00 UTC
		/// </summary>
		public static readonly DateTime J2000UtDateTime = new DateTime(2000, 1, 1, 12, 0, 0);

		/// <summary>
		/// 2415021.0 (01-01-1900 12:00:00 UTC)
		/// </summary>
		public static readonly double J1900 = 2415021.0;

		/// <summary>
		/// 2451545.0 (01-01-2000 12:00:00 UTC)
		/// </summary>
		public static double J2000 = 2451545.0;

		#region === Julian Date and Julian Day Number ===
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
         *  blah blah blah (see code implementation)
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
		internal static int JulianCalendarDateToJulianDayNumber(int year, int month, int day)
		{
			return 367 * year
				- 7 * (year + 5001 + (month - 9) / 7) / 4
				+ 275 * month / 9
				+ day
				+ 1729777;
		}

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
		internal static int JulianCalendarDateToJulianDayNumber1(int year, int month, int day)
		{
			return 367 * year
				- (7 * (year + 5001 + (month - 9) / 7)) / 4
				+ (275 * month) / 9
				+ day
				+ 1729777;
		}

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

		public static double UniversalDateTimeToJulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
            double fraction = (double)(hour - 12) / 24 + (double)minute / 1440 +
                (second + (double)millisecond / 1000) / 86400;
            return UniversalDateToJulianDayNumber(year, month, day) + fraction;
        }

		public static double UniversalDateTimeToJulianDate(int year, int month, int day, double hours)
		{
            return UniversalDateTimeToJulianDate(year, month, day, 0, 0, 0, 0) + hours / 24;
		}

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

		#endregion

		#region === Moon and Sun ===

		/* Base on algorithm of Ho Ngoc Duc, at
		 * http://www.informatik.uni-leipzig.de/~duc/amlich/
		 * 
		 */
        
		public static double GetNewMoon(int k)
		{
			var T = k / 1236.85; // Time in Julian centuries from 1900 January 0.5
			var T2 = T * T;
			var T3 = T2 * T;
			var Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
			Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2).ToRadians()); // Mean new moon
			var M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3; // Sun's mean anomaly
			var Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3; // Moon's mean anomaly
			var F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3; // Moon's argument of latitude
			var C1 = (0.1734 - 0.000393 * T) * Math.Sin(M.ToRadians()) + 0.0021 * Math.Sin((2 * M).ToRadians());
			C1 = C1 - 0.4068 * Math.Sin(Mpr.ToRadians()) + 0.0161 * Math.Sin(2 * Mpr.ToRadians());
			C1 = C1 - 0.0004 * Math.Sin((3 * Mpr).ToRadians());
			C1 = C1 + 0.0104 * Math.Sin((2 * F).ToRadians()) - 0.0051 * Math.Sin((M + Mpr).ToRadians());
			C1 = C1 - 0.0074 * Math.Sin((M - Mpr).ToRadians()) + 0.0004 * Math.Sin((2 * F + M).ToRadians());
			C1 = C1 - 0.0004 * Math.Sin((2 * F - M).ToRadians()) - 0.0006 * Math.Sin((2 * F + Mpr).ToRadians());
			C1 = C1 + 0.0010 * Math.Sin((2 * F - Mpr).ToRadians()) + 0.0005 * Math.Sin((2 * Mpr + M).ToRadians());
			double deltat;
			if (T < -11)
				deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3;
			else
				deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
			var JdNew = Jd1 + C1 - deltat;
			return (JdNew);
		}

		/* Jean Meeus: Astronomical Algorithms
		 * Accuracy: 0.01 degree
		 * // http://www.geoastro.de/elevaz/basics/meeus.htm
		 */
		/// <summary>
		/// Returns the sun longitude at a moment given by Julian Date (jd).
		/// The unit is in radians.
		/// </summary>
		/// <param name="jd"></param>
		/// <returns></returns>
		public static double GetSunLongitudeAtJulianDate(double jd)
		{
			//if (jd < J2000)
			//	throw new ArgumentOutOfRangeException(
			//		"This algorithm is only valid after 1st January 2000, 12:00:00 UT.");

			// Time in Julian centuries from 2000-01-01 12:00:00 GMT
			var T = (jd - J2000) / 36525;
			var T2 = T * T;

			// mean anomaly, degree
			var M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2;

			// mean longitude, degree
			var L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2;

			var DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(M.ToRadians())
				+ (0.019993 - 0.000101 * T) * Math.Sin(2 * M.ToRadians()) + 0.000290 * Math.Sin(3 * M.ToRadians());

			// true longitude, degree
			var L = L0 + DL;

			// convert to radians
			L = L.ToRadians();
			// normalize to (0, 2*PI)
			L = L - Math.PI * 2 * ((int)(L / (Math.PI * 2)));
			if (L < 0)
				L = L + Math.PI;

			return L;
		}

		/* Analisation of algorithm of Hồ Ngọc Đức
		 * ---------------------------------------
		 * double off = LocalToJD(31, 12, Y) - 2415021.076998695;
		 *              ^ returns Julian Date of 31st December #Year, 00:00:00 UTC+7
		 *                    i.e. 30th December #Year, 17:00:00 UTC
		 * int k = INT(off / 29.530588853);
		 * double jd = NewMoon(k);
		 *             ^ returns the new moon date *time* in Julian Dater, *UTC*
		 * int[] ret = LocalFromJD(jd);
		 *             ^ converts (jd + timezone/24) back to Calendar Date,
		 *                   disregrading the time (i.e. the begining of day in local time zone 
		 *                   [00:00:00 local] that contains the new moon.)
		 *             
		 * // sun longitude at local midnight
		 * double sunLong = SunLongitude(LocalToJD(ret[0], ret[1], ret[2])); 
		 * if (sunLong > 3 * PI / 2)
		 * {
		 *     jd = NewMoon(k - 1);
		 * }
		 * return LocalFromJD(jd);
		 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		 * e.g.: 
		 * 	     NewMoon(1236) = 2451520.4393767994
		 * 	                   = 7/12/1999 10:32:42 UTC
		 * 	                   = 8/12/1999 17:32:42 UTC+7, thus,
		 * 	                   
		 *  [ Note: UniversalFromJD(2451520.4393767994) = (7, 12, 1999) ]
		 *              LocalFromJD(2451520.4393767994) =
		 *    UniversalFromJD(JD + LOCAL_TIMEZONE/24.0) = (8, 12, 1999)
		 *    
		 *                       LocalToJD(8, 12, 1999) = 
		 * UniversalToJD(D, M, Y) - LOCAL_TIMEZONE/24.0 = 8/12/1999 00:00:00 - 7
		 * ^                        ^                   = 7/12/1999 17:00:00 UTC <---+
		 * |                        +---[ Shift back to local midnight ]             |
		 * +---[ Julius Date of D/M/Y 00:00:00 UTC (midnight), 8/12/1999 00:00:00]   |
		 *                   [ This value is used to determine the sun longitude ]---+
		 */

		/// <summary>
		/// Returns the local date time of the new moon just before 
		/// the winter solstice in the given lunar year.
		/// </summary>
		/// <param name="lunarYear"></param>
		/// <param name="timeZone"></param>
		/// <returns></returns>
		public static DateTime NewMoon11(int lunarYear, double timeZone)
		{
            // number of days from J1900 to local midnight of 31st December #Year
            // (lunarYear, 12, 31): local
            // (lunarYear, 12, 31, timeZone): universal
            var offset = UniversalDateTimeToJulianDate(lunarYear, 12, 31, timeZone) - J1900;

			var k = (int)(offset / SynodicMonth);

			// local date & *time* of new moon
			var newMoonLocalDateTime = JulianDateToUniversalDateTime(GetNewMoon(k)).AddHours(timeZone);
			// beginning of that day (i.e. midnight, i.e. strip off the time)
			var newMoonMidnightLocal = newMoonLocalDateTime.Date;
			// Julian Date at the beginning of that day
			var julianDate = 
                newMoonMidnightLocal.AddHours(-timeZone).UniversalDateTimeToJulianDate();

			var sunLongitude = GetSunLongitudeAtJulianDate(julianDate);
            
            // Check the winter solstice is after the beginning of that day:
			// If the winter soltice is *before* the beginning of that day, 
			//     the previous new moon is the new moon just before winter solstice,
            // else the current new moon is the new moon just before winter solstice.
			if (sunLongitude > (3 * Math.PI / 2))
			{
				newMoonLocalDateTime = JulianDateToUniversalDateTime(GetNewMoon(k - 1)).AddHours(timeZone);
			}
            ////debug info
            //Console.WriteLine("===============================================");
            //Console.WriteLine("      Lunar Year: {0} - Moon phase #{1}", lunarYear, k);
            //Console.WriteLine("-----------------------------------------------");
            //Console.WriteLine("     New moon at: {0:dd/MM/yyyy hh:mm:ss} UTC+0", JulianDateToUniversalDateTime(GetNewMoon(k)));
            //Console.WriteLine("              or: {0:dd/MM/yyyy hh:mm:ss} UTC+7", newMoonLocalDateTime);
            //Console.WriteLine("              or: Julian Date {0}", GetNewMoon(k));
            //Console.WriteLine("-----------------------------------------------");
            //Console.WriteLine("Sun longitude at: {0:dd/MM/yyyy hh:mm:ss} UTC+0", JulianDateToUniversalDateTime(julianDate));
            //Console.WriteLine("              or: Julian Date {0}", julianDate);
            //Console.WriteLine("              is: {0} degrees\n" +
            //                  "                  {1} 270 degrees", sunLongitude.ToDegrees(),
            //    (sunLongitude > 3 * Math.PI / 2) ? ">" : "<");
            //Console.WriteLine("-----------------------------------------------");
            //Console.WriteLine("New moon before Winter Solstice of this year \n" +
            //                  "              is: {0:dd/MM/yyyy hh:mm:ss} UTC+7", newMoonLocalDateTime);
            //Console.WriteLine("===============================================");
            
            return newMoonLocalDateTime;
		}

		#endregion

		#region === Helpers ===

		public static double ToRadians(this double degrees)
		{
			return degrees * Math.PI / 180;
		}

		public static double ToDegrees(this double radians)
		{
			return radians * 180 / Math.PI;
		}

		#endregion

	}
}
