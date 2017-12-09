/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar.Core
{
    public static class Helper
    {
        public static double ToNormalizedArc(this double arc)
        {
            // e.g: arc =  2 deg -> thisArc = 2 deg, returns 2
            //      arc = -2 deg -> thisArc = 2 deg, returns 2
            //      arc = 362 deg -> thisArc 
            double thisAcr = Math.Abs(arc);
            while (thisAcr > (2 * Math.PI))
                thisAcr -= (2 * Math.PI);
            return thisAcr;
        }

        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public static double ToDegrees(this double radians)
        {
            return radians * 180 / Math.PI;
        }

    }
}
