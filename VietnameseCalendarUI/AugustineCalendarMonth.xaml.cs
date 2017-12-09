/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for AugustineCalendarMonth.xaml
    /// </summary>
    public partial class AugustineCalendarMonth : UserControl
    {
        public static readonly string[] DayOfWeekLabels =
            {"Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"};
        public static readonly int DAYS_PER_WEEK = 7;
        public static readonly int WEEKS = 6;
        public static readonly DateTime MINIMUM_SUPPORTED_DATE = new DateTime(1900, 1, 1);

        public DateTime SelectedDate { get; private set; }

        #region === Private Fields and Constants

        private const int ROW_IDX_MONTH_LABEL = 0;
        private const int ROW_IDX_SOLAR_TERM_DECORATOR = 1;
        private const int ROW_IDX_DOW_LABELS = 2;
        private const int ROW_IDX_CW_LABELS = 3;
        private const int ROW_IDX_DAYS = 3;
        private const int ROW_IDX_TODAY_LABEL = 9;
        private const int ROW_IDX_SELECTED_DATE_LABEL = 10;
        private const int MAX_ROW_SPAN = 11;

        private const int COL_IDX_MONTH_LABEL = 0;
        private const int COL_IDX_CW_LABELS = 0;
        private const int COL_IDX_DOW_LABELS = 1;
        private const int COL_IDX_DAYS = 1;
        private const int COL_IDX_TODAY_LABEL = 0;
        private const int COL_IDX_SELECTED_DATE_LABEL = 0;
        private const int MAX_COL_SPAN = 8;

        private DayOfWeek firstDayOfWeek = DayOfWeek.Sunday;

        private Label monthLabel;
        private UserControl solarTermBar;
        private Label todayInfoLabel;
        private Label selectedDateInfoLabel;
        private Label[] cwLabels;
        private AugustineCalendarDay[] days;
        private StackPanel datePickerStackPanel;
        private DatePicker datePicker;

        private int thisMonth;
        private int thisYear;
        private DateTime today;
        private DateTime pageBegin;
        private DateTime monthBegin;
        private DateTime monthEnd;
        private LunarDate monthBeginLunarDate;
        private LunarDate monthEndLunarDate;

        #endregion

        public AugustineCalendarMonth()
        {
            InitializeComponent();

            today = DateTime.Today;
            SelectedDate = today;

            InitializeDays();
            InitializeMonthLabel();
            //InitializeSolarTermBar();
            InitializeTodayLabel();
            InitializeSelectedDateLabel();
            InitializeDayOfWeekLabels();
            //InitializeCwLabels();
        }

        #region === UI Component Initializers ===

        private void InitializeMonthLabel()
        {
            monthLabel = new Label
            {
                FontSize = 20f,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0,0,0,0),
            };
            monthLabel.MouseWheel += MonthLabel_MouseWheel;

            Grid.SetColumn(monthLabel, COL_IDX_MONTH_LABEL);
            Grid.SetColumnSpan(monthLabel, MainGrid.ColumnDefinitions.Count - COL_IDX_MONTH_LABEL);
            Grid.SetRow(monthLabel, ROW_IDX_MONTH_LABEL);
            MainGrid.Children.Add(monthLabel);
            UpdateMonthLabels();
        }

        private void InitializeSolarTermBar()
        {
            solarTermBar = new UserControl();
            solarTermBar.Content = SolarTermDecorator.CreateSolarTermBar(today.Year, 7);
            Grid.SetRow(solarTermBar, ROW_IDX_SOLAR_TERM_DECORATOR);
            Grid.SetColumn(solarTermBar, 0);
            Grid.SetColumnSpan(solarTermBar, MAX_COL_SPAN);
            MainGrid.Children.Add(solarTermBar);

        }

        private void InitializeTodayLabel()
        {
            todayInfoLabel = new Label
            {
                FontSize = 12f,
                Background = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Stretch,
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

        private void InitializeSelectedDateLabel()
        {
            // the label
            selectedDateInfoLabel = new Label
            {
                FontSize = 12f,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.PowderBlue,
                Margin = new Thickness(0),
                Padding = new Thickness(0, 0, 0, 0),
                Cursor = Cursors.Hand,
            };
            selectedDateInfoLabel.MouseUp += SelectedDateInfoLabel_MouseUp;
            Grid.SetColumn(selectedDateInfoLabel, COL_IDX_SELECTED_DATE_LABEL);
            Grid.SetColumnSpan(selectedDateInfoLabel, MainGrid.ColumnDefinitions.Count - COL_IDX_MONTH_LABEL);
            Grid.SetRow(selectedDateInfoLabel, ROW_IDX_SELECTED_DATE_LABEL);
            MainGrid.Children.Add(selectedDateInfoLabel);

            // the datePicker and the "Finish Selecting" button
            datePicker = new DatePicker
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0, 0, 0, 0),
                Background = Brushes.White,
                SelectedDate = SelectedDate,
                FirstDayOfWeek = firstDayOfWeek,
            };
            datePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;

            Button datePickerButton = new Button
            {
                Content = "✔",
                HorizontalAlignment = HorizontalAlignment.Right,
                //FontFamily = new FontFamily("Segoe UI Symbol"),
                Background = Brushes.White,
                Foreground = Brushes.DarkGreen,
                Padding = new Thickness(3, 0, 3, 0),
            };
            datePickerButton.Click += DatePickerButton_Click;

            datePickerStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            datePickerStackPanel.Visibility = Visibility.Collapsed;

            Grid.SetColumn(datePickerStackPanel, COL_IDX_SELECTED_DATE_LABEL);
            Grid.SetColumnSpan(datePickerStackPanel, MainGrid.ColumnDefinitions.Count - COL_IDX_MONTH_LABEL);
            Grid.SetRow(datePickerStackPanel, ROW_IDX_SELECTED_DATE_LABEL - 1);
            Grid.SetRowSpan(datePickerStackPanel, 2);

            datePickerStackPanel.Children.Add(datePicker);
            datePickerStackPanel.Children.Add(datePickerButton);
            MainGrid.Children.Add(datePickerStackPanel);
        }

        private void InitializeDayOfWeekLabels()
        {
            for (int i = 0; i < DAYS_PER_WEEK; i++)
            {
                int currentDayOfWeek = (int)firstDayOfWeek + i;
                if (currentDayOfWeek >= 7)
                    currentDayOfWeek -= 7;
                Label label = new Label
                {
                    Content = DayOfWeekLabels[currentDayOfWeek],
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    FontSize = 14f,
                };
                Grid.SetColumn(label, COL_IDX_DOW_LABELS + i);
                Grid.SetRow(label, ROW_IDX_DOW_LABELS);
                MainGrid.Children.Add(label);
            }
        }

        private void InitializeCwLabels()
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
                    //day.ToolTip = day.LunarDate;
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
                    day.MouseWheel += Day_MouseWheel;

                    Grid.SetColumn(day, COL_IDX_DAYS + i);
                    Grid.SetRow(day, ROW_IDX_DAYS + j);
                    MainGrid.Children.Add(day);
                }
            }
        }

        #endregion

        #region === Event Handlers ===

        private void MonthLabel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0) { SelectedDate = SelectedDate.AddYears(1); }
            else { SelectedDate = SelectedDate.AddYears(-1); }
            UpdateEverything();
        }


        private void Day_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0) { SelectedDate = SelectedDate.AddMonths(1); }
            else { SelectedDate = SelectedDate.AddMonths(-1); }
            UpdateEverything();
        }

        private void Day_MouseEnter(object sender, MouseEventArgs e)
        {
            ((AugustineCalendarDay)sender).BorderStyle = BorderStyles.HotTrack;
        }

        private void Day_MouseLeave(object sender, MouseEventArgs e)
        {
            var day = ((AugustineCalendarDay)sender);
            if (day.SolarDate == SelectedDate)
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
            SelectedDate = day.SolarDate;

            // TODO: only update the border of selected day when page is not changed
            //if (selectedDate < monthBegin || selectedDate > monthEnd)
            //{
            //    //
            //}

            UpdateEverything();
        }

        private void TodayInfoLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectedDate = today;
            thisMonth = today.Month;
            thisYear = today.Year;
            UpdateEverything();
        }

        private void SelectedDateInfoLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            datePicker.SelectedDate = SelectedDate;
            datePickerStackPanel.Visibility = Visibility.Visible;
            //selectedDateInfoLabel.Visibility = Visibility.Collapsed;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDate = ((DatePicker)sender).SelectedDate.Value;
            UpdateEverything();
        }

        private void DatePickerButton_Click(object sender, RoutedEventArgs e)
        {
            datePickerStackPanel.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region === UI Updaters ===

        private void UpdateReferenceDates()
        {
            if (SelectedDate < MINIMUM_SUPPORTED_DATE)
            {
                SelectedDate = MINIMUM_SUPPORTED_DATE;
            }

            thisMonth = SelectedDate.Month;
            thisYear = SelectedDate.Year;
            monthBegin = new DateTime(thisYear, thisMonth, 1);
            monthEnd = new DateTime(thisYear, thisMonth, DateTime.DaysInMonth(thisYear, thisMonth));

            if (monthBegin == new DateTime(1, 1, 1))
            {
                pageBegin = monthBegin;
                return;
            }

            var offset = monthBegin.DayOfWeek - firstDayOfWeek;
            pageBegin = monthBegin.AddDays(-offset);
            if (pageBegin.Month == monthBegin.Month)
            {
                pageBegin = pageBegin.AddDays(-7);
            }
        }


        private void UpdateMonthLabels()
        {
            String lunarInfo = "";
            if (monthBeginLunarDate.Year != monthEndLunarDate.Year)
            {
                lunarInfo = String.Format("Th. {0}{1} năm {2}/Th. {3}{4} năm {5}",
                    monthBeginLunarDate.MonthName,
                    monthBeginLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthBeginLunarDate.YearName,
                    monthEndLunarDate.MonthName,
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.YearName);
            }
            else if (monthBeginLunarDate.Month == monthEndLunarDate.Month &&
                    monthBeginLunarDate.IsLeapMonth == monthEndLunarDate.IsLeapMonth)
            {
                lunarInfo = String.Format("Th. {0}{1} năm {2}",
                    monthEndLunarDate.MonthName,
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.YearName);
            }
            else
            {
                lunarInfo = String.Format("Th. {0}{1}/{2}{3} năm {4}",
                    monthBeginLunarDate.MonthName,
                    monthBeginLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.MonthName,
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.YearName);
            }

            String solarInfo = String.Format("Th. {0}/{1}", thisMonth, thisYear);

            monthLabel.Content = solarInfo + " - " + lunarInfo;
        }

        private void UpdateSolarTermBar()
        {
            solarTermBar.Content = SolarTermDecorator.CreateSolarTermBar(SelectedDate.Year, 7);
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

        private void UpdateTodayInfoLabel()
        {
            todayInfoLabel.Content 
                = "Hôm nay: " + GetFullDayInfo(today);
        }

        private void UpdateSelectedDateInfoLabel()
        {
            if (SelectedDate != null && SelectedDate != today)
            {
                selectedDateInfoLabel.Visibility = Visibility.Visible;
                selectedDateInfoLabel.Content =
                        "Đang chọn: " + GetFullDayInfo(SelectedDate);
            }
            else
            {
                selectedDateInfoLabel.Visibility = Visibility.Collapsed;
            }
        }


        private void UpdateEverything()
        {
            UpdateReferenceDates();
            UpdateDays();
            UpdateMonthLabels();
            //UpdateSolarTermBar();
            UpdateSelectedDateInfoLabel();
        }
        
        #endregion

        #region === Miscelanous ===

        private void StyleThisDay(AugustineCalendarDay day)
        {
            // TODO check special days

            // reset Style
            day.FaceStyle = FaceStyles.Normal;
            day.BorderStyle = BorderStyles.Normal;


            // face style
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

            // border style
            if (day.SolarDate == SelectedDate)
            {
                day.BorderStyle = BorderStyles.Selected;
            }
        }

        internal static String GetFullDayInfo(DateTime date)
        { 
            return String.Format("{0} {1:dd/MM/yyyy} - {2}",
                DayOfWeekLabels[(int)date.DayOfWeek], date,
                LunarDate.FromSolar(date.Year, date.Month, date.Day, 7));
        }

        public void SelectDate(DateTime date)
        {
            SelectedDate = date;
            UpdateEverything();
        }

        #endregion
    }
}
