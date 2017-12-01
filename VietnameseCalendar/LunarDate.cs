using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar
{
	public class LunarDate
	{
		public static string[] MonthNames = { "Giêng", "Hai", "Ba", "Tư",
											"Năm", "Sáu", "Bảy", "Tám",
											"Chín", "Mười", "Một (11)", "Chạp"};
		public static string[] Stems = {"Giáp", "Ất", "Bính", "Đinh", "Mậu",
										 "Kỷ", "Canh", "Tân", "Nhâm", "Quý"};
		public static string[] Branches = {"Tý", "Sửu", "Dần", "Mão",
											 "Thìn", "Tỵ", "Ngọ", "Mùi",
											 "Thân", "Dậu", "Tuất", "Hợi"};

		public int Year;
		public int Month;
		public bool LeapMonth;
		public int Day;

		public LunarDate(int year, int month, bool leapMonth, int day)
		{
			Year = year;
			Month = month;
			LeapMonth = leapMonth;
			Day = day;
		}

		// Year
		/// <summary>
		/// Get zero-based stem index (0: Giáp, 1: Ất...) of the current year.
		/// </summary>
		/// <returns></returns>
		public int GetStemIndex()
		{
			return (Year + 6) % 10;
		}

		/// <summary>
		/// Get zero-based branch index (0: Tý, 1: Sửu...) of the current year.
		/// </summary>
		/// <returns></returns>
		public int GetBranchIndex()
		{
			return (Year + 8) % 12;
		}
		public string GetStemName()
		{
			return Stems[GetStemIndex()];
		}
		public string GetBranchName()
		{
			return Branches[GetBranchIndex()];
		}
		public string GetYearName()
		{
			return GetStemName() + " " + GetBranchName();
		}
		// Month
		public int GetMonthStemIndex()
		{
			return (Year * 12 + Month + 3) % 10;
		}
		public int GetMonthBranchNumber()
		{
			switch (Month)
			{
				case 11:
					return 0;
				case 12:
					return 1;
				default:
					return Month + 1;
			}
		}
		public string GetMonthStemName()
		{
			return Stems[GetMonthStemIndex()];
		}
		public string GetMonthBranchName()
		{
			return Branches[GetMonthBranchNumber()];
		}
		public string GetMonthFullName()
		{
			return GetMonthStemName() + " " + GetMonthBranchName();
		}
		public string GetMonthName()
		{
			return MonthNames[Month - 1];
		}
		public string IsLeapString()
		{
			return (LeapMonth ? " (Nhuận)" : "");
		}

		public override string ToString()
		{
			return Year + "." + Month + "." + Day + "." + " " + "Ngày " + Day + " tháng " + GetMonthName() + " (" + GetMonthFullName() + ")" + IsLeapString() + " năm " + GetYearName();
		}
	}

}
