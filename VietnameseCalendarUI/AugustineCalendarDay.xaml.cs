/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AugustineCalendarDay : UserControl
    {
        public AugustineCalendarDay()
        {
            InitializeComponent();

            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            FaceStyle = FaceStyles.Normal;
            BorderStyle = BorderStyles.Normal;

            IsSolarMonthVisible = false;
            IsLunarMonthVisible = false;
            Label = "";
            lunarDate = null;
        }

        private LunarDate lunarDate;
        public LunarDate LunarDate { get => lunarDate; private set => lunarDate = value; }

        private DateTime solarDate;
        public DateTime SolarDate
        {
            get => solarDate;
            set
            {
                solarDate = value;
                try { lunarDate = LunarDate.FromSolar(solarDate, 7); }
                catch { lunarDate = null; }
                isSolarMonthVisible = solarDate.Day == 1;
                // in case of invalid date
                if (lunarDate != null)
                {
                    isLunarMonthVisible = (lunarDate.Day == 1) || (solarDate.Day == 1);
                }
                ToolTip = lunarDate.FullDayInfo;
                if (lunarDate.IsTermBegin)
                {
                    Label = lunarDate.SolarTerm;
                } else
                {
                    Label = "";
                }
                UpdateSolarDateLabel();
                UpdateLunarDateLabel();
            }
        }

        private bool isSolarMonthVisible;
        public bool IsSolarMonthVisible { get => isSolarMonthVisible; set { isSolarMonthVisible = value; UpdateSolarDateLabel(); } }

        private bool isLunarMonthVisible;
        public bool IsLunarMonthVisible { get => isLunarMonthVisible; set { isLunarMonthVisible = value; UpdateLunarDateLabel(); } }

        private string label;
        public string Label { get => label; set { label = value; UpdateDayLabel(); } }

        private FaceStyle faceStyle;
        public FaceStyle FaceStyle
        {
            get => faceStyle;
            set
            {
                faceStyle = value;
                Background = faceStyle.Backcolor;
                Foreground = faceStyle.Foreground;
                textSolar.FontSize = faceStyle.SolarFontSize;
                textLunar.FontSize = faceStyle.LunarFontSize;
                textLabel.FontSize = faceStyle.LabelFontSize;
                textSolar.FontWeight = faceStyle.FontWeight;
                textLunar.FontWeight = faceStyle.FontWeight;
            }
        }

        private BorderStyle borderStyle;
        public BorderStyle BorderStyle
        {
            get => borderStyle;
            set
            {
                borderStyle = value;
                Padding = borderStyle.Padding;
                BorderBrush = borderStyle.BorderColor;
                BorderThickness = borderStyle.BorderThickness;
            }
        }

        private void UpdateSolarDateLabel()
        {
            textSolar.Text = String.Format("{0}{1}",
                solarDate.Day, isSolarMonthVisible ? "/" + solarDate.Month : "");
        }

        private void UpdateLunarDateLabel()
        {
            if (lunarDate == null)
            {
                textLunar.Text = "!invalid!";
                return;
            }
            textLunar.Text = String.Format("{0}{1}",
                lunarDate.Day, isLunarMonthVisible ? ("/" + lunarDate.Month + (lunarDate.IsLeapMonth ? "n" : "")) : "");
        }

        private void UpdateDayLabel()
        {
            if (label == null || label.Length == 0)
            {
                textLabel.Visibility = Visibility.Collapsed;
                textLabel.Text = "";
            }
            else
            {
                textLabel.Visibility = Visibility.Visible;
                textLabel.Text = label;
            }
        }
    }
}
