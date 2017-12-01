using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Augustine.VietnameseCalendar;

namespace Trial
{
	class Program
	{
		static void Main(string[] args)
		{
			DateTime testDate = new DateTime(2000, 1, 1, 8, 30, 0);
			double jd = testDate.UniversalDateTimeToJulianDate();
			Console.WriteLine("Test Date         : {0}", testDate);
			Console.WriteLine("Julian Date Number: {0}", testDate.UniversalDateToJulianDayNumber());
			Console.WriteLine("Julian Date       : {0}", jd);
			Console.WriteLine("Convert back      : {0}", Astronomy.JulianDateToUniversalDateTime(jd));
			Console.WriteLine();

			DateTime dt = DateTime.Now;
			jd = dt.UniversalDateTimeToJulianDate();
			Console.WriteLine("It's now          : {0}", dt);
			Console.WriteLine("Julian Date Number: {0}", dt.UniversalDateToJulianDayNumber());
			Console.WriteLine("Julian Date       : {0}", jd);
			Console.WriteLine("Convert back      : {0}", Astronomy.JulianDateToUniversalDateTime(jd));
			Console.WriteLine();

			var newMoonUtc = Astronomy.JulianDateToUniversalDateTime(Astronomy.GetNewMoon(1236));
			var newMoonLocal = newMoonUtc.AddHours(7);
			Console.WriteLine(newMoonUtc);
			Console.WriteLine(newMoonLocal);
			Console.WriteLine();

			for (int year = 2000; year < 2020; year++)
			{
				Console.WriteLine(Astronomy.NewMoonBeforeWinterSolstice(year, 7));
			}

            Console.WriteLine();
            Console.WriteLine(new LunarYear(2004, 7));

            Console.ReadKey();
		}
	}
}

