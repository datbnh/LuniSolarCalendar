using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for ThemeEditor.xaml
    /// </summary>
    public partial class ThemeEditor : Window
    {
        private AugustineCalendarMonth target;


        public ThemeEditor()
        {
            InitializeComponent();
            DataContext = this;
            ThemeColor1 = ThemeColors.DarkSemiTransparent;


        }

        public void SetTarget(AugustineCalendarMonth calendar)
        {
            target = calendar;
            ThemeColor1 = target.Theme.ThemeColor;
            //Resources["ThemeColor"] = ThemeColors.DarkSemiTransparent;// target.Theme.ThemeColor;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //target.Theme = Themes.CreateTheme((ThemeColor)Resources["ThemeColor"], null);
            target.Theme = Themes.CreateTheme(ThemeColor1, null);
        }

        public ThemeColor ThemeColor1 { get => (ThemeColor)GetValue(ThemeColor1Property); set => SetValue(ThemeColor1Property, value); }

        public static readonly DependencyProperty ThemeColor1Property = DependencyProperty.Register(
            "ThemeColor1", typeof(ThemeColor), typeof(ThemeEditor), new PropertyMetadata(ThemeColors.DarkSemiTransparent));

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var h = MasterScrollViewer.ActualHeight;
            double headerH = 30;
            foreach (object item in MasterStackPanel.Children)
            {
                if (item.GetType() == typeof(Expander)) {
                    if (!((Expander)item).IsExpanded)
                    {
                        headerH = ((Expander)item).ActualHeight;
                        break;
                    }
                }
            }
            foreach (object item in MasterStackPanel.Children)
            {
                if (item.GetType() == typeof(FrameworkElement))
                {
                    ((FrameworkElement)item).MaxHeight = h - headerH * (MasterStackPanel.Children.Count - 1);
                }
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            var thisExpander = sender as Expander;
            foreach (object item in MasterStackPanel.Children)
            {
                if (item.GetType() == typeof(Expander))
                {
                    if (item != thisExpander)
                        ((Expander)item).IsExpanded = false;
                }
            }
        }
    }
}
