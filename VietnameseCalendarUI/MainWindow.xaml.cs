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
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            (new About()).Show();
        }

        private void ConverterToolButton_Click(object sender, RoutedEventArgs e)
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

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed
            && e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }
}
