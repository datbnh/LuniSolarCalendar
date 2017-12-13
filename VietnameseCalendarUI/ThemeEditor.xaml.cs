using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for ThemeEditor.xaml
    /// </summary>
    public partial class ThemeEditor : Window
    {
        private AugustineCalendarMonth target;
        private ThemeColor initialState;

        public ThemeEditor()
        {
            InitializeComponent();
            DataContext = this;
            ThemeColor1 = ThemeColors.DarkSemiTransparent;

            
        }

        private void InitializeColorPickers()
        {
            ExpanderColorsStackPanel.Children.Clear();
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.Background), nameof(ThemeColor.Foreground), "CHUNG", true));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.NormalBackground), nameof(ThemeColor.Foreground), "Ngày Trong Tuần", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SaturdayBackground), nameof(ThemeColor.SaturdayForeground), "Thứ Bảy", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SundayBackground), nameof(ThemeColor.SundayForeground), "Chủ Nhật", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SpecialLevel1Background), nameof(ThemeColor.SpecialLevel1Foreground), "Ngày Đặc Biệt (Giáng Sinh, Tết...)", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SpecialLevel2Background), nameof(ThemeColor.SpecialLevel2Foreground), "Ngày Đặc Biệt loại 2 (Rằm tháng Giêng, Tết Đoan Ngọ...)", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SpecialLevel3Background), nameof(ThemeColor.SpecialLevel3Foreground), "Ngày Đặc Biệt loại 3 (nhắc nhở)", false));
        }

        private Expander CreateColorPickerPanel(string backgroundPath, string foregroundPath, string headerText, bool isExpanded = false)
        {
            var backgroundBinding =
                new Binding(backgroundPath) { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };
            var backgroundBindingTwoWay =
                new Binding(backgroundPath) { Mode = BindingMode.TwoWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };

            var foregroundBinding =
                new Binding(foregroundPath) { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };
            var foregroundBindingTwoWay =
                new Binding(foregroundPath) { Mode = BindingMode.TwoWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };

            var sampleText = new Run(" [ Mẫu ] ");
            sampleText.SetBinding(Run.BackgroundProperty, backgroundBinding);
            sampleText.SetBinding(Run.ForegroundProperty, foregroundBinding);
            var headerTextBlock = new TextBlock();
            headerTextBlock.Inlines.Add(new Run(headerText + " ") { FontWeight = FontWeights.Bold, });
            headerTextBlock.Inlines.Add(sampleText);

            var colorInfoTextBlock = new TextBlock();
            var backgroundInfoString = new Run();
            var foregroundInfoString = new Run();
            backgroundInfoString.SetBinding(Run.TextProperty, backgroundBinding);
            foregroundInfoString.SetBinding(Run.TextProperty, foregroundBinding);
            colorInfoTextBlock.Inlines.Add("Màu nền: ");
            colorInfoTextBlock.Inlines.Add(backgroundInfoString);
            colorInfoTextBlock.Inlines.Add(" | Màu Chữ: ");
            colorInfoTextBlock.Inlines.Add(foregroundInfoString);

            var backgroundColorPicker = new ColorPicker();
            backgroundColorPicker.SetBinding(ColorPicker.BrushProperty, backgroundBindingTwoWay);
            var foregroundColorPicker = new ColorPicker();
            foregroundColorPicker.SetBinding(ColorPicker.BrushProperty, foregroundBindingTwoWay);

            var colorPickerStackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
            };
            colorPickerStackPanel.Children.Add(backgroundColorPicker);
            colorPickerStackPanel.Children.Add(foregroundColorPicker);

            var mainStackPanel = new StackPanel();
            mainStackPanel.Children.Add(colorInfoTextBlock);
            mainStackPanel.Children.Add(colorPickerStackPanel);

            var expander = new Expander()
            {
                Header = headerTextBlock,
                Content = mainStackPanel,
                IsExpanded = isExpanded,
            };
            return expander;
        }

        public void SetTarget(AugustineCalendarMonth calendar)
        {
            target = calendar;
            ThemeColor1 =  target.Theme.ThemeColor;
            initialState = (ThemeColor)ThemeColor1.Clone();
            //Resources["ThemeColor"] = ThemeColors.DarkSemiTransparent;// target.Theme.ThemeColor;
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
                if (item is Expander) {
                    if (!((Expander)item).IsExpanded)
                    {
                        headerH = ((Expander)item).ActualHeight;
                        break;
                    }
                }
            }
            foreach (object item in MasterStackPanel.Children)
            {
                if (item is FrameworkElement)
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
                if (item is Expander)
                {
                    if (item != thisExpander)
                        ((Expander)item).IsExpanded = false;
                }
            }
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            initialState = (ThemeColor)ThemeColor1.Clone();
            target.Theme = Themes.CreateTheme(ThemeColor1, null);
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeColorPickers();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void restore_Click(object sender, RoutedEventArgs e)
        {
            ThemeColor1 = (ThemeColor)initialState.Clone();
            InitializeColorPickers();
        }
    }
}
