/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
            DataContext = this;
        }

        public byte A { get => (byte)GetValue(AProperty); set => SetValue(AProperty, value); }

        public byte R { get => (byte)GetValue(RProperty); set => SetValue(RProperty, value); }

        public byte G { get => (byte)GetValue(GProperty); set => SetValue(GProperty, value); }

        public byte B { get => (byte)GetValue(BProperty); set => SetValue(BProperty, value); }

        public Brush Brush { get => (SolidColorBrush)GetValue(BrushProperty); set => SetValue(BrushProperty, value); }

        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
            "Brush", typeof(Brush), typeof(ColorPicker),
            new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0, 255, 255, 255)), OnBrushPropertyChanged, null));

        public static readonly DependencyProperty AProperty = DependencyProperty.Register(
            "A", typeof(byte), typeof(ColorPicker),
            new PropertyMetadata((byte)255, OnColorComponentPropertyChanged, null));

        public static readonly DependencyProperty RProperty = DependencyProperty.Register(
            "R", typeof(byte), typeof(ColorPicker),
            new PropertyMetadata((byte)255, OnColorComponentPropertyChanged, null));

        public static readonly DependencyProperty GProperty = DependencyProperty.Register(
            "G", typeof(byte), typeof(ColorPicker),
            new PropertyMetadata((byte)255, OnColorComponentPropertyChanged, null));

        public static readonly DependencyProperty BProperty = DependencyProperty.Register(
            "B", typeof(byte), typeof(ColorPicker),
            new PropertyMetadata((byte)255, OnColorComponentPropertyChanged, null));

        private static void OnColorComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var IsBrushUpdateRequired = false;
            var cp = d as ColorPicker;
            //Console.WriteLine("[" + e.Property + ">] NewValue = " + e.NewValue);
            if (e.Property == AProperty)
            {
                if ((byte)cp.GetValue(AProperty) != ((SolidColorBrush)cp.Brush).Color.A)
                {
                    IsBrushUpdateRequired = true;
                }
            }
            else if (e.Property == RProperty)
            {
                if ((byte)cp.GetValue(RProperty) != ((SolidColorBrush)cp.Brush).Color.R)
                {
                    IsBrushUpdateRequired = true;
                }
            }
            else if (e.Property == GProperty)
            {
                if ((byte)cp.GetValue(GProperty) != ((SolidColorBrush)cp.Brush).Color.G)
                {
                    IsBrushUpdateRequired = true;
                }
            }
            else if (e.Property == BProperty)
            {
                if ((byte)cp.GetValue(BProperty) != ((SolidColorBrush)cp.Brush).Color.B)
                {
                    IsBrushUpdateRequired = true;
                }
            }
            //Console.WriteLine("IsBrushUpdateRequired = " + IsBrushUpdateRequired);
            if (IsBrushUpdateRequired)
                cp.SetValue(BrushProperty, new SolidColorBrush(Color.FromArgb(cp.A, cp.R, cp.G, cp.B)));

            //Console.WriteLine("[<" + e.Property + "]");
        }

        private static void OnBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cp = d as ColorPicker;
            if (cp.Brush != null)
            {
                //Console.WriteLine("[Brush>]" + e.Property + " Changed. NewValue = " + e.NewValue);
                cp.SetValue(BrushProperty, e.NewValue);
                cp.SetValue(AProperty, ((SolidColorBrush)cp.Brush).Color.A);
                cp.SetValue(RProperty, ((SolidColorBrush)cp.Brush).Color.R);
                cp.SetValue(GProperty, ((SolidColorBrush)cp.Brush).Color.G);
                cp.SetValue(BProperty, ((SolidColorBrush)cp.Brush).Color.B);
                //Console.WriteLine("[<Brush]" + cp.Brush);
            } else
            {
                cp.SetValue(BrushProperty, new SolidColorBrush(Color.FromArgb(0, 255, 255, 255)));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(string property)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        //}
    }
}
