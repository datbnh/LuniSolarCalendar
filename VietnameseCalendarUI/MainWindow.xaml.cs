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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        public MainWindow()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            //InitializeConfiguration();
            //InitializeView();

            Configuration.DefaultConfiguration.SaveToFile("test.txt");
            //Configuration.DefaultConfiguration.SaveToBinaryFile("test.bin.txt");
            Serializer.LoadFromFile<Configuration>("test.txt", out Configuration c1);
            //Serializer.LoadFromBinaryFile<Configuration>("test.bin.txt", out Configuration c2);
            //System.Console.WriteLine(c1 == c2);
            //// LoadConfig
            //// ApplyConfig
        }

        private void InitializeView()
        {
            throw new NotImplementedException();
        }

        private void InitializeConfiguration()
        {
            throw new NotImplementedException();
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Click += new EventHandler(NotifyIcon_Click);
            notifyIcon.DoubleClick += new EventHandler(NotifyIcon_DoubleClick);
            //notifyIcon.ContextMenu = 
            notifyIcon.Icon = System.Drawing.SystemIcons.Application;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(0,"Hello","World", System.Windows.Forms.ToolTipIcon.Info);

        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("DC");
            notifyIcon.ShowBalloonTip(0, "You", "double clicked on me!", System.Windows.Forms.ToolTipIcon.Info);
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(0, "You", "clicked on me!", System.Windows.Forms.ToolTipIcon.Info);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            (new About()).Show();
        }

        private void ConverterToolButton_Click(object sender, RoutedEventArgs e)
        {
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //augustineCalendarMonth.Theme = Themes.DarkSemiTransparent;
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                Show();
            }

            if (WindowState == WindowState.Maximized)
            {
                ToolsPopup.VerticalOffset = 10;
                ToolsPopup.HorizontalOffset = -((FrameworkElement)ToolsPopup.Child).ActualWidth - 10;
            } else
            {
                ToolsPopup.VerticalOffset = 0;
                ToolsPopup.HorizontalOffset = 5;
            }
                
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            ThemeEditor te = new ThemeEditor();
            te.SetTarget(augustineCalendarMonth);
            te.Show();
        }

        private void ButtonWidget_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (WindowStyle != WindowStyle.None)
            {

                mainWindow.AllowsTransparency = true;
                mainWindow.WindowStyle = WindowStyle.None;
                
            } else
            {
                mainWindow.AllowsTransparency = false;
                mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            mainWindow.augustineCalendarMonth.Theme = this.augustineCalendarMonth.Theme;
            mainWindow.Show();
            foreach (var item in Application.Current.Windows)
            {
                if (item is ThemeEditor)
                {
                    ((ThemeEditor)item).Close();
                    break;
                }
            }
            this.Close();
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ToolsPopup.IsOpen = true;
        }

        private void Popup_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ToolsPopup.IsOpen = false;
        }

        private void ButtonClose_MouseDown(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMove_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ToolsPopup.IsOpen = false;
        }

        private void ButtonMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            var offset = ToolsPopup.HorizontalOffset;
            ToolsPopup.HorizontalOffset = offset + 1;
            ToolsPopup.HorizontalOffset = offset;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var offset = ToolsPopup.HorizontalOffset;
            ToolsPopup.HorizontalOffset = offset + 1;
            ToolsPopup.HorizontalOffset = offset;
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
    }
    public class WindowStateToMovability : IValueConverter
    {
        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((WindowState)value == WindowState.Maximized)
                return false;
            return true;
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WindowStyleToCloseAndMoveVisibility : IValueConverter
    {
        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((WindowStyle)value == WindowStyle.None)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
