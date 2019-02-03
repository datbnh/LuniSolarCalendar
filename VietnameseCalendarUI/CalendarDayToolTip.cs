/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core.LuniSolarCalendar;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    public static class CalendarDayToolTip
    {
        public static ToolTip CreateToolTip(string header, LuniSolarDate date,
            string decorator, bool isSymbolicDecorator, double hueValue = -1,
            bool overideContentForeground = false,
            int maxWidth = 400, int padding = 3)
        {
            Grid contentGrid = new Grid();
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition());

            string[] columnHeader = { "Dương lịch: ", "Âm lịch: ", "Năm ", "Tháng ", "Ngày ", "Tiết " };
            string[] columnContent = {
                string.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:d} ({0:dddd})",date.SolarDate),
                string.Format("Ngày {0} tháng {1} năm {2}", date.Day, date.MonthShortName, date.Year),
                date.YearName, date.MonthLongName, date.DayName, date.SolarTerm };
            for (int i = 0; i < columnHeader.Length; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
                TextBlock headerCell = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Text = columnHeader[i],
                };
                TextBlock contentCell = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    TextAlignment = TextAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    Text = columnContent[i],
                    FontWeight = FontWeights.SemiBold,
                };
                Grid.SetRow(headerCell, i);
                Grid.SetRow(contentCell, i);
                Grid.SetColumn(headerCell, 0);
                Grid.SetColumn(contentCell, 1);
                contentGrid.Children.Add(headerCell);
                contentGrid.Children.Add(contentCell);
            }
            ContentControl cc = new ContentControl
            {
                Content = contentGrid
            };

            return ToolTipWithHeader.CreateToolTip(header, null, cc,
            decorator, isSymbolicDecorator, hueValue, overideContentForeground, maxWidth, padding);
        }
    }

    public static class ToolTipWithHeader
    {
        public static ToolTip CreateToolTip(string header, string subHeader, Control content,
            string decorator, bool isSymbolicDecorator = true, double hueValue = -1, bool overrideContentForeground = false,
            int maxWidth = 200, int padding = 3, double headerFontSize = 14)
        {
            ToolTip toolTip = new ToolTip
            {
                MaxWidth = maxWidth,
                Padding = new Thickness(padding),
                FontFamily = FontManager.Body,
            };

            StackPanel stackPanel = new StackPanel();

            if (!string.IsNullOrEmpty(header))
            {
                TextBlock headerTextBlock = new TextBlock
                {
                    Text = header,
                    TextAlignment = TextAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = FontManager.Heading,
                    FontWeight = FontWeights.SemiBold,
                    FontSize = headerFontSize,
                };
                stackPanel.Children.Add(headerTextBlock);
            }

            if (!string.IsNullOrEmpty(subHeader))
            {
                TextBlock subHeaderTextBlock = new TextBlock
                {
                    Text = subHeader,
                    TextWrapping = TextWrapping.Wrap,
                };
                stackPanel.Children.Add(subHeaderTextBlock);
            }

            if (!(string.IsNullOrEmpty(header) && string.IsNullOrEmpty(subHeader))
                && content != null)
            {
                stackPanel.Children.Add(new Separator());
            }

            if (content != null)
            {
                stackPanel.Children.Add(content);
            }

            if (hueValue >= 0)
            {
                Color background = Helper.GetBackgroundFromHue(hueValue);
                Color foreground = Helper.GetForegroundFromHue(hueValue);
                toolTip.Background = new SolidColorBrush(background);
                toolTip.Foreground = new SolidColorBrush(foreground);
                if (overrideContentForeground && content != null)
                {
                    content.Foreground = new SolidColorBrush(foreground);
                }
            }

            if (!string.IsNullOrEmpty(decorator))
            {
                TextBlock decoratorTextBlock = new TextBlock
                {
                    Opacity = 0.2,
                    Text = decorator,
                    FontFamily = isSymbolicDecorator ? FontManager.Symbol : FontManager.Body,
                };

                VisualBrush decoratorVisualBrush = new VisualBrush
                {
                    Stretch = Stretch.Uniform,
                    AlignmentX = AlignmentX.Right,
                    AlignmentY = AlignmentY.Bottom,
                    Visual = decoratorTextBlock
                };

                Grid decoratorGrid = new Grid
                {
                    Background = decoratorVisualBrush,
                    Margin = new Thickness(0, 0, -15, -15),
                };

                Grid mainGrid = new Grid();
                mainGrid.Children.Add(decoratorGrid);
                mainGrid.Children.Add(stackPanel);

                toolTip.Content = mainGrid;
            }
            else
            {
                toolTip.Content = stackPanel;
            }

            return toolTip;
        }
    }
}
