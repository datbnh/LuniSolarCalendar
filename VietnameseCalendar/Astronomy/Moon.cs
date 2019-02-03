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
    public static class Moon
    {
        /* Based on algorithm of Ho Ngoc Duc, at
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

        /* Analysation of algorithm of Hồ Ngọc Đức
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
        public static DateTime GetNewMoon11(int lunarYear, double timeZone)
        {
            // number of days from J1900 to local midnight of 31st December #Year
            // (lunarYear, 12, 31): local
            // (lunarYear, 12, 31, timeZone): universal
            var offset = JulianDateConverter.UniversalDateTimeToJulianDate(lunarYear, 12, 31, timeZone) - Constants.J1900;

            var k = (int)(offset / Constants.SynodicMonth);

            // local date & *time* of new moon
            var newMoonLocalDateTime = JulianDateConverter.JulianDateToUniversalDateTime(GetNewMoon(k)).AddHours(timeZone);
            // beginning of that day (i.e. midnight, i.e. strip off the time)
            var newMoonMidnightLocal = newMoonLocalDateTime.Date;
            // Julian Date at the beginning of that day
            var julianDate =
                newMoonMidnightLocal.AddHours(-timeZone).UniversalDateTimeToJulianDate();

            var sunLongitude = Sun.GetSunLongitudeAtJulianDate(julianDate);

            // Check the winter solstice is after the beginning of that day:
            // If the winter soltice is *before* the beginning of that day, 
            //     the previous new moon is the new moon just before winter solstice,
            // else the current new moon is the new moon just before winter solstice.
            if (sunLongitude > (3 * Math.PI / 2))
            {
                newMoonLocalDateTime = JulianDateConverter.JulianDateToUniversalDateTime(GetNewMoon(k - 1)).AddHours(timeZone);
            }

            return newMoonLocalDateTime;
        }
    }
}
