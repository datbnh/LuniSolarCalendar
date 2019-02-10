/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core.LuniSolarCalendar;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for Converter.xaml
    /// </summary>
    public partial class Converter : Window
    {
        // combo box update logic:
        // ----------------------
        // * lunar year changes
        //   -> (list of lunar months) and (list of lunar days) always change
        // * lunar month changes
        //   -> list of lunar days always changes
        //
        // * solar year changes
        //   -> list of solar days changes when in February (leap year)
        // *  solar month changes
        //   -> list of solar days always changes (February leap year)
        // *  list of solar months never changes

        private int lastTrackedIndex;
        private LuniSolarDate<VietnameseLocalInfoProvider> selectedLunarDate;
        private bool isUpdatingComboBoxes;

        public DateTime SelectedDate { get => selectedLunarDate.SolarDate; set { selectedLunarDate = LuniSolarCalendar<VietnameseLocalInfoProvider>.LuniSolarDateFromSolarDate(value); SelectDate(selectedLunarDate.SolarDate); } }

        private void SelectDate(DateTime date)
        {
            isUpdatingComboBoxes = true;
            SyncSelectedDateToSolarComboBoxes();
            SyncSolarComboBoxesToLunar();
            isUpdatingComboBoxes = false;
        }

        /// <summary>
        /// Today will be selected by default.
        /// </summary>
        public Converter()
        {
            Init(DateTime.Today);
        }

        /// <summary>
        /// Selects the targetDate after constructing the object.
        /// </summary>
        /// <param name="targetDate"></param>
        public Converter(DateTime targetDate)
        {
            Init(targetDate);
        }

        internal void Init(DateTime targetDate)
        {
            InitializeComponent();

            selectedLunarDate = LuniSolarCalendar<VietnameseLocalInfoProvider>.LuniSolarDateFromSolarDate(targetDate);

            isUpdatingComboBoxes = true;

            PopulateComboBoxYearItems();
            comboBoxSolarYear.SelectedItem = selectedLunarDate.SolarDate.Year;
            SyncSelectedLunarYear();

            PopulateComboBoxSolarMonthItems();
            comboBoxSolarMonth.SelectedItem = selectedLunarDate.SolarDate.Month;

            PopulateComboBoxSolarDayItems();
            comboBoxSolarDay.SelectedItem = selectedLunarDate.SolarDate.Day;

            PopulateComboBoxLunarMonthItems();
            SyncSelectedLunarMonth();

            PopulateComboBoxLunarDayItems();
            comboBoxLunarDay.SelectedItem = selectedLunarDate.Day;

            PopulateDetails();

            isUpdatingComboBoxes = false;
        }

        // never changes
        private void PopulateComboBoxYearItems()
        {
            for (int i = AugustineCalendarMonth.MINIMUM_SUPPORTED_DATE.Year; i < 3000; i++)
            {
                var item = new ComboLunarYearItem(i);
                comboBoxSolarYear.Items.Add(i);
                comboBoxLunarYear.Items.Add(item);
            }
        }

        // never changes
        private void PopulateComboBoxSolarMonthItems()
        {
            for (int i = 1; i <= 12; i++)
            {
                comboBoxSolarMonth.Items.Add(i);
            }
        }

        private void PopulateComboBoxSolarDayItems()
        {
            if (comboBoxSolarYear.SelectedIndex == -1)
                return;
            if (comboBoxSolarMonth.SelectedIndex == -1)
                return;

            var currentSelected = comboBoxSolarDay.SelectedIndex;
            comboBoxSolarDay.Items.Clear();
            for (int i = 0; i <
                DateTime.DaysInMonth((int)comboBoxSolarYear.SelectedValue, (int)comboBoxSolarMonth.SelectedValue); i++)
                comboBoxSolarDay.Items.Add(i + 1);

            if (currentSelected == -1)
                currentSelected = 0;
            if (comboBoxSolarDay.Items.Count > currentSelected)
                comboBoxSolarDay.SelectedIndex = currentSelected;
            else
                comboBoxSolarDay.SelectedIndex = comboBoxSolarDay.Items.Count - 1;
        }

        private void PopulateComboBoxLunarMonthItems()
        {
            var thisYear = LuniSolarCalendar<VietnameseLocalInfoProvider>.GetLunarYear(((ComboLunarYearItem)comboBoxLunarYear.SelectedItem).Year);
            var nextYear = LuniSolarCalendar<VietnameseLocalInfoProvider>.GetLunarYear(((ComboLunarYearItem)comboBoxLunarYear.SelectedItem).Year + 1);

            //var thisYear = LunarYear.GetLunarYear(((ComboLunarYearItem)comboBoxLunarYear.SelectedItem).Year, 7);
            //var nextYear = LunarYear.GetLunarYear(((ComboLunarYearItem)comboBoxLunarYear.SelectedItem).Year + 1, 7);

            var currentSelected = comboBoxLunarMonth.SelectedIndex;
            comboBoxLunarMonth.Items.Clear();
            for (int i = 0; i < thisYear.Months.Length; i++)
            {
                var month = thisYear.Months[i].Item1;
                var nextMonthBeginDate = i == thisYear.Months.Length - 1 ? nextYear.Months[0].Item2 : thisYear.Months[i + 1].Item2;
                var monthLength = (nextMonthBeginDate - thisYear.Months[i].Item2).TotalDays;
                if (month == 11 || month == 12)
                    continue; // ignore #11 & #12 of thisYear
                var monthItem = new ComboLunarMonthItem(month, thisYear.Months[i].Item3, (int)monthLength);
                comboBoxLunarMonth.Items.Add(monthItem);
            }
            for (int i = 0; i < nextYear.Months.Length; i++)
            {
                var month = nextYear.Months[i].Item1;
                if (month == 1)
                    break; // only take #11 and #12 of nextYear
                var monthLength = (nextYear.Months[i + 1].Item2 - nextYear.Months[i].Item2).TotalDays;
                var monthItem = new ComboLunarMonthItem(month, nextYear.Months[i].Item3, (int)monthLength);
                comboBoxLunarMonth.Items.Add(monthItem);
            }

            if (currentSelected == -1)
                currentSelected = 0;
            if (comboBoxLunarMonth.Items.Count > currentSelected)
                comboBoxLunarMonth.SelectedIndex = currentSelected;
            else
                comboBoxLunarMonth.SelectedIndex = comboBoxLunarMonth.Items.Count - 1;
        }

        private void PopulateComboBoxLunarDayItems()
        {
            var currentSelected = comboBoxLunarDay.SelectedIndex;
            comboBoxLunarDay.Items.Clear();
            for (int i = 0; i < ((ComboLunarMonthItem)comboBoxLunarMonth.SelectedItem).MonthLength; i++)
                comboBoxLunarDay.Items.Add(i + 1);
            if (currentSelected == -1)
                currentSelected = 0;
            if (comboBoxLunarDay.Items.Count > currentSelected)
                comboBoxLunarDay.SelectedIndex = currentSelected;
            else
                comboBoxLunarDay.SelectedIndex = comboBoxLunarDay.Items.Count - 1;
        }

        private void PopulateDetails()
        {
            solarPlaceHolder.Text = String.Format("Dương lịch: {0} {1:dd/MM/yyyy}",
                AugustineCalendarMonth.DayOfWeekFullLabels[(int)selectedLunarDate.SolarDate.DayOfWeek], selectedLunarDate.SolarDate);
            lunarPlaceHolder.Text = "Âm lịch: " + selectedLunarDate.FullDayInfo;
        }

        #region Solar Date Selectors

        private void ComboBoxSolarYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingComboBoxes)
                return;
            if (comboBoxSolarYear.SelectedIndex == -1)
                return;

            lastTrackedIndex = comboBoxSolarYear.SelectedIndex;

            isUpdatingComboBoxes = true;
            //Console.WriteLine("ComboBoxSolarYear_SelectionChanged " + comboBoxSolarYear.SelectedIndex);
            if (comboBoxSolarMonth.SelectedIndex == 1) // February is being selected
                PopulateComboBoxSolarDayItems();
            SyncSolarComboBoxesToLunar();
            isUpdatingComboBoxes = false;
        }

        private void ComboBoxSolarMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingComboBoxes)
                return;
            if (comboBoxSolarMonth.SelectedIndex == -1)
                return;

            isUpdatingComboBoxes = true;
            //Console.WriteLine("ComboBoxSolarMonth_SelectionChanged " + comboBoxSolarMonth.SelectedIndex);
            PopulateComboBoxSolarDayItems();
            SyncSolarComboBoxesToLunar();
            isUpdatingComboBoxes = false;
        }

        private void ComboBoxSolarDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingComboBoxes)
                return;
            if (comboBoxSolarDay.SelectedIndex == -1)
                return;

            isUpdatingComboBoxes = true;
            //Console.WriteLine("ComboBoxSolarDay_SelectionChanged " + comboBoxSolarDay.SelectedIndex);
            SyncSolarComboBoxesToLunar();
            isUpdatingComboBoxes = false;
        }

        #endregion

        #region Lunar Date Selectors

        private void ComboBoxLunarYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingComboBoxes)
                return;
            if (comboBoxLunarYear.SelectedIndex == -1)
                return;
            lastTrackedIndex = comboBoxLunarYear.SelectedIndex;

            isUpdatingComboBoxes = true;

            PopulateComboBoxLunarMonthItems();
            PopulateComboBoxLunarDayItems();
            SyncLunarComboBoxesToSolar();
            isUpdatingComboBoxes = false;
        }

        private void ComboBoxLunarMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingComboBoxes)
                return;
            if (comboBoxLunarMonth.SelectedIndex == -1)
                return;

            isUpdatingComboBoxes = true;
            PopulateComboBoxLunarDayItems();
            SyncLunarComboBoxesToSolar();
            isUpdatingComboBoxes = false;
        }

        private void ComboBoxLunarDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingComboBoxes)
                return;
            if (comboBoxLunarDay.SelectedIndex == -1)
                return;

            isUpdatingComboBoxes = true;
            SyncLunarComboBoxesToSolar();
            isUpdatingComboBoxes = false;
        }

        internal class ComboLunarYearItem : IEquatable<ComboLunarYearItem>
        {
            public ComboLunarYearItem(int year) { Year = year; }
            public int Year { get; set; }

            public bool Equals(ComboLunarYearItem other)
            {
                return Year == other.Year;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as ComboLunarYearItem);
            }

            public override string ToString() { return Year + " (" + LuniSolarYear<VietnameseLocalInfoProvider>.GetYearName(Year) + ")"; }
        }

        internal class ComboLunarMonthItem
        {
            public ComboLunarMonthItem(int month, bool isLeapMonth, int monthLength) { Month = month; IsLeapMonth = isLeapMonth; MonthLength = monthLength; }
            public int Month { get; set; }
            public int MonthLength { get; set; }
            public bool IsLeapMonth { get; set; }

            public override string ToString() { return LuniSolarDate<VietnameseLocalInfoProvider>.GetMonthShortName(Month, IsLeapMonth); }
        }

        #endregion

        #region Sychronizers
        private void SyncSelectedDateToSolarComboBoxes()
        {
            comboBoxSolarYear.SelectedItem = selectedLunarDate.SolarDate.Year;
            comboBoxSolarMonth.SelectedItem = selectedLunarDate.SolarDate.Month;
            PopulateComboBoxSolarDayItems();
            comboBoxSolarDay.SelectedItem = selectedLunarDate.SolarDate.Day;
            PopulateDetails();
        }

        private void SyncLunarComboBoxesToSolar()
        {
            selectedLunarDate = LuniSolarCalendar<VietnameseLocalInfoProvider>.LuniSolarDateFromLunarInfo(
                ((ComboLunarYearItem)comboBoxLunarYear.SelectedItem).Year,
                ((ComboLunarMonthItem)comboBoxLunarMonth.SelectedItem).Month,
                ((ComboLunarMonthItem)comboBoxLunarMonth.SelectedItem).IsLeapMonth,
                ((int)comboBoxLunarDay.SelectedItem));

            SyncSelectedDateToSolarComboBoxes();
        }

        private void SyncSolarComboBoxesToLunar()
        {
            var selectedDate = new DateTime(
                (int)comboBoxSolarYear.SelectedValue,
                (int)comboBoxSolarMonth.SelectedValue,
                (int)comboBoxSolarDay.SelectedValue);
            selectedLunarDate = LuniSolarCalendar<VietnameseLocalInfoProvider>.LuniSolarDateFromSolarDate(selectedDate);

            SyncSelectedLunarYear();
            PopulateComboBoxLunarMonthItems();
            SyncSelectedLunarMonth();
            PopulateComboBoxLunarDayItems();
            comboBoxLunarDay.SelectedItem = selectedLunarDate.Day;
            PopulateDetails();
        }

        private void SyncSelectedLunarMonth()
        {
            for (int i = 0; i < comboBoxLunarMonth.Items.Count; i++)
            {
                if (comboBoxLunarMonth.Items[i].ToString() == selectedLunarDate.MonthShortName)
                {
                    comboBoxLunarMonth.SelectedIndex = i;
                    break;
                }
            }
        }

        private void SyncSelectedLunarYear()
        {
            for (int i = 0; i < comboBoxLunarYear.Items.Count; i++)
            {
                if (((ComboLunarYearItem)comboBoxLunarYear.Items[i]).Year == selectedLunarDate.Year)
                {
                    comboBoxLunarYear.SelectedIndex = i;
                    break;
                }
            }
        }
        #endregion

        #region Editable ComboBox Validator

        private void ComboBoxLunarYear_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateComboSelection((ComboBox)sender);
        }

        private void ComboBoxLunarYear_DropDownClosed(object sender, EventArgs e)
        {
            ValidateComboSelection((ComboBox)sender);
        }

        private void ValidateComboSelection(ComboBox comboBox)
        {
            //Console.WriteLine("{0} -> {1}", comboBox.SelectedIndex, lastTrackedIndex);
            if (comboBox.SelectedIndex == -1)
            {
                comboBox.SelectedIndex = lastTrackedIndex;
                // Updating the SelectedIndex/SelectedItem does not mean
                // the Text will be updated accordingly.
                // e.g. User puts "19" as input, the first matched item
                // "1900 (Canh Tý)" is now selected. However, at this moment,
                // it the focus is lost, the text remains "19". 
                // Thus, we need to update the text.
                comboBox.Text = comboBox.SelectedValue.ToString();
            }
        }

        #endregion

        public void Show(DateTime date)
        {
            selectedLunarDate = LuniSolarCalendar<VietnameseLocalInfoProvider>.LuniSolarDateFromSolarDate(date);
            Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
