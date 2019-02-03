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
    public class Constants
    {
        public const double DraconicMonth = 27.212220817d; // days
        public const double SiderealMonth = 27.321661547d; // days
        public const double AnomalisticMonth = 27.554549878d; // days
        public const double SynodicMonth = 29.530588853d; // days
        public const double EclipseYear = 346.620076d; // days
        public const double TropicalYear = 365.24219879d; // days
        public const double JulianCentury = 36525.00d; // days
        public const double SolarCentury = 36524.219879d; //days


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
    }
}
