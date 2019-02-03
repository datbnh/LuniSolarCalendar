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
    public static class Sun
    {
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
            var T = (jd - Constants.J2000) / Constants.JulianCentury;
            if (jd < Constants.J2000)
                T = (jd - Constants.J1900) / Constants.JulianCentury;

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
            return L.ToNormalizedArc();
        }

        public static int GetSolarTermIndex(DateTime dateTime)
        {
            var julianDate = JulianDateConverter.UniversalDateTimeToJulianDate(dateTime);
            return (int)(GetSunLongitudeAtJulianDate(julianDate) / Math.PI * 12);
        }

        public static DateTime GetDateTimeOfSolarTerm(int termIndex, int year)
        {
            int[] trialMonths =
                { 3,  4,  4,  5,  5,  6,
                  6,  7,  7,  8,  8,  9,
                  9,  10, 10, 11, 11, 12,
                  12, 1,  1,  2,  2,  3 };
            int[] trialDays =
                { 21, 5, 20,  6, 21,  6,
                  21, 7, 23,  7, 23,  8,
                  23, 8, 23,  7, 22,  7,
                  22, 6, 21,  4, 19,  5 };

            double desiredSunLong = termIndex * (Math.PI / 12);

            DateTime estimatedDateTime = new DateTime(year, trialMonths[termIndex], trialDays[termIndex]);
            double currentSunLong = Sun.GetSunLongitudeAtJulianDate(
                estimatedDateTime.UniversalDateTimeToJulianDate());

            currentSunLong = Sun.GetSunLongitudeAtJulianDate(
                estimatedDateTime.UniversalDateTimeToJulianDate());

            var error = (currentSunLong - desiredSunLong).ToNormalizedArc();
            var direction = 1;
            if (error > Math.PI)
                direction = -1;

            double resolution = 1; // days
            double previousSunLong = currentSunLong;
            var count = 0;
            do
            {
                estimatedDateTime = estimatedDateTime.AddDays(resolution * direction);
                currentSunLong = Sun.GetSunLongitudeAtJulianDate(estimatedDateTime.UniversalDateTimeToJulianDate());

                double error1 = (currentSunLong - desiredSunLong).ToNormalizedArc();
                double error2 = (desiredSunLong + 2 * Math.PI - currentSunLong).ToNormalizedArc();

                if (error1 > error && error2 > error)
                {
                    direction = direction * (-1);
                    resolution = resolution / 2;
                }

                error = error1;
                if (error2 > error1)
                    error = error1;
                else
                    error = error2;

                count++;
            } while (resolution > (1f / 86400) && Math.Abs(error) > 0.00001);

            return estimatedDateTime;
        }
    }
}
