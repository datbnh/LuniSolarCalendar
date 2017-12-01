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

        public LunarYear(int year, double timeZone)
        {
            Year = year;
            int numberOfMonths;
            DateTime month11A = Astronomy.NewMoonBeforeWinterSolstice(Year - 1, timeZone).Date;
            DateTime month11B = Astronomy.NewMoonBeforeWinterSolstice(Year, timeZone).Date;

            double jdMonth11A =
                Astronomy.LocalDateTimeToJulianDate(month11A.Year, month11A.Month, month11A.Day, timeZone);
            double jdMonth11B =
                Astronomy.LocalDateTimeToJulianDate(month11B.Year, month11B.Month, month11B.Day, timeZone);

            int k = (int)(0.5 + (jdMonth11A - 2415021.076998695) / 29.530588853);

            IsLeapYear = (jdMonth11B - jdMonth11A) > 365.0;

            if (IsLeapYear)
            {
                numberOfMonths = 14;
            }
            else
            {
                numberOfMonths = 13;
            }
            Months = new Tuple<DateTime, int, bool>[numberOfMonths];

            if (!IsLeapYear)
            {
                Months[0] = new Tuple<DateTime, int, bool>(month11A, 11, false);
                Months[numberOfMonths - 1] = new Tuple<DateTime, int, bool>(month11B, 11, false);
                for (int i = 1; i < numberOfMonths - 1; i++)
                {
                    var newMoon = 
                        Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(k + i)).AddHours(timeZone);
                    Months[i] = new Tuple<DateTime, int, bool>(newMoon, (i + 11) % 12, false);
                }
            }
            else
            {
                DateTime[] newMoons = new DateTime[numberOfMonths];
                //double[] sunLongitudeAtMonthBeginnings = new double[numberOfMonths];
                int[] majorTermAtMonthBeginnings = new int[numberOfMonths];

                // get all the new moons
                newMoons[0] = month11A;
                newMoons[numberOfMonths - 1] = month11B;
                for (int i = 1; i < numberOfMonths - 1; i++)
                {
                    newMoons[i] =
                        Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(k + i)).AddHours(timeZone);
                }

                // TODO: code optimization:
                // Uneccessary to get ALL the sun longitude. 
                // The process can be stop when first found the leap month.

                // get all sunLongitudeAtMonthBeginnings
                for (int i = 0; i < numberOfMonths; i++)
                {
                    double julianDateAtThisMonthBeginning = newMoons[i].UniversalDateTimeToJulianDate() - timeZone / 24;
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
                        Months[i] = new Tuple<DateTime, int, bool>(newMoons[i], (i + 10) % 12, true);
                    }
                    if (majorTermAtMonthBeginnings[i] == majorTermAtMonthBeginnings[i + 1])
                    {
                    } else
                    {
                        Months[i] = new Tuple<DateTime, int, bool>(newMoons[i], (i + 10) % 12, false);
                    }

                }
                Months[numberOfMonths - 1] = 
                    new Tuple<DateTime, int, bool>(newMoons[13], 11, false);
            }
        }

        //static void initLeapYear(int[][] ret)
        //{
        //    double[] sunLongitudes = new double[ret.length];
        //    for (int i = 0; i < ret.length; i++)
        //    {
        //        int[] a = ret[i];
        //        double jdAtMonthBegin = LocalToJD(a[0], a[1], a[2]);
        //        sunLongitudes[i] = SunLongitude(jdAtMonthBegin);
        //    }
        //    boolean found = false;
        //    for (int i = 0; i < ret.length; i++)
        //    {
        //        if (found)
        //        {
        //            ret[i][3] = MOD(i + 10, 12);
        //            continue;
        //        }
        //        double sl1 = sunLongitudes[i];
        //        double sl2 = sunLongitudes[i + 1];
        //        boolean hasMajorTerm = Math.floor(sl1 / PI * 6) != Math.floor(sl2 / PI * 6);
        //        if (!hasMajorTerm)
        //        {
        //            found = true;
        //            ret[i][4] = 1;
        //            ret[i][3] = MOD(i + 10, 12);
        //        }
        //    }
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Lunar year: {0}, is leap year: {1}", Year, IsLeapYear));
            foreach (var month in Months)
            {
                sb.AppendLine(String.Format("Month {0}{1}: {2}", month.Item2, month.Item3 ? "*" : " ", month.Item1.ToShortDateString()));
            }
            return sb.ToString();
        }

    }
}
