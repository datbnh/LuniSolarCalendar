using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Augustine.VietnameseCalendar;
using System.Diagnostics;
using System.IO;
using Augustine.VietnameseCalendar.Core;

namespace Trial
{
	class Program
	{
		static void Main(string[] args)
		{
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            //DateTime testDate = new DateTime(2000, 1, 1, 8, 30, 0);
            //double jd = testDate.UniversalDateTimeToJulianDate();
            //Console.WriteLine("Test Date         : {0}", testDate);
            //Console.WriteLine("Julian Date Number: {0}", testDate.UniversalDateToJulianDayNumber());
            //Console.WriteLine("Julian Date       : {0}", jd);
            //Console.WriteLine("Convert back      : {0}", Astronomy.JulianDateToUniversalDateTime(jd));
            //Console.WriteLine();

            //DateTime dt = DateTime.Now;
            //jd = dt.UniversalDateTimeToJulianDate();
            //Console.WriteLine("It's now          : {0}", dt);
            //Console.WriteLine("Julian Date Number: {0}", dt.UniversalDateToJulianDayNumber());
            //Console.WriteLine("Julian Date       : {0}", jd);
            //Console.WriteLine("Convert back      : {0}", Astronomy.JulianDateToUniversalDateTime(jd));
            //Console.WriteLine();

            //var newMoonUtc = Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(1236));
            //var newMoonLocal = newMoonUtc.AddHours(7);
            //Console.WriteLine(newMoonUtc);
            //Console.WriteLine(newMoonLocal);
            //Console.WriteLine();

            //for (int year = 2000; year <= 2999; year++)
            //{
            //    console.writeline(astronomy.newmoon11(year, 7).tostring("dd/mm/yyyy hh:mm:ss utc+7"));
            //}
            //console.writeline();

            DateTime date = new DateTime(2033, 12, 22);
            LunarDate lunarDate = LunarDate.FromSolar(date, 7);
            DateTime solarDate = LunarDate.ToSolar(lunarDate);
            if ((date != solarDate))
            {
                Console.WriteLine(String.Format("{0:dd/MM/yyyy} = {1}", date, lunarDate));
                Console.WriteLine(String.Format("---> incorrectly converted back to {0:dd/MM/yyyy}", solarDate));
            }
            Console.ReadKey();
            Console.WriteLine();

            var timeZone = 7;
            for (int i = 2000; i <= 2010; i++)
            {
                Console.WriteLine(LunarYear.GetLunarYear(i, timeZone));
                Console.WriteLine("-------------------------");
            }

            Console.ReadKey();
		}
	}
}


