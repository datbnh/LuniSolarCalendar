using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar.Core
{
	/// <summary>
	/// Base on http://referencesource.microsoft.com/#mscorlib/system/datetime.cs.
	/// Allow negative year.
	/// </summary>
	public struct AstronomicDateTime
	{
		public int Year;
		public int Month;
		public int Day;
		public int Hour;
		public int Minute;
		public int Second;
		public int Millisecond;

		// TODO: implement the struct
	}
}
