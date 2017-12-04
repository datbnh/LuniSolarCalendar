using Microsoft.VisualStudio.TestTools.UnitTesting;
using Augustine.VietnameseCalendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Augustine.VietnameseCalendar.Core.Tests
{
	[TestClass()]
	public class AstronomyTests
	{
		[TestMethod()]
		public void UniversalDateToJulianDayNumberTest()
		{
			Assert.AreEqual(0, Astronomy.UniversalDateToJulianDayNumber(-4712, 1, 1), 0.0000001);
			DateTime date = new DateTime(2000, 1, 1);
			Assert.AreEqual(2451545, date.UniversalDateToJulianDayNumber(), 0.0000001, String.Format("Input date: {0}", date));
			Assert.AreEqual(2299160, Astronomy.UniversalDateToJulianDayNumber(1582, 10, 4), 0.0000001);
			Assert.AreEqual(2299161, Astronomy.UniversalDateToJulianDayNumber(1582, 10, 15), 0.0000001);
		}

		[TestMethod()]
		public void UniversalDateTimeToJulianDateTest()
		{
			DateTime date = new DateTime(2000, 1, 1);
			Assert.AreEqual(2451544.5, date.UniversalDateTimeToJulianDate(), 0.0000001);
		}

		[TestMethod()]
		public void JulianDayNumberToUniversalDateTest()
		{
			int[] results = Astronomy.JulianDayNumberToUniversalDate(0);
			Assert.AreEqual(-4712, results[0]);
			Assert.AreEqual(1, results[1]);
			Assert.AreEqual(1, results[2]);

			results = Astronomy.JulianDayNumberToUniversalDate(2299160);
			Assert.AreEqual(1582, results[0]);
			Assert.AreEqual(10, results[1]);
			Assert.AreEqual(4, results[2]);

			results = Astronomy.JulianDayNumberToUniversalDate(2299161);
			Assert.AreEqual(1582, results[0]);
			Assert.AreEqual(10, results[1]);
			Assert.AreEqual(15, results[2]);
		}

		[TestMethod()]
		public void JulianDayNumberToUniversalDateTimeTest()
		{
			DateTime dateTime1 = new DateTime(2000, 1, 1, 6, 15, 11);
			DateTime dateTime2 = new DateTime(2000, 1, 1, 16, 15, 11);
			Assert.AreEqual(dateTime1, Astronomy.JulianDateToUniversalDateTime(dateTime1.UniversalDateTimeToJulianDate()));
			Assert.AreEqual(dateTime2, Astronomy.JulianDateToUniversalDateTime(dateTime2.UniversalDateTimeToJulianDate()));
			//dateTime1 = new DateTime(1582, 10, 15, 6, 15, 11);
			//dateTime2 = new DateTime(1582, 10, 4, 16, 15, 11);
			//Assert.AreEqual(dateTime1, Astronomy.JulianDateToUniversalDateTime(dateTime1.UniversalDateTimeToJulianDate()));
			//Assert.AreEqual(dateTime2, Astronomy.JulianDateToUniversalDateTime(dateTime2.UniversalDateTimeToJulianDate()));
		}

		[TestMethod()]
		public void GetNewMoonInJulianDateTest()
		{
			//int k = 1236;
			double jd = Astronomy.GetNewMoon(1236);
			TimeSpan diff = (new DateTime(1999, 12, 7, 22, 32, 42)) - Astronomy.JulianDateToUniversalDateTime(jd);
			Assert.AreEqual(2451520.4393767994, jd, 1E-10);
			int tollerance = 500;// milliseconds
			bool isPassed = (diff < TimeSpan.FromMilliseconds(tollerance))
				&& (diff > -TimeSpan.FromMilliseconds(tollerance));
			Assert.IsTrue(isPassed, String.Format("There wwas a difference of {0} between expected and retured value.", diff));
		}

		[TestMethod()]
		public void GetSunLongitudeAtJulianDateTest()
		{
			Assert.AreEqual(4.986246180809974, Astronomy.GetSunLongitudeAtJulianDate(2451550.2083333335), 1E-10);
			//Assert.AreEqual(4.453168980086705, Astronomy.GetSunLongitudeAtJulianDate(2451520.2083333335), 1E-10);
		}
	}
}