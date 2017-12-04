using Augustine.VietnameseCalendar.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for AugustineCalendarMonth.xaml
    /// </summary>
    public partial class AugustineCalendarMonth : UserControl
    {

        public readonly string[] dayOfWeekLabels =
            {"Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"};
        public const int DAYS_PER_WEEK = 7;
        public const int WEEKS = 6;

        private const int ROW_IDX_MONTH_LABEL = 0;
        private const int ROW_IDX_DOW_LABELS = 1;
        private const int ROW_IDX_CW_LABELS = 2;
        private const int ROW_IDX_DAYS = 2;
        private const int ROW_IDX_TODAY_LABEL = 8;
        private const int ROW_IDX_SELECTED_DATE_LABEL = 9;

        private const int COL_IDX_MONTH_LABEL = 0;
        private const int COL_IDX_CW_LABELS = 0;
        private const int COL_IDX_DOW_LABELS = 1;
        private const int COL_IDX_DAYS = 1;
        private const int COL_IDX_TODAY_LABEL = 0;
        private const int COL_IDX_SELECTED_DATE_LABEL = 0;

        private DayOfWeek firstDayOfWeek = DayOfWeek.Sunday;

        private Label monthLabel;
        private Label todayInfoLabel;
        private Label selectedDateInfoLabel;
        private Label[] cwLabels;
        private AugustineCalendarDay[] days;
        private int thisMonth;
        private int thisYear;
        private DateTime today;
        private DateTime pageBegin;
        private DateTime monthBegin;
        private DateTime monthEnd;
        private DateTime selectedDate;
        private LunarDate monthBeginLunarDate;
        private LunarDate monthEndLunarDate;

        public AugustineCalendarMonth()
        {
            InitializeComponent();

            today = DateTime.Today;
            thisMonth = today.Month;
            thisYear = today.Year;

            InitializeDays();
            InitializeMonthLabel();
            InitializeTodayLabel();
            InitializeSelectedDateLabel();
            InitializeDayOfWeekLabels();
            //InitializeCwLabels();
        }

        private void InitializeTodayLabel()
        {
            todayInfoLabel = new Label
            {
                FontSize = 12f,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0, 3, 0, 0),
                Cursor = Cursors.Hand,
            };
            todayInfoLabel.MouseUp += TodayInfoLabel_MouseUp;
            Grid.SetColumn(todayInfoLabel, COL_IDX_TODAY_LABEL);
            Grid.SetColumnSpan(todayInfoLabel, MainGrid.ColumnDefinitions.Count - COL_IDX_MONTH_LABEL);
            Grid.SetRow(todayInfoLabel, ROW_IDX_TODAY_LABEL);
            MainGrid.Children.Add(todayInfoLabel);
            UpdateTodayInfoLabel();
        }

        private void TodayInfoLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            selectedDate = today;
            thisMonth = today.Month;
            thisYear = today.Year;
            UpdateEverything();
        }

        private void UpdateTodayInfoLabel()
        {
            todayInfoLabel.Content 
                = "Hôm nay: " + GetFullDayInfo(today);
        }

        private void InitializeSelectedDateLabel()
        {
            selectedDateInfoLabel = new Label
            {
                FontSize = 12f,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0, 0, 0, 0),
            };
            Grid.SetColumn(selectedDateInfoLabel, COL_IDX_SELECTED_DATE_LABEL);
            Grid.SetColumnSpan(selectedDateInfoLabel, MainGrid.ColumnDefinitions.Count - COL_IDX_MONTH_LABEL);
            Grid.SetRow(selectedDateInfoLabel, ROW_IDX_SELECTED_DATE_LABEL);
            MainGrid.Children.Add(selectedDateInfoLabel);
        }

        private void UpdateSelectedDateInfoLabel()
        {
            if (selectedDate != null && selectedDate != today)
            {
                selectedDateInfoLabel.Visibility = Visibility.Visible;
                selectedDateInfoLabel.Content =
                        "Đang chọn: " + GetFullDayInfo(selectedDate);
            }
            else
            {
                selectedDateInfoLabel.Visibility = Visibility.Collapsed;
            }
        }

        public void InitializeMonthLabel()
        {
            monthLabel = new Label
            {
                FontSize = 20f,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0,0,0,0),
            };
            Grid.SetColumn(monthLabel, COL_IDX_MONTH_LABEL);
            Grid.SetColumnSpan(monthLabel, MainGrid.ColumnDefinitions.Count - COL_IDX_MONTH_LABEL);
            Grid.SetRow(monthLabel, ROW_IDX_MONTH_LABEL);
            MainGrid.Children.Add(monthLabel);
            UpdateMonthLabels();
        }

        public void InitializeDayOfWeekLabels()
        {
            for (int i = 0; i < DAYS_PER_WEEK; i++)
            {
                int currentDayOfWeek = (int)firstDayOfWeek + i;
                if (currentDayOfWeek >= 7)
                    currentDayOfWeek -= 7;
                Label label = new Label
                {
                    Content = dayOfWeekLabels[currentDayOfWeek],
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    FontSize = 14f,
                };
                Grid.SetColumn(label, COL_IDX_DOW_LABELS + i);
                Grid.SetRow(label, ROW_IDX_DOW_LABELS);
                MainGrid.Children.Add(label);
            }
        }

        public void InitializeCwLabels()
        {
            cwLabels = new Label[WEEKS];
            for (int i = 0; i < WEEKS; i++)
            {
                Label label = new Label
                {
                    Content = i + 1,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 1, 0),
                };
                cwLabels[i] = label;

                Grid.SetColumn(label, COL_IDX_CW_LABELS);
                Grid.SetRow(label, ROW_IDX_CW_LABELS + i);
                MainGrid.Children.Add(label);
            }
        }

        private void InitializeDays()
        {
            UpdateReferenceDates();
            days = new AugustineCalendarDay[DAYS_PER_WEEK * WEEKS];

            for (int i = 0; i < DAYS_PER_WEEK; i++)
            {
                for (int j = 0; j < WEEKS; j++)
                {
                    AugustineCalendarDay day = new AugustineCalendarDay
                    {
                        SolarDate = pageBegin.AddDays(i + j * 7),
                    };
                    days[i + j * 7] = day;
                    StyleThisDay(day);

                    // TODO: combine with code in UpdateDays()
                    if (day.SolarDate == monthBegin)
                    {
                        monthBeginLunarDate = day.LunarDate;
                    }
                    else if (day.SolarDate == monthEnd)
                    {
                        monthEndLunarDate = day.LunarDate;
                    }

                    day.MouseEnter += Day_MouseEnter;
                    day.MouseLeave += Day_MouseLeave;
                    day.MouseUp += Day_MouseUp;

                    Grid.SetColumn(day, COL_IDX_DAYS + i);
                    Grid.SetRow(day, ROW_IDX_DAYS + j);
                    MainGrid.Children.Add(day);
                }
            }
        }

        private void Day_MouseEnter(object sender, MouseEventArgs e)
        {
            ((AugustineCalendarDay)sender).BorderStyle = BorderStyles.HotTrack;
        }

        private void Day_MouseLeave(object sender, MouseEventArgs e)
        {
            var day = ((AugustineCalendarDay)sender);
            if (day.SolarDate == selectedDate)
            {
                day.BorderStyle = BorderStyles.Selected;
            }
            else
            {
                day.BorderStyle = BorderStyles.Normal;
            }

        }

        private void Day_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var day = ((AugustineCalendarDay)sender);
            selectedDate = day.SolarDate;

            if (day.SolarDate < monthBegin)
            {
                if (thisMonth == 1)
                {
                    thisMonth = 12;
                    thisYear--;
                }
                else
                    thisMonth--;

            }
            else if (day.SolarDate > monthEnd)
            {
                if (thisMonth == 12)
                {
                    thisMonth = 1;
                    thisYear++;
                }
                else
                    thisMonth++;
            }

            UpdateEverything();
        }

        private void UpdateEverything()
        {
            UpdateReferenceDates();
            UpdateDays();
            UpdateMonthLabels();
            UpdateSelectedDateInfoLabel();
        }

        private void UpdateDays()
        {
            for (int i = 0; i < DAYS_PER_WEEK; i++)
            {
                for (int j = 0; j < WEEKS; j++)
                {
                    var day = days[i + j * 7];
                    day.SolarDate = pageBegin.AddDays(i + j * 7);
                    if (day.SolarDate == monthBegin)
                    {
                        monthBeginLunarDate = day.LunarDate;
                    } else if (day.SolarDate == monthEnd)
                    {
                        monthEndLunarDate = day.LunarDate;
                    }
                    StyleThisDay(day);
                }
            }         
        }

        private void UpdateMonthLabels()
        {
            String lunarInfo = "";
            if (monthBeginLunarDate.Year != monthEndLunarDate.Year)
            {
                lunarInfo = String.Format("Th. {0}{1} năm {2}/Th. {3}{4} năm {5}",
                    monthBeginLunarDate.GetMonthName(),
                    monthBeginLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthBeginLunarDate.GetYearName(),
                    monthEndLunarDate.GetMonthName(),
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.GetYearName());
            }
            else if (monthBeginLunarDate.Month == monthEndLunarDate.Month &&
                    monthBeginLunarDate.IsLeapMonth == monthEndLunarDate.IsLeapMonth)
            {
                lunarInfo = String.Format("Th. {0}{1} năm {2}",
                    monthEndLunarDate.GetMonthName(),
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.GetYearName());
            }
            else
            {
                lunarInfo = String.Format("Th. {0}{1}/{2}{3} năm {4}",
                    monthBeginLunarDate.GetMonthName(),
                    monthBeginLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.GetMonthName(),
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.GetYearName());
            }

            String solarInfo = String.Format("Th. {0}/{1}", thisMonth, thisYear);
            
            monthLabel.Content = solarInfo + " - " + lunarInfo;
        }

        private void StyleThisDay(AugustineCalendarDay day)
        {
            // TODO check special days

            // reset Style
            day.FaceStyle = FaceStyles.Normal;
            day.BorderStyle = BorderStyles.Normal;

            if (day.SolarDate.DayOfWeek == DayOfWeek.Sunday)
            {
                day.FaceStyle = FaceStyles.Sunday;
            }
            else if (day.SolarDate.DayOfWeek == DayOfWeek.Saturday)
            {
                day.FaceStyle = FaceStyles.Saturday;
            }

            if (day.SolarDate == today)
            {
                day.FaceStyle = FaceStyles.Today;
            }

            if (day.SolarDate.Month != thisMonth)
            {
                day.FaceStyle = FaceStyles.GrayedOut;
            }


            if (day.SolarDate == selectedDate)
            {
                day.BorderStyle = BorderStyles.Selected;
            }
        }

        private void UpdateReferenceDates()
        {
            monthBegin = new DateTime(thisYear, thisMonth, 1);
            monthEnd = new DateTime(thisYear, thisMonth, DateTime.DaysInMonth(thisYear, thisMonth));
            var offset = monthBegin.DayOfWeek - firstDayOfWeek;
            pageBegin = monthBegin.AddDays(-offset);
            if (pageBegin.Month == monthBegin.Month)
            {
                pageBegin = pageBegin.AddDays(-7);
            }
        }

        private String GetFullDayInfo(DateTime date)
        {
            
            return String.Format("{0} {1:dd/MM/yyyy} - {2} âm lịch",
                dayOfWeekLabels[(int)date.DayOfWeek], date,
                LunarDate.FromSolar(date.Year, date.Month, date.Day, 7));
        }
    }
}
