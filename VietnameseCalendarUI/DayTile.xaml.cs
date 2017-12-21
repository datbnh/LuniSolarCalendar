﻿/*************************************************************
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
using System.Windows.Media;
using static Augustine.VietnameseCalendar.UI.SpecialDayManager;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DayTile : UserControl
    {
        public DayTile()
        {
            InitializeComponent();

            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;

            IsSolarMonthVisible = false;
            IsLunarMonthVisible = false;
            Label = "Label";
            Decorator.Text = "*";
            DayType = DayTypes.Normal;
        }

        private DateTime solarDate;
        public DateTime SolarDate
        {
            get => solarDate;
            set
            {
                solarDate = value;

                try { lunarDate = LuniSolarDate.LuniSolarDateFromSolarDate(solarDate, 7); }
                catch { lunarDate = null; }

                //isSolarMonthVisible = solarDate.Day == 1;

                var toolTipTitle = "";
                var toolTipDecorator = "";

                // in case of invalid date - BUG!!!
                if (lunarDate != null)
                {
                    isLunarMonthVisible = (lunarDate.Day == 1) || (solarDate.Day == 1);
                    if (lunarDate.GetSpecialDateInfo(out SpecialDateInfo specialDayInfo))
                    {
                        Label = specialDayInfo.Label;
                        Decorator.Text = specialDayInfo.Decorator;
                        toolTipTitle = Label;
                        DayType = specialDayInfo.DayType;
                    }
                    else
                    {
                        if (lunarDate.IsTermBeginThisDay)
                        {
                            Label = lunarDate.SolarTerm;
                            toolTipTitle = "Bắt đầu tiết " + Label;
                            switch (lunarDate.SolarTermIndex)
                            {
                                case 0:
                                    Decorator.Text = SolarTermDecorator.VernalEquinox;
                                    break;
                                case 6:
                                    Decorator.Text = SolarTermDecorator.SummerSolstice;
                                    break;
                                case 12:
                                    Decorator.Text = SolarTermDecorator.AutumnalEquinox;
                                    break;
                                case 18:
                                    Decorator.Text = SolarTermDecorator.WinterSolstice;
                                    break;
                                default:
                                    toolTipTitle = ""; // normal term is ignored.
                                    Decorator.Text = "";
                                    break;
                            }
                        }
                        else
                        {
                            Label = "";
                            Decorator.Text = "";
                        }

                        if (solarDate.DayOfWeek == DayOfWeek.Saturday)
                            DayType = DayTypes.Saturday;
                        else if (solarDate.DayOfWeek == DayOfWeek.Sunday)
                            DayType = DayTypes.Sunday;
                        else
                            DayType = DayTypes.Normal;
                    }

                    toolTipDecorator = Decorator.Text;
                    if (string.IsNullOrEmpty(toolTipDecorator))
                        toolTipDecorator = lunarDate.SolarDate.Day.ToString();

                    ToolTip = CalendarDayToolTip.CreateToolTip(toolTipTitle, lunarDate, toolTipDecorator, -1);    
                }

                UpdateSolarDateLabel();
                UpdateLunarDateLabel();
            }
        }

        private LuniSolarDate lunarDate;
        public LuniSolarDate LunarDate { get => lunarDate; private set => lunarDate = value; }

        private bool isSolarMonthVisible;
        public bool IsSolarMonthVisible { get => isSolarMonthVisible; set { isSolarMonthVisible = value; UpdateSolarDateLabel(); } }

        private bool isLunarMonthVisible;
        public bool IsLunarMonthVisible { get => isLunarMonthVisible; set { isLunarMonthVisible = value; UpdateLunarDateLabel(); } }

        private string label;
        public string Label { get => label; set { label = value; UpdateDayLabel(); } }

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

        public DayTypes DayType { get => (DayTypes)GetValue(DayTypeProperty); set => SetValue(DayTypeProperty, value); }

        public bool IsSelected { get  => (bool)GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }

        public Theme Theme { get => (Theme)GetValue(ThemeProperty); set
            {
                SetValue(ThemeProperty, value);
                Style = value.DayTileStyle;
            }
        }

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(
            "Theme", typeof(Theme), typeof(DayTile), new PropertyMetadata(Themes.Light));

        public static readonly DependencyProperty DayTypeProperty = DependencyProperty.Register(
            "DayType", typeof(DayTypes), typeof(DayTile), new PropertyMetadata(DayTypes.Normal));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof(bool), typeof(DayTile), new PropertyMetadata(false));

    }
}