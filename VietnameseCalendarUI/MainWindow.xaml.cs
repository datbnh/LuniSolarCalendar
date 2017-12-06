/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System.Windows;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //day1.SolarDate = DateTime.Today;
            //day1.IsLunarMonthVisible = true;
            //day1.Label = "Hello";
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            (new About()).Show();
        }

        private void buttonConverterTool_Click(object sender, RoutedEventArgs e)
        {
            // I do not make this as a field of the program to reduce the allocated memory on RAM,
            // as user does not use the converter most of the time.
            Converter converter = new Converter(augustineCalendarMonth.SelectedDate);
            converter.ShowDialog();
            if (converter.DialogResult.HasValue && converter.DialogResult.Value)
            {
                augustineCalendarMonth.SelectDate(converter.SelectedDate);
            }
        }
    }
}
