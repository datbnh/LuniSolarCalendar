/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core;
using Augustine.VietnameseCalendar.Core.LuniSolarCalendar;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using static Augustine.VietnameseCalendar.UI.TextSize;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for AugustineCalendarMonth.xaml
    /// </summary>
    public partial class AugustineCalendarMonth : UserControl
    {
        #region === Constant Fields ===

        public static readonly string[] DayOfWeekShortLabels =
            {"CN", "T2", "T3", "T4", "T5", "T6", "T7"};
        public static readonly string[] DayOfWeekMediumLabels =
            {"C.Nhật", "T.Hai", "T.Ba", "T.Tư", "T.Năm", "T.Sáu", "T.Bảy"};
        public static readonly string[] DayOfWeekFullLabels =
            {"Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"};
        public static readonly int DAYS_PER_WEEK = 7;
        public static readonly int WEEKS = 6;
        public static readonly DateTime MINIMUM_SUPPORTED_DATE = new DateTime(1900, 1, 1);

        private const int ROW_IDX_MONTH_LABEL = 0;
        private const int ROW_IDX_SOLAR_TERM_DECORATOR = 1;
        private const int ROW_IDX_DOW_LABELS = 2;
        private const int ROW_IDX_CW_LABELS = 3;
        private const int ROW_IDX_DAYS = 3;
        private const int ROW_IDX_TODAY_LABEL = 9;
        private const int ROW_IDX_SELECTED_DATE_LABEL = 10;
        private const int MAX_ROW_SPAN = 11;

        private const int COL_IDX_MONTH_LABEL = 0;
        private const int COL_IDX_SOLAR_TERM_DECORATOR = 0;
        private const int COL_IDX_CW_LABELS = 0;
        private const int COL_IDX_DOW_LABELS = 1;
        private const int COL_IDX_DAYS = 1;
        private const int COL_IDX_TODAY_LABEL = 1;
        private const int COL_IDX_SELECTED_DATE_LABEL = 1;
        private const int MAX_COL_SPAN = 10;

        #endregion

        #region === Fields ===

        private DayOfWeek firstDayOfWeek = DayOfWeek.Sunday;

        private Label monthLabelSolar;
        private Label monthLabelLunar;
        private UserControl solarTermBar;
        private Label todayInfoLabel;
        private Label selectedDateInfoLabel;
        private Label[] cwLabels;
        private Label[] dayOfWeekLabels;
        private DayTile[] days;
        private StackPanel datePickerStackPanel;
        private DatePicker datePicker;

        //private int thisMonth;
        //private int thisYear;
        private DateTime today;
        private DateTime pageBegin;
        private DateTime monthBegin;
        private DateTime monthEnd;
        private LuniSolarDate<VietnameseLocalInfoProvider> monthBeginLunarDate;
        private LuniSolarDate<VietnameseLocalInfoProvider> monthEndLunarDate;


        //private Binding backgroundBinding;
        private Binding foregroundBinding;
        private Binding borderBinding;

        private int cellSpacing = 0;

        #endregion

        public AugustineCalendarMonth()
        {
            InitializeComponent();
            FontFamily = FontManager.Body;

            UseLayoutRounding = true;

            today = DateTime.Today;
            SelectedDate = today;

            InitializeBinders();
            InitializeDays();
            InitializeMonthLabel();
            InitializeSolarTermBar();
            InitializeTodayLabel();
            InitializeSelectedDateLabel();
            InitializeDayOfWeekLabels();
            InitializeCwLabels();


            //Theme = Themes.Light;
        }

        #region === Properties ===

        public DateTime SelectedDate { get; private set; }

        private Theme theme;
        public Theme Theme
        {
            get => theme;
            set
            {

                if (value != theme)
                {
                    theme = value;

                    if (theme.TextAndShadow.IsDropShadow)
                        ApplyShadowToTexts(theme.TextAndShadow.ShadowEffect);
                    else
                        ApplyShadowToTexts(null);

                    TextOptions.SetTextFormattingMode(this, theme.TextAndShadow.TextFormattingMode);

                    Background = value.ThemeColor.Background;
                    Foreground = value.ThemeColor.Foreground;
                    BorderBrush = value.ThemeColor.Border;
                    if (days != null)
                    {
                        for (int i = 0; i < days.Length; i++)
                        {
                            days[i].Theme = value;
                        }
                    }

                    // todo: update using data binding
                    if (cwLabels != null)
                    {
                        for (int i = 0; i < cwLabels.Length; i++)
                        {
                            cwLabels[i].Foreground = value.ThemeColor.GrayedOutForeground;
                        }
                    }
                }
            }
        }

        public SizeMode SizeMode { get => (SizeMode)GetValue(SizeModeProperty); set => SetValue(SizeModeProperty, value); }

        public static readonly DependencyProperty SizeModeProperty = DependencyProperty.Register(
            "SizeMode", typeof(SizeMode), typeof(AugustineCalendarMonth),
            new PropertyMetadata(SizeMode.Normal, OnSizeModeChanged, null));

        private static void OnSizeModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((AugustineCalendarMonth)d).ApplySizeMode((SizeMode)e.NewValue);

        private void ApplySizeMode(SizeMode newValue)
        {
            SizeSet sizeSet = Theme.TextSize.GetSizeSet(newValue);
            monthLabelSolar.FontSize = sizeSet.MonthSolarLabelTextSize;
            monthLabelLunar.FontSize = sizeSet.MonthLunarLabelTextSize;

            switch (newValue)
            {
                case SizeMode.Small:
                    break;
                case SizeMode.Normal:
                    break;
                case SizeMode.Large:
                    break;
                default:
                    break;
            }

            if (days != null)
            {
                for (int i = 0; i < days.Length; i++)
                {
                    days[i].SizeMode = SizeMode;
                }
            }

            UpdateTodayInfoLabel();
            UpdateSelectedDateInfoLabel();
            UpdateDayOfWeekLabels();
        }

        internal void RefreshToday()
        {
            if (today != DateTime.Today)
            {
                today = DateTime.Today;
                SelectedDate = today;
                UpdateEverything();
            }
        }

        #endregion

        #region === Methods ===

        #region --- Event Handlers ---

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

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
            => SizeMode = Theme.TextSize.GetSizeMode(this.ActualWidth, this.ActualHeight);
        
        private void Day_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var day = ((DayTile)sender);
            day.IsSelected = true;
            SelectedDate = day.SolarDate;
            UpdateEverything();
        }

        private void TodayInfoLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectedDate = today;
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

        #region --- UI Component Initializers ---

        private void InitializeBinders()
        {
            foregroundBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, GetType(), 1),
                Path = new PropertyPath("Foreground"),
                Mode = BindingMode.OneWay,
            };

            borderBinding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, GetType(), 1),
                Path = new PropertyPath("BorderBrush"),
                Mode = BindingMode.OneWay,
            };
        }

        private void InitializeMonthLabel()
        {
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            monthLabelSolar = new Label
            {
                FontSize = 18f,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, -3),
                Padding = new Thickness(0, 0, 0, 0),
            };

            monthLabelLunar = new Label
            {
                FontSize = 16f,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, -3, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
            };

            stackPanel.Children.Add(monthLabelSolar);
            stackPanel.Children.Add(monthLabelLunar);

            stackPanel.MouseWheel += MonthLabel_MouseWheel;
            //BindingOperations.SetBinding(monthLabel, ForegroundProperty, backgroundBinding);
            BindingOperations.SetBinding(monthLabelSolar, ForegroundProperty, foregroundBinding);
            BindingOperations.SetBinding(monthLabelLunar, ForegroundProperty, foregroundBinding);

            Grid.SetColumn(stackPanel, COL_IDX_MONTH_LABEL);
            Grid.SetColumnSpan(stackPanel, MainGrid.ColumnDefinitions.Count - COL_IDX_MONTH_LABEL);
            Grid.SetRow(stackPanel, ROW_IDX_MONTH_LABEL);
            MainGrid.Children.Add(stackPanel);
            UpdateMonthLabels();
        }

        private void InitializeSolarTermBar()
        {
            solarTermBar = new UserControl
            {
                Content = SolarTermBar.CreateSolarTermBar(today.Year, 7, this.FontFamily)
            };
            Grid.SetRow(solarTermBar, ROW_IDX_SOLAR_TERM_DECORATOR);
            Grid.SetColumn(solarTermBar, COL_IDX_SOLAR_TERM_DECORATOR);
            Grid.SetColumnSpan(solarTermBar, MAX_COL_SPAN - COL_IDX_SOLAR_TERM_DECORATOR);
            MainGrid.Children.Add(solarTermBar);

        }

        private void InitializeTodayLabel()
        {
            todayInfoLabel = new Label
            {
                FontSize = 12f,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
                Padding = new Thickness(0, 3, 0, 0),
                Cursor = Cursors.Hand,
            };
            todayInfoLabel.MouseUp += TodayInfoLabel_MouseUp;
            //BindingOperations.SetBinding(todayInfoLabel, ForegroundProperty, backgroundBinding);
            BindingOperations.SetBinding(todayInfoLabel, ForegroundProperty, foregroundBinding);
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
                Margin = new Thickness(0),
                Padding = new Thickness(0, 0, 0, 0),
                Cursor = Cursors.Hand,
            };
            selectedDateInfoLabel.MouseUp += SelectedDateInfoLabel_MouseUp;
            //BindingOperations.SetBinding(selectedDateInfoLabel, BackgroundProperty, backgroundBinding);
            BindingOperations.SetBinding(selectedDateInfoLabel, ForegroundProperty, foregroundBinding);
            Grid.SetColumn(selectedDateInfoLabel, COL_IDX_SELECTED_DATE_LABEL);
            Grid.SetColumnSpan(selectedDateInfoLabel, MAX_COL_SPAN - COL_IDX_SELECTED_DATE_LABEL);
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
            Grid.SetColumnSpan(datePickerStackPanel, MAX_COL_SPAN - COL_IDX_MONTH_LABEL);
            Grid.SetRow(datePickerStackPanel, ROW_IDX_SELECTED_DATE_LABEL - 1);
            Grid.SetRowSpan(datePickerStackPanel, 2);

            datePickerStackPanel.Children.Add(datePicker);
            datePickerStackPanel.Children.Add(datePickerButton);
            MainGrid.Children.Add(datePickerStackPanel);
        }

        private void InitializeDayOfWeekLabels()
        {
            dayOfWeekLabels = new Label[DAYS_PER_WEEK];
            for (int i = 0; i < DAYS_PER_WEEK; i++)
            {
                int currentDayOfWeek = (int)firstDayOfWeek + i;
                if (currentDayOfWeek >= 7)
                    currentDayOfWeek -= 7;
                Label label = new Label
                {
                    //Content = DayOfWeekShortLabels[currentDayOfWeek],
                    //BorderThickness = new Thickness(0, 1, 1, 1),
                    FontSize = 14f,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Padding = new Thickness(0),
                    Margin = new Thickness(0),
                };
                dayOfWeekLabels[i] = label;
                BindingOperations.SetBinding(label, ForegroundProperty, foregroundBinding);

                //BindingOperations.SetBinding(label, BorderBrushProperty, borderBinding);
                Border b = new Border()
                {
                    BorderThickness = new Thickness(0, 1, 1, 1),
                };

                b.Child = label;

                BindingOperations.SetBinding(b, BorderBrushProperty, borderBinding);

                Grid.SetColumn(b, COL_IDX_DOW_LABELS + i);
                Grid.SetRow(b, ROW_IDX_DOW_LABELS);
                MainGrid.Children.Add(b);
            }
            UpdateDayOfWeekLabels();
        }

        private void InitializeCwLabels()
        {
            cwLabels = new Label[WEEKS];
            for (int i = 0; i < WEEKS; i++)
            {
                Label label = new Label
                {
                    BorderThickness = new Thickness(0, 0, 1, 0),
                    Margin = new Thickness(0),
                    Padding = new Thickness(1),
                    FontStyle = FontStyles.Italic,
                };
                cwLabels[i] = label;

                Border b = new Border()
                {
                    BorderThickness = new Thickness(0, 0, 1, 0),
                };

                b.Child = label;

                BindingOperations.SetBinding(b, BorderBrushProperty, borderBinding);

                Grid.SetColumn(b, COL_IDX_CW_LABELS);
                Grid.SetRow(b, ROW_IDX_CW_LABELS + i);
                MainGrid.Children.Add(b);
            }

            // dummy cell to draw a left border of the first day of week label
            Label dummy = new Label
            {
                BorderThickness = new Thickness(0, 0, 1, 0),
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                Content = null,
            };

            BindingOperations.SetBinding(dummy, BorderBrushProperty, borderBinding);

            Grid.SetColumn(dummy, COL_IDX_CW_LABELS);
            Grid.SetRow(dummy, ROW_IDX_CW_LABELS - 1);
            MainGrid.Children.Add(dummy);

            UpdateCwLabels();
        }

        private void InitializeDays()
        {
            UpdateReferenceDates();
            days = new DayTile[DAYS_PER_WEEK * WEEKS];

            for (int i = 0; i < DAYS_PER_WEEK; i++)
            {
                for (int j = 0; j < WEEKS; j++)
                {
                    DayTile day = new DayTile
                    {
                        //ActiveSizeMode = TextSize.SizeMode.Normal,
                        SolarDate = pageBegin.AddDays(i + j * 7),
                        Margin = new Thickness(cellSpacing),
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

                    //day.MouseEnter += Day_MouseEnter;
                    //day.MouseLeave += Day_MouseLeave;
                    day.MouseUp += Day_MouseUp;
                    day.MouseWheel += Day_MouseWheel;

                    Grid.SetColumn(day, COL_IDX_DAYS + i);
                    Grid.SetRow(day, ROW_IDX_DAYS + j);
                    MainGrid.Children.Add(day);
                }
            }

            StackPanel sp = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            sp.Children.Add(new Separator());
            Grid.SetColumn(sp, COL_IDX_DAYS);
            Grid.SetRow(sp, ROW_IDX_DAYS);
            Grid.SetColumnSpan(sp, WEEKS);
            MainGrid.Children.Add(sp);
        }

        #endregion

        #region --- UI Updaters ---

        private void UpdateReferenceDates()
        {
            if (SelectedDate < MINIMUM_SUPPORTED_DATE)
            {
                SelectedDate = MINIMUM_SUPPORTED_DATE;
            }

            var thisMonth = SelectedDate.Month;
            var thisYear = SelectedDate.Year;
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
                lunarInfo = String.Format("Tháng {0}{1} năm {2} - Tháng {3}{4} năm {5}",
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
                lunarInfo = String.Format("Tháng {0}{1} năm {2}",
                    monthEndLunarDate.MonthName,
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.YearName);
            }
            else
            {
                lunarInfo = String.Format("Tháng {0}{1}/{2}{3} năm {4}",
                    monthBeginLunarDate.MonthName,
                    monthBeginLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.MonthName,
                    monthEndLunarDate.IsLeapMonth ? " nhuận" : "",
                    monthEndLunarDate.YearName);
            }

            String solarInfo = String.Format("Tháng {0} năm {1}", SelectedDate.Month, SelectedDate.Year);

            monthLabelSolar.Content = solarInfo;
            monthLabelLunar.Content = lunarInfo;
        }

        private void UpdateSolarTermBar()
        {
            solarTermBar.Content = SolarTermBar.CreateSolarTermBar(SelectedDate.Year, 7, FontFamily);
        }

        private void UpdateDayOfWeekLabels()
        {
            if (dayOfWeekLabels == null)
                return;
            for (int i = 0; i < dayOfWeekLabels.Length; i++)
            {
                switch (SizeMode)
                {
                    case SizeMode.Small:
                        dayOfWeekLabels[i].Content = DayOfWeekShortLabels[i];
                        break;
                    case SizeMode.Normal:
                        dayOfWeekLabels[i].Content = DayOfWeekMediumLabels[i];
                        break;
                    case SizeMode.Large:
                        dayOfWeekLabels[i].Content = DayOfWeekFullLabels[i];
                        break;
                    default:
                        dayOfWeekLabels[i].Content = DayOfWeekMediumLabels[i];
                        break;
                }
            }
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
                    }
                    else if (day.SolarDate == monthEnd)
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

        private void UpdateCwLabels()
        {
            var week0 = (pageBegin.AddDays(-(int)firstDayOfWeek)).DayOfYear / 7 + 1;
            for (int i = 0; i < cwLabels.Length; i++)
            {
                if ((week0 + i) >= 53)
                    cwLabels[i].Content = i + week0 - 52;
                else
                    cwLabels[i].Content = i + week0;
            }

            
        }


        private void UpdateEverything()
        {
            UpdateReferenceDates();
            UpdateCwLabels();
            UpdateDays();
            UpdateMonthLabels();
            UpdateSolarTermBar();
            UpdateTodayInfoLabel();
            UpdateSelectedDateInfoLabel();
        }

        #endregion

        #region --- Miscelanous ---

        private void StyleThisDay(DayTile day)
        {
            day.IsSelected = false;
            day.IsToday = false;

            if (day.SolarDate.Month != SelectedDate.Month)
            {
                day.DayType = DayType.GrayedOut;
            }

            if (day.SolarDate == today)
            {
                day.IsToday = true;
            }

            if (day.SolarDate == SelectedDate)
            {
                day.IsSelected = true;
            }
        }

        internal String GetFullDayInfo(DateTime date)
        {
            switch (SizeMode)
            {
                case SizeMode.Small:
                    return string.Format("{0} {1:dd/MM/yyyy}",
                        DayOfWeekFullLabels[(int)date.DayOfWeek], date);
                case SizeMode.Normal:
                case SizeMode.Large:
                default:
                    return string.Format("{0} {1:dd/MM/yyyy} - {2}",
                        DayOfWeekFullLabels[(int)date.DayOfWeek], date,
                        LuniSolarCalendar<VietnameseLocalInfoProvider>.LuniSolarDateFromSolarDate(date.Year, date.Month, date.Day));
            }
        }

        public void SelectDate(DateTime date)
        {
            SelectedDate = date;
            UpdateEverything();
        }

        #endregion

        #endregion

        internal void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Home:
                    SelectedDate = SelectedDate.AddDays(-SelectedDate.Day + 1);
                    break;
                case Key.End:
                    SelectedDate = SelectedDate.AddDays(-SelectedDate.Day + 1).AddMonths(1).AddDays(-1);
                    break;
                case Key.PageUp:
                    SelectedDate = SelectedDate.AddMonths(-1);
                    break;
                case Key.PageDown:
                    SelectedDate = SelectedDate.AddMonths(1);
                    break;
                case Key.Left:
                    SelectedDate = SelectedDate.AddDays(-1);
                    break;
                case Key.Up:
                    SelectedDate = SelectedDate.AddDays(-7);
                    break;
                case Key.Right:
                    SelectedDate = SelectedDate.AddDays(1);
                    break;
                case Key.Down:
                    SelectedDate = SelectedDate.AddDays(7);
                    break;
                default:
                    break;
            }
            UpdateEverything();
        }

        public void ApplyShadowToTexts(DropShadowEffect e)
        {
            monthLabelSolar.Effect = e;
            monthLabelLunar.Effect = e;
            foreach (var item in dayOfWeekLabels)
            {
                item.Effect = e;
            }
            foreach (var item in days)
            {
                item.ApplyShadowToTexts(e);
            }            //Effect = e;
        }
    }
}
