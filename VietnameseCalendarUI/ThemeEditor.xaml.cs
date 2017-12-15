/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

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
            //DataContext = this;
        }

        private void SynchronizeColorPickers()
        {
            ExpanderColorsStackPanel.Children.Clear();
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.Background), nameof(ThemeColor.Foreground), "CHUNG", true));
            ExpanderColorsStackPanel.Children.Add(new Separator());
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.MouseOverBackground), null, "Ngày Đang Quan Tâm", false, "Ngày đang rê chuột lên."));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.NormalBackground), null, "Ngày Trong Tuần", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.GrayedOutBackground), nameof(ThemeColor.GrayedOutForeground), "Ngày Không Thuộc Tháng Hiện Tại", false));
            ExpanderColorsStackPanel.Children.Add(new Separator());
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SaturdayBackground), nameof(ThemeColor.SaturdayForeground), "Thứ Bảy", false));
            ExpanderColorsStackPanel.Children.Add(CreateColorPickerPanel(nameof(ThemeColor.SundayBackground), nameof(ThemeColor.SundayForeground), "Chủ Nhật", false));
            ExpanderColorsStackPanel.Children.Add(new Separator());
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
            var colorPickerStackPanel = new StackPanel() { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center, };
            //var colorInfoTextBlock = new TextBlock();
            //var colorInfo = new Grid();
            var colorInfoStackPanel = new StackPanel() { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center, };
            var backgroundColorPicker = new ColorPicker();
            var backgroundInfoString = new TextBox() { VerticalContentAlignment = VerticalAlignment.Center, };
            var foregroundInfoString = new TextBox() { VerticalContentAlignment = VerticalAlignment.Center, };

            colorInfoStackPanel.Children.Add(new Label() { Content = "Màu nền: ", VerticalContentAlignment = VerticalAlignment.Center });
            colorInfoStackPanel.Children.Add(backgroundInfoString);
            colorInfoStackPanel.Children.Add(new Label() { Content = " | Màu Chữ: ", VerticalContentAlignment = VerticalAlignment.Center });
            colorInfoStackPanel.Children.Add(foregroundInfoString);

            colorPickerStackPanel.Children.Add(backgroundColorPicker);
            // foreground picker will be added in the below code if foregroundPath is not null nor empty

            mainStackPanel.Children.Add(colorInfoStackPanel);
            mainStackPanel.Children.Add(colorPickerStackPanel);

            // binding properties
            var backgroundBinding =
                new Binding(backgroundPath) { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };
            var backgroundBindingTwoWay =
                new Binding(backgroundPath) { Mode = BindingMode.TwoWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };

            sampleText.SetBinding(Label.BackgroundProperty, backgroundBinding);
            backgroundInfoString.SetBinding(TextBox.TextProperty, backgroundBindingTwoWay);
            backgroundColorPicker.SetBinding(ColorPicker.BrushProperty, backgroundBindingTwoWay);

            if (!string.IsNullOrEmpty(foregroundPath))
            {
                var foregroundBinding =
                    new Binding(foregroundPath) { Mode = BindingMode.OneWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };
                var foregroundBindingTwoWay =
                    new Binding(foregroundPath) { Mode = BindingMode.TwoWay, Source = this.ThemeColor1, NotifyOnSourceUpdated = true, };

                sampleText.SetBinding(Label.ForegroundProperty, foregroundBinding);
                foregroundInfoString.SetBinding(TextBox.TextProperty, foregroundBindingTwoWay);

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
                foregroundInfoString.IsEnabled = false;
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
            ThemeColor1 = (ThemeColor)target.Theme.ThemeColor.Clone();
            initialState = (ThemeColor)ThemeColor1.Clone();

            SynchronizeColorPickers();
            SynchronizeAdvancedSettings();
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
            target.Theme = new Theme(ThemeColor1, null, null);
            if ((bool)isDropShadow.IsChecked)
            {
                double r = 3;
                double d = 1;
                Color c = Colors.DimGray;

                if (!double.TryParse(DropShadowRadius.Text, out r))
                    DropShadowRadius.Text = r.ToString();
                if (!double.TryParse(DropShadowDepth.Text, out d))
                    DropShadowDepth.Text = d.ToString();
                try { c = (Color)ColorConverter.ConvertFromString(DropShadowColor.Text); }
                catch (Exception) { }

                DropShadowColor.Text = c.ToString();

                target.Effect = new DropShadowEffect { BlurRadius = r, Color = c, ShadowDepth = d };
            } 
            else
            {
                target.ClearValue(EffectProperty);
            }

            if (textFormattingMode.SelectedIndex == 0)
            {
                TextOptions.SetTextFormattingMode(target, TextFormattingMode.Ideal);
            }
            else if(textFormattingMode.SelectedIndex == 1)
            {
                TextOptions.SetTextFormattingMode(target, TextFormattingMode.Display);
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            SynchronizeColorPickers();
            SynchronizeAdvancedSettings();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void restore_Click(object sender, RoutedEventArgs e)
        {
            ThemeColor1 = (ThemeColor)initialState.Clone();
            SynchronizeColorPickers();
            SynchronizeAdvancedSettings();
        }

        private void SynchronizeAdvancedSettings()
        {
            if (target.Effect != null && target.Effect is DropShadowEffect)
            {
                var ef = target.Effect as DropShadowEffect;
                isDropShadow.IsChecked = true;
                DropShadowRadius.Text = ef.BlurRadius.ToString();
                DropShadowDepth.Text = ef.ShadowDepth.ToString();
                DropShadowColor.Text = ef.Color.ToString();
            }
            if (TextOptions.GetTextFormattingMode(target) == TextFormattingMode.Ideal)
            {
                textFormattingMode.SelectedIndex = 0;
            }
            else
            {
                textFormattingMode.SelectedIndex = 1;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Vietnamese calendar configuration files (*.vccfg)|*.vccfg|All files (*.*)|*.*";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    ThemeColor1.SaveToFile(saveFileDialog.FileName);
                    MessageBox.Show("Đã lưu file thành công!", "Lịch Việt Nam: Lưu Cấu Hình", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã gặp lỗi trong khi lưu dữ liệu. Vui lòng thử lại."
                    + Environment.NewLine + Environment.NewLine + "// Dành cho lập trình viên:"
                    + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException.Message,
                    "Lịch Việt Nam: Lưu Cấu Hình", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Vietnamese calendar configuration files (*.vccfg)|*.vccfg|All files (*.*)|*.*";
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    Serializer.LoadFromFile<ThemeColor>(openFileDialog.FileName, out ThemeColor loadedTheme);
                    ThemeColor1 = loadedTheme;
                    MessageBox.Show("Đã đọc file thành công!", "Lịch Việt Nam: Đọc Cấu Hình", MessageBoxButton.OK, MessageBoxImage.Information);
                    SynchronizeColorPickers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã gặp lỗi trong khi đọc dữ liệu. File đã chọn không hợp lệ. Vui lòng thử lại."
                    + Environment.NewLine + Environment.NewLine + "// Dành cho lập trình viên:"
                    + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException.Message,
                    "Lịch Việt Nam: Đọc Cấu Hình", MessageBoxButton.OK, MessageBoxImage.Error);
            }         
        }
    }
}
