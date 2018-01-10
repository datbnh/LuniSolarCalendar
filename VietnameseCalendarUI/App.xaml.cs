/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string confFile = "Configuration.bin";

        private static readonly Bitmap icon = UI.Properties.Resources.Calendar_16x;

        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        private Configuration configuration;
        CalendarMonthWindow currentWindow;


        public App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolver);
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Click += new EventHandler((s, e) =>
            {
                if (e is System.Windows.Forms.MouseEventArgs)
                    if (((System.Windows.Forms.MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.Right)
                        return;
                if (currentWindow != null && currentWindow.IsLoaded)
                {
                    if (!currentWindow.IsVisible)
                    {
                        currentWindow.WindowState = WindowState.Normal;
                        currentWindow.Show();
                        currentWindow.Activate();
                    }
                    else
                    {
                        currentWindow.Hide();
                    }
                }
            });
            //notifyIcon.DoubleClick += new EventHandler(NotifyIcon_DoubleClick);
            notifyIcon.Icon = Icon.FromHandle(icon.GetHicon()); ;
            notifyIcon.Visible = true;
            notifyIcon.Text = "Lịch Việt Nam - Vietnamese Luni Solar Calendar";

            System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            contextMenu.MenuItems.Add("Thông tin chương trình", (s, e) => { (new About()).Show(); });
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add("Thoát", (s, e) => { Shutdown(); });
            notifyIcon.ContextMenu = contextMenu;
            //notifyIcon.ShowBalloonTip(0, "Lịch Việt Nam", "Chào bạn! Lịch Việt Nam đã khởi động.", System.Windows.Forms.ToolTipIcon.Info);
        }

        public Assembly AssemblyResolver(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return Assembly.Load(bytes);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!Serializer.TryLoadFromBinaryFile<Configuration>(confFile, out configuration))
                configuration = Configuration.DefaultConfiguration;

            CreateCalendarMonthWindow();
            InitializeNotifyIcon();
        }

        private void CreateCalendarMonthWindow()
        {
            currentWindow = new CalendarMonthWindow(configuration);
            currentWindow.OnRequestChangeViewMode += new CalendarMonthWindow.RequestChangeViewMode(ChangeViewMode);
            currentWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Visible = false;
            SyncCurrentStateToConfiguration();
            Serializer.TrySaveToBinaryFile(configuration, confFile);
        }

        private void ChangeViewMode()
        {
            SyncCurrentStateToConfiguration();
            configuration.ViewMode = (configuration.ViewMode == ViewMode.Normal) ? ViewMode.Widget : ViewMode.Normal;
            currentWindow.RequestClose();
            foreach (var item in Application.Current.Windows)
            {
                if (item is ThemeEditor)
                {
                    ((ThemeEditor)item).Close();
                    break;
                }
            }
            CreateCalendarMonthWindow();
        }

        private void SyncCurrentStateToConfiguration()
        {
            configuration.Theme = currentWindow.AugustineCalendarMonth.Theme;
            configuration.ViewMode = (currentWindow.WindowStyle == WindowStyle.None) ? ViewMode.Widget : ViewMode.Normal;
            configuration.Location = new Location()
            {
                Left = currentWindow.Left,
                Top = currentWindow.Top,
                Width = currentWindow.Width,
                Height = currentWindow.Height,
            };
        }
    }
}
