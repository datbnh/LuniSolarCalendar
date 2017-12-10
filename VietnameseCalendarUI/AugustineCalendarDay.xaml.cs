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
using System.Windows.Media;
using static Augustine.VietnameseCalendar.UI.SpecialDayManager;

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
            Label = "Label";
            Decorator.Text = "*";
        }

        private DateTime solarDate;
        public DateTime SolarDate
        {
            get => solarDate;
            set
            {
                solarDate = value;
                try
                {
                    lunarDate = LuniSolarDate.LuniSolarDateFromSolarDate(solarDate, 7);
                }
                catch
                {
                    lunarDate = null;
                }

                isSolarMonthVisible = solarDate.Day == 1;

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
                    }

                    toolTipDecorator = Decorator.Text;
                    if (string.IsNullOrEmpty(toolTipDecorator))
                        toolTipDecorator = lunarDate.SolarDate.Day.ToString();
                    CalendarDayToolTipView toolTipContent =
                        new CalendarDayToolTipView(new CalendarDayToolTipModel(lunarDate, toolTipTitle, toolTipDecorator));
                    ToolTip = new ToolTip()
                    {
                        Content = toolTipContent,
                    };
                }
                
                UpdateSolarDateLabel();
                UpdateLunarDateLabel();
            }
        }

        //public bool IsSolarSpecial { get; private set; }
        //public bool IsLunarSpecial { get; private set; }

        private LuniSolarDate lunarDate;
        public LuniSolarDate LunarDate { get => lunarDate; private set => lunarDate = value; }

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
