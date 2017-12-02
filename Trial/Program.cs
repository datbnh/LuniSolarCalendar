using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Augustine.VietnameseCalendar;
using System.Diagnostics;
using System.IO;

namespace Trial
{
	class Program
	{
		static void Main(string[] args)
		{
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Trace.Listeners.Clear();
            String path = Path.Combine(Path.GetTempPath(), AppDomain.CurrentDomain.FriendlyName + ".log");
            TextWriterTraceListener twtl = new TextWriterTraceListener(path);
            twtl.Name = "TextLogger";
            twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

            ConsoleTraceListener ctl = new ConsoleTraceListener(false);
            ctl.TraceOutputOptions = TraceOptions.DateTime;

            Trace.Listeners.Add(twtl);
            Trace.Listeners.Add(ctl);
            Trace.AutoFlush = true;

            Trace.WriteLine(path);

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

            //DateTime date = new DateTime(2000, 10, 26);
            //while (date < new DateTime(2000, 10, 29))
            DateTime date = new DateTime(2000, 1, 1);
            while (date < new DateTime(2999, 12, 31))
            {
                //LunarDate.SolarToLunar(date.Year, date.Month, date.Day, 7);
                Trace.WriteLine(String.Format("{0:dd/MM/yyyy} = {1}", date, LunarDate.SolarToLunar(date.Year, date.Month, date.Day, 7)));
                date = date.AddDays(15);
            }
            Console.ReadKey();
            Console.WriteLine();

            var timeZone = 7;
            for (int i = 2000; i <= 2010; i++)
            {
                Console.WriteLine(new LunarYear(i, timeZone));
                Console.WriteLine("-------------------------");
            }

            Console.ReadKey();
		}
	}
}


