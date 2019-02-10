/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            var core = Assembly.GetAssembly(typeof(Core.Astronomy.JulianDateConverter));
            var ui = Assembly.GetAssembly(typeof(AugustineCalendarMonth));

            Version coreVersion = core.GetName().Version;
            DateTime coreBuildDate = new DateTime(2000, 1, 1)
                        .AddDays(coreVersion.Build).AddSeconds(coreVersion.Revision * 2);

            Version uiVersion = ui.GetName().Version;
            DateTime uiBuildDate = new DateTime(2000, 1, 1)
                        .AddDays(uiVersion.Build).AddSeconds(uiVersion.Revision * 2);

            Title = String.Format("About {0}", AssemblyProduct);
            textBlock1.Text = core.GetName().Name;
            textBlock2.Text = String.Format("Version {0} (Built {1})", coreVersion, coreBuildDate);
            textBlock3.Text = ui.GetName().Name;
            textBlock4.Text = String.Format("Version {0} (Built {1})", uiVersion, uiBuildDate);
            label5.Text = AssemblyCompany + Environment.NewLine + AssemblyCopyright;
            label5.TextAlignment = TextAlignment.Right;
            //this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("Version {0} (Built {1})", AssemblyVersion, buildDate);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
            //this.Icon = ScreenDimmer.IconMediumBright32x32;
            //this.logoPictureBox.Image = TextIcon.CreateTextIcon("\uE286", Color.Black, "", 32).ToBitmap();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
