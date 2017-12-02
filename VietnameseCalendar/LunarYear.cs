using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar
{
    public class LunarYear
    {
        /// <summary>
        /// Lunar year
        /// </summary>
        public int Year;

        /// <summary>
        /// Information (month starting day, month index, is leap month) of months 
        /// from "tháng Một (11)" last lunar year to this lunar year.
        /// </summary>
        public Tuple<DateTime, int, bool>[] Months;

        public bool IsLeapYear;

        public double TimeZone;

        private DateTime month11LastYear;
        private DateTime month11ThisYear;

        public LunarYear(int year, double timeZone)
        {
            Year = year;
            TimeZone = timeZone;

            month11LastYear = Astronomy.NewMoonBeforeWinterSolstice(Year - 1, timeZone).Date;
            month11ThisYear = Astronomy.NewMoonBeforeWinterSolstice(Year, timeZone).Date;

            double jdMonth11LastYear =
                Astronomy.LocalDateTimeToJulianDate(month11LastYear.Year, month11LastYear.Month, month11LastYear.Day, timeZone);
            double jdMonth11ThisYear =
                Astronomy.LocalDateTimeToJulianDate(month11ThisYear.Year, month11ThisYear.Month, month11ThisYear.Day, timeZone);

            int k = (int)(0.5 + (jdMonth11LastYear - 2415021.076998695) / 29.530588853);

            IsLeapYear = (jdMonth11ThisYear - jdMonth11LastYear) > 365.0;

            if (!IsLeapYear)
            {
                InitNonLeapYear(k);
            }
            else
            {
                InitLeapYear(k);
            }
        }

        private void InitNonLeapYear(int k)
        {
            int numberOfMonths = 13;
            Months = new Tuple<DateTime, int, bool>[numberOfMonths];
            Months[0] = new Tuple<DateTime, int, bool>(month11LastYear, 11, false);
            Months[numberOfMonths - 1] = new Tuple<DateTime, int, bool>(month11ThisYear, 11, false);
            for (int i = 1; i < numberOfMonths - 1; i++)
            {
                var newMoon =
                    Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(k + i)).AddHours(TimeZone);
                Months[i] = new Tuple<DateTime, int, bool>(newMoon, (i + 11) % 11 + 1, false);
            }
        }

        private void InitLeapYear(int k)
        {
            int numberOfMonths = 14;
            Months = new Tuple<DateTime, int, bool>[numberOfMonths];
            DateTime[] newMoons = new DateTime[numberOfMonths];
            //double[] sunLongitudeAtMonthBeginnings = new double[numberOfMonths];
            int[] majorTermAtMonthBeginnings = new int[numberOfMonths];

            // get all the new moons
            newMoons[0] = month11LastYear;
            newMoons[numberOfMonths - 1] = month11ThisYear;
            for (int i = 1; i < numberOfMonths - 1; i++)
            {
                newMoons[i] =
                    Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(k + i)).AddHours(TimeZone);
            }

            // TODO: code optimization:
            // Uneccessary to get ALL the sun longitude. 
            // The process can stop when first found the leap month.

            // get all sunLongitudeAtMonthBeginnings
            for (int i = 0; i < numberOfMonths; i++)
            {
                double julianDateAtThisMonthBeginning = newMoons[i].UniversalDateTimeToJulianDate() - TimeZone / 24;
                //sunLongitudeAtMonthBeginnings[i] = Astronomy.GetSunLongitudeAtJulianDate(julianDateAtThisMonthBeginning);
                majorTermAtMonthBeginnings[i] =
                    (int)(Astronomy.GetSunLongitudeAtJulianDate(julianDateAtThisMonthBeginning) * 6 / Math.PI);
            }

            // determine leap month
            bool found = false;
            for (int i = 0; i < numberOfMonths - 1; i++)
            {
                // if major term at the beginning of this month is same as
                // major term at the beginning of next month, i.e. this month 
                // does not have major term, this month is leap month.
                // Only one leap month in a year.
                if (found)
                {
                    Months[i] = new Tuple<DateTime, int, bool>(newMoons[i], (i - 1 + 11) % 12, false);
                    continue;
                }
                found = majorTermAtMonthBeginnings[i] == majorTermAtMonthBeginnings[i + 1];
                Months[i] = new Tuple<DateTime, int, bool>(newMoons[i],
                    found ? (i - 1 + 11) % 12 : (i + 11) % 12, found);
            }
            Months[numberOfMonths - 1] =
                new Tuple<DateTime, int, bool>(newMoons[13], 11, false);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Lunar year: {0} {1}", Year, IsLeapYear ? "(Leap year)" : ""));
            foreach (var month in Months)
            {
                sb.AppendLine(String.Format("Month {0,2}{1}: {2:MM/dd/yy}", month.Item2 == 0 ? 12 : month.Item2, month.Item3 ? "*" : " ", month.Item1.ToShortDateString()));
            }
            return sb.ToString();
        }

    }
}
