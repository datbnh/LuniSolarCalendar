/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System.Windows.Controls;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for CalendarDayToolTip.xaml
    /// </summary>
    public partial class CalendarDayToolTipView : UserControl
    {
        public CalendarDayToolTipView(CalendarDayToolTipModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
