/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

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

            AugustineCalendarMonth.Theme = configuration.Theme;

            var margin = SystemParameters.WindowResizeBorderThickness;
            var border = SystemParameters.BorderWidth + 3;
            AugustineCalendarMonth.Margin = new Thickness(margin.Left + border, margin.Top + border, 
                margin.Right + border, margin.Bottom + border);
        }


        private void Window_StateChanged(object sender, EventArgs e)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsRequestedClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed
            && e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }


        private void Border_MouseEnter(object sender, MouseEventArgs e) => PopupToolBar.IsOpen = true;

        private void Popup_MouseLeave(object sender, MouseEventArgs e) => PopupToolBar.IsOpen = false; 


        private void ButtonClose_MouseDown(object sender, RoutedEventArgs e)
        {
            PopupToolBar.IsOpen = false;
            this.Close();
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

        private void ButtonMove_MouseUp(object sender, MouseButtonEventArgs e) => PopupToolBar.IsOpen = false;

        private void ButtonMove_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void ButtonConverterTool_Click(object sender, RoutedEventArgs e)
        {
            Converter converter = new Converter(AugustineCalendarMonth.SelectedDate);
            converter.ShowDialog();
            if (converter.DialogResult.HasValue && converter.DialogResult.Value)
            {
                AugustineCalendarMonth.SelectDate(converter.SelectedDate);
            }
        }

        private void ButtonWidget_Click(object sender, RoutedEventArgs e) => OnRequestChangeViewMode();

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            ThemeEditor te = new ThemeEditor();
            te.SetTarget(AugustineCalendarMonth);
            te.Show();
        }

        private void ButtonAbout_Click(object sender, RoutedEventArgs e) => (new About()).Show();


        public void RequestClose()
        {
            IsRequestedClosing = true;
            Close();
        }

        private void window_KeyUp(object sender, KeyEventArgs e)
        {
            AugustineCalendarMonth.UserControl_KeyDown(sender, e);
        }
    }
}
