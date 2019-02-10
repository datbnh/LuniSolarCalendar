using Augustine.VietnameseCalendar.Core.Astronomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar.Core.LuniSolarCalendar
{
    /// <summary>
    /// 
    /// </summary>
    public static class LuniSolarCalendar<T> where T:ILocalInfoProvider
    {
        /* Problem 1: MonthNames, StemNames, BranchNames and SolarTerms are expensive.
         * They should be implemented in a to a singleton class.
         * 
         * Problem 2: MonthNames, StemNames, BranchNames, SolarTerms and TimeZone
         * vary for each type of Luni Solar Calendar (e.g. Vietnamese, Chinese),
         * they should be declared in a Factory and defined in derrived class.
         * 
         * Problem 3: Cannot make a singleton class abstract at the same time.
         * 
         * Trail 1: Static class with virtual members of MonthNames...
         * -> Cannot virtualise static member or method.
         * 
         */

        public static int YearCacheSize { get; set; } = 3;
        public static int DateCacheSize { get; set; } = 35;

        private static Dictionary<string, LuniSolarYear<T>> _yearCache;
        private static Dictionary<string, LuniSolarDate<T>> _dateCache;
        private static Dictionary<string, string> _luniSolarToSolarHashMap;

        #region Year and Date converters

        public static LuniSolarYear<T> GetLunarYear(int year)
        {
            float timeZone = ILocalInfoProvider.GetLocalInfoProvider<T>().TimeZone;
            if (YearCacheSize > 0)
            {
                // init dictionary for the first time
                if (_yearCache == null)
                {
                    _yearCache = new Dictionary<string, LuniSolarYear<T>>();
                }

                // hash the year and time zone
                string key = GetYearHash(year, timeZone);

                // if year is already cached, just take it out
                if (_yearCache.ContainsKey(key))
                {
                    return _yearCache[key];
                }
                // else, calculate the year and add cache to dictionay
                else
                {
                    // if dictionary is already full, remove the oldest pair.
                    if (_yearCache.Count == YearCacheSize)
                    {
                        _yearCache.Remove(_yearCache.Keys.First());
                    }
                    // calculate the year
                    var lunarYear = new LuniSolarYear<T>(year);
                    // do not forget to add new year to dictionay :))
                    _yearCache.Add(key, lunarYear);
                    return lunarYear;
                }
            }
            // no caching, just calculate the year directly
            else
            {
                return new LuniSolarYear<T>(year);
            }
        }

        public static LuniSolarDate<T> LuniSolarDateFromSolarDate(int year, int month, int day)
        {
            float timeZone = ILocalInfoProvider.GetLocalInfoProvider<T>().TimeZone;
            if (DateCacheSize > 0)
            {
                // init dictionary for the first time
                if (_dateCache == null)
                {
                    _dateCache = new Dictionary<string, LuniSolarDate<T>>(DateCacheSize);
                }

                // hash the year and time zone
                string key = GetSolarDateHash(year, month, day, timeZone);

                // if year is already cached, just take it out
                if (_dateCache.ContainsKey(key))
                {
                    return _dateCache[key];
                }
                // else, calculate the date and add cache to dictionay
                else
                {
                    // if dictionary is already full, remove the oldest pair.
                    if (_dateCache.Count == DateCacheSize)
                    {
                        _dateCache.Remove(_dateCache.Keys.First());
                    }
                    // calculate the date
                    var luniSolarDate = LuniSolarDate<T>.CalculateLuniSolarDateFromSolarDate(year, month, day);
                    // do not forget to add new date to dictionay :))
                    _dateCache.Add(key, luniSolarDate);
                    return luniSolarDate;
                }
            }
            // no caching, just calculate the date directly
            else
            {
                return LuniSolarDate<T>.CalculateLuniSolarDateFromSolarDate(year, month, day);
            }
        }

        //TODO: move this method to LuniSolarDate class. Implement cache checking in this LuniSolarCalendar class instead.
        public static LuniSolarDate<T> LuniSolarDateFromLunarInfo(int lunarYear, int lunarMonth, bool isLeapMonth, int lunarDay)
        {
            LuniSolarYear<T> thisLunarYear;
            if (lunarMonth >= 11)
            {
                thisLunarYear = GetLunarYear(lunarYear + 1);
            }
            else
            {
                thisLunarYear = GetLunarYear(lunarYear);
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

            DateTime monthBeginningDate = thisLunarYear.Months[monthIndex].Item2;
            var solarDate = monthBeginningDate.AddDays(lunarDay - 1);
            return new LuniSolarDate<T>(lunarYear, lunarMonth, isLeapMonth, lunarDay, solarDate);

            // TODO: validate input/output
        }

        public static LuniSolarDate<T> LuniSolarDateFromSolarDate(DateTime solarDate)
        {
            return LuniSolarDateFromSolarDate(solarDate.Year, solarDate.Month, solarDate.Day);
        }

        //public static bool IsTermBeginThisDay(DateTime solarDate) 
        //    => LuniSolarYear<VietnameseLocalInfoProvider>.GetSolarTermIndex(solarDate.AddDays(-1)) != GetSolarTermIndex(solarDate);

        #endregion

        #region Index calculators
        


        #endregion


        #region Index to string converters

  

        #endregion



        #region Hash Functions

        internal static string GetYearHash(int solarYear, float timeZone)
        {
            // TODO return int to save memory

            /* Example: year 2015 UTC-12:45
             * IN:
             *             year = 2015, 
             *         timeZone = UTC-12:45 (negative 12:45)
             * OUT:
             *         year0000 = 20150000
             *     uintTimeZone = 1275
             *             sign = -1
             *             hash = -20151275
             */
            //int year0000 = solarYear * 10000;
            //int uintTimeZone = (int)(timeZone * 100);
            //int sign = Math.Sign(timeZone);
            //return sign * (year0000 + uintTimeZone);
            return string.Format("{0:d4}{1:+0.00;-0.00;+0.00}", solarYear, timeZone);
        }

        internal static string GetLunarDateHash(int lunarYear, int lunarMonth, bool isLeapMonth, int lunarDay, float timeZone)
        {
            // TODO return int/long to save memory
            return string.Format("{0:d4}{1:d2}{2}{3:d2}{4:+0.00;-0.00;+0.00}", 
                lunarYear, lunarMonth, isLeapMonth?'1':'0', lunarDay, timeZone);
        }

        internal static string GetSolarDateHash(int solarYear, int solarMonth, int solarDay, float timeZone)
        {
            // TODO return int/long to save memory
            return string.Format("{0:d4}{1:d2}{2:d2}{3:+0.00;-0.00;+0.00}",
                solarYear, solarMonth, solarDay, timeZone);
        }

        #endregion
    }
}
