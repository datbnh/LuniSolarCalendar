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
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.MouseOverBackground), null, "Ngày Đang Quan Tâm", false, "Ngày đang rê chuột lên."));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.NormalBackground), null, "Ngày Trong Tuần", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SaturdayBackground), nameof(ThemeColor.SaturdayForeground), "Thứ Bảy", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SundayBackground), nameof(ThemeColor.SundayForeground), "Chủ Nhật", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SpecialLevel1Background), nameof(ThemeColor.SpecialLevel1Foreground), "Ngày Đặc Biệt", false, "Những ngày như Tết, Giáng Sinh..."));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SpecialLevel2Background), nameof(ThemeColor.SpecialLevel2Foreground), "Ngày Đặc Biệt loại 2", false, "Những ngày như Rằm tháng Giêng, Quốc Tế Phụ Nữ, Valentine..."));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SpecialLevel3Background), nameof(ThemeColor.SpecialLevel3Foreground), "Ngày Đặc Biệt loại 3", false, "Những ngày mang tính nhắc nhở..."));
        }

        private Expander CreateColorPickerPanel(string backgroundPath, string foregroundPath, string headerText, bool isExpanded = false, string toolTipText = "")
        {
            // header
            var header = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Center,
            };
            header.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto, });
            header.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto, });

            var sampleText = new Label { Content = " Mẫu ", BorderBrush = Brushes.DimGray, BorderThickness = new Thickness(1), FontSize = 14 };
            var sampleBlock = new Border() { Background = Resources["CrossBrush"] as LinearGradientBrush, Padding = new Thickness(3), BorderBrush = null, };
            Grid.SetColumn(sampleBlock, 1);

            if (backgroundPath != nameof(ThemeColor.Background)) {
                var baseBackgroundBinding =
                    new Binding(nameof(ThemeColor.Background))
                    { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };
                var sampleBaseBackground = new Border()
                {
                    Background = Resources["CrossBrush"] as LinearGradientBrush,
                    Padding = new Thickness(3),
                    BorderBrush = null,
                };
                sampleBaseBackground.SetBinding(Border.BackgroundProperty, baseBackgroundBinding);
                sampleBaseBackground.Child = sampleText;
                sampleBlock.Child = sampleBaseBackground;
            } else
            {
                sampleBlock.Child = sampleText;
            }

            header.Children.Add(new Label() {
                Content = headerText + " ",
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Center,
                ToolTip = string.IsNullOrEmpty(toolTipText) ? null : toolTipText, });
            header.Children.Add(sampleBlock);

            // content
            var mainStackPanel = new StackPanel();
            var colorPickerStackPanel = new StackPanel() { Orientation = Orientation.Horizontal, };
            var colorInfoTextBlock = new TextBlock();
            var backgroundColorPicker = new ColorPicker();
            var backgroundInfoString = new Run();
            var foregroundInfoString = new Run();
                          
            colorInfoTextBlock.Inlines.Add("Màu nền: ");
            colorInfoTextBlock.Inlines.Add(backgroundInfoString);
            colorInfoTextBlock.Inlines.Add(" | Màu Chữ: ");
            colorInfoTextBlock.Inlines.Add(foregroundInfoString);

            colorPickerStackPanel.Children.Add(backgroundColorPicker);
            // foreground picker will be added in the below code if foregroundPath is not null nor empty

            mainStackPanel.Children.Add(colorInfoTextBlock);
            mainStackPanel.Children.Add(colorPickerStackPanel);

            // binding properties
            var backgroundBinding =
                new Binding(backgroundPath) { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };
            var backgroundBindingTwoWay =
                new Binding(backgroundPath) { Mode = BindingMode.TwoWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };

            sampleText.SetBinding(Label.BackgroundProperty, backgroundBinding);
            backgroundInfoString.SetBinding(Run.TextProperty, backgroundBinding);
            backgroundColorPicker.SetBinding(ColorPicker.BrushProperty, backgroundBindingTwoWay);

            if (!string.IsNullOrEmpty(foregroundPath))
            {
                var foregroundBinding =
                    new Binding(foregroundPath) { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };
                var foregroundBindingTwoWay =
                    new Binding(foregroundPath) { Mode = BindingMode.TwoWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };

                sampleText.SetBinding(Label.ForegroundProperty, foregroundBinding);
                foregroundInfoString.SetBinding(Run.TextProperty, foregroundBinding);

                var foregroundColorPicker = new ColorPicker();
                foregroundColorPicker.SetBinding(ColorPicker.BrushProperty, foregroundBindingTwoWay);
                colorPickerStackPanel.Children.Add(foregroundColorPicker);
            }
            else
            {
                var foregroundBinding =
                    new Binding(nameof(ThemeColor.Foreground)) { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };

                sampleText.SetBinding(Label.ForegroundProperty, foregroundBinding);
                foregroundInfoString.Text = "(Theo màu chữ chung)";
            }

            // finally, the outmost UIElement
            var expander = new Expander()
            {
                Header = header,
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
