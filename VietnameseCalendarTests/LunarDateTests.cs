using Augustine.VietnameseCalendar.Core;
/*************************************************************
* ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
*  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
* // Melbourne, December 2017                      //       *
* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
*              https://github.com/datbnh/SolarLunarCalendar *
*************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Augustine.VietnameseCalendar.Core.Tests
{
    [TestClass()]
    public class LunarDateTests
    {
        [TestMethod()]
        public void LunarSolarConvertersTest_1946_March()
        {
            Console.OutputEncoding = Encoding.Unicode;
            string path = Path.Combine(Path.GetTempPath(), String.Format("VietnameseCalendar_ConvertersTest_{0}.TestLog.txt", DateTime.Now.ToString("yyyyMMddTHHmmss")));
            InitTracer(path);

            DateTime startDate = new DateTime(1946, 3, 23);
            DateTime endDate = new DateTime(1946, 3, 31);
            int daySteps = 1;
            int errors = Test(startDate, endDate, daySteps, path);
            Assert.AreEqual(0, errors, "There were {0} errors while executing the test. See {1} for more details.", errors, path);
        }

        [TestMethod()]
        public void LunarSolarConvertersTest_1946_2000()
        {
            Console.OutputEncoding = Encoding.Unicode;
            string path = Path.Combine(Path.GetTempPath(), String.Format("VietnameseCalendar_ConvertersTest_{0}.TestLog.txt", DateTime.Now.ToString("yyyyMMddTHHmmss")));
            InitTracer(path);

            DateTime startDate = new DateTime(1946, 1, 1);
            DateTime endDate = new DateTime(2999, 12, 31);
            int daySteps = 1;
            int errors = Test(startDate, endDate, daySteps, path);
            Assert.AreEqual(0, errors, "There were {0} errors while executing the test. See {1} for more details.", errors, path);
        }

        [TestMethod()]
        public void LunarSolarConvertersTest_1900_1945()
        {
            Console.OutputEncoding = Encoding.Unicode;
            string path = Path.Combine(Path.GetTempPath(), String.Format("VietnameseCalendar_ConvertersTest_{0}.TestLog.txt", DateTime.Now.ToString("yyyyMMddTHHmmss")));
            InitTracer(path);

            DateTime startDate = new DateTime(1900, 1, 1);
            DateTime endDate = new DateTime(1945, 12, 31);
            int daySteps = 1;
            int errors = Test(startDate, endDate, daySteps, path);
            Assert.AreEqual(0, errors, "There were {0} errors while executing the test. See {1} for more details.", errors, path);
        }

        private static int Test(DateTime startDate, DateTime endDate, int daySteps, string logFilepath)
        {
            int totalDays = (int)(endDate - startDate).TotalDays;

            DateTime date = startDate;
            int errors = 0;

            Trace.WriteLine(
                String.Format("Begin testing Lunar/Solar date converters..." +
                              "\r\n    Start date = {0:dd/MM/yyyy}" +
                              "\r\n    End date   = {1:dd/MM/yyyy}" +
                              "\r\n    Step       = {2} day(s)" +
                              "\r\n    Test log   : \"{3}\"" +
                              "\r\n----------------------------------------", startDate, endDate, daySteps, logFilepath.ToString()));
            DateTime testBegin = DateTime.Now;
            while (date <= endDate)
            {
                try
                {
                    LunarDate lunarDate = LunarDate.FromSolar(date, 7);
                    DateTime solarDate = LunarDate.ToSolar(lunarDate);
                    Trace.WriteLine(String.Format("{0:dd/MM/yyyy} = {1}", date, lunarDate.FullDayInfo));
                    if ((date != solarDate))
                    {
                        Trace.WriteLine(String.Format("        ---> incorrectly converted back to {0:dd/MM/yyyy}", solarDate));
                        errors++;
                    }

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(String.Format("{0:dd/MM/yyyy} ---> unhandled exception occured: {1}", date, ex.Message));
                    errors++;
                }
                date = date.AddDays(1);
            }
            var testDuration = DateTime.Now - testBegin;
            Trace.WriteLine("----------------------------------------");
            Trace.WriteLine(String.Format("Completed testing {0} days in {1}. {2} error(s) found ({3} %).",
                totalDays, testDuration.ToString(), errors, (int)(errors * 10000f / totalDays) / 100f));
            Trace.WriteLine("");
            return errors;
        }

        private static void InitTracer(string logFilePath)
        {
            Trace.Listeners.Clear();

            TextWriterTraceListener twtl = new TextWriterTraceListener(logFilePath);
            twtl.Name = "TextLogger";
            twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

            ConsoleTraceListener ctl = new ConsoleTraceListener(false);
            ctl.TraceOutputOptions = TraceOptions.DateTime;

            Trace.Listeners.Add(twtl);
            Trace.Listeners.Add(ctl);
            Trace.AutoFlush = true;

            Trace.WriteLine(String.Format("========= {0} =========", DateTime.Now));
        }

        [TestMethod()]
        public void GetDayNameTest()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine(LunarDate.GetDayName(today.Year, today.Month, today.Day));
        }
    }
}