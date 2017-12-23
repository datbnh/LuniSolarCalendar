/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Augustine.VietnameseCalendar.UI
{
    public partial class CalendarMonthWindow : Window
    {
        public delegate void RequestChangeViewMode();
        public RequestChangeViewMode OnRequestChangeViewMode;

        private bool IsRequestedClosing = false;

        public CalendarMonthWindow(Configuration configuration)
        {
            InitializeComponent();

            if (configuration.Location != null)
            {
                WindowStartupLocation = WindowStartupLocation.Manual;
                Width = configuration.Location.Width;
                Height = configuration.Location.Height;
                Top = configuration.Location.Top;
                Left = configuration.Location.Left;
            }

            AugustineCalendarMonth.Theme = configuration.Theme;

            if (configuration.ViewMode == ViewMode.Widget)
            {
                AllowsTransparency = true;
                WindowStyle = WindowStyle.None;
                ShowInTaskbar = false;
            }
            else
            {
                AllowsTransparency = false;
                WindowStyle = WindowStyle.SingleBorderWindow;
                ShowInTaskbar = true;
            }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            (new About()).Show();
        }

        private void ConverterToolButton_Click(object sender, RoutedEventArgs e)
        {
            Converter converter = new Converter(AugustineCalendarMonth.SelectedDate);
            converter.ShowDialog();
            if (converter.DialogResult.HasValue && converter.DialogResult.Value)
            {
                AugustineCalendarMonth.SelectDate(converter.SelectedDate);
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed
            && e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //augustineCalendarMonth.Theme = Themes.DarkSemiTransparent;
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            if (WindowStyle == WindowStyle.None && WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                Show();
            }

            if (WindowState == WindowState.Maximized)
            {
                PopupToolBar.VerticalOffset = 10;
                PopupToolBar.HorizontalOffset = -((FrameworkElement)PopupToolBar.Child).ActualWidth - 10;
            } else
            {
                PopupToolBar.VerticalOffset = 0;
                PopupToolBar.HorizontalOffset = 5;
            }
                
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            ThemeEditor te = new ThemeEditor();
            te.SetTarget(AugustineCalendarMonth);
            te.Show();
        }

        private void ButtonWidget_Click(object sender, RoutedEventArgs e)
        {
            OnRequestChangeViewMode();
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PopupToolBar.IsOpen = true;
        }

        private void Popup_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PopupToolBar.IsOpen = false;
        }

        private void ButtonClose_MouseDown(object sender, RoutedEventArgs e)
        {
            PopupToolBar.IsOpen = false;
            this.Close();
        }

        private void ButtonMove_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PopupToolBar.IsOpen = false;
        }

        private void ButtonMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            var offset = PopupToolBar.HorizontalOffset;
            PopupToolBar.HorizontalOffset = offset + 1;
            PopupToolBar.HorizontalOffset = offset;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var offset = PopupToolBar.HorizontalOffset;
            PopupToolBar.HorizontalOffset = offset + 1;
            PopupToolBar.HorizontalOffset = offset;
        }

        private void ButtonMaximize_MouseDown(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsRequestedClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        public void RequestClose()
        {
            IsRequestedClosing = true;
            Close();
        }
    }
}
