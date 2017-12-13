﻿

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
        internal bool IsInitializing = true;

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
            new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), OnBrushPropertyChanged, null));

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

        //private static object OnCoerceA(DependencyObject d, object baseValue)
        //{
        //    Console.WriteLine("Coerce A");
        ////    return (byte)baseValue;
        //    return ((SolidColorBrush)((ColorPicker)d).Brush).Color.A;
        //}

        //private static object OnCoerceR(DependencyObject d, object baseValue)
        //{
        //    Console.WriteLine("Coerce R");
        //    //return (byte)baseValue;
        //    return ((SolidColorBrush)((ColorPicker)d).Brush).Color.R;
        //}

        //private static object OnCoerceG(DependencyObject d, object baseValue)
        //{
        //    Console.WriteLine("Coerce G");
        //    //return (byte)baseValue;
        //    return ((SolidColorBrush)((ColorPicker)d).Brush).Color.G;
        //}

        //private static object OnCoerceB(DependencyObject d, object baseValue)
        //{
        //    Console.WriteLine("Coerce B");
        //    //return (byte)baseValue;
        //    return ((SolidColorBrush)((ColorPicker)d).Brush).Color.B;
        //}

        private static void OnColorComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var IsBrushUpdateRequired = false;
            var cp = d as ColorPicker;
            Console.WriteLine("[" + e.Property + ">] NewValue = " + e.NewValue);
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
            Console.WriteLine("IsBrushUpdateRequired = " + IsBrushUpdateRequired);
            if (IsBrushUpdateRequired)
                cp.SetValue(BrushProperty, new SolidColorBrush(Color.FromArgb(cp.A, cp.R, cp.G, cp.B)));

            //if (cp.GetValue(e.Property) != e.NewValue)
            //{
            //    Console.WriteLine(e.Property + " Changed | cp.GetValue(e.Property) = " + cp.GetValue(e.Property) + " | e.NewValue = " + e.NewValue);
            //    Console.WriteLine(((SolidColorBrush) cp.Brush).Color.A + " " + ((SolidColorBrush)cp.Brush).Color.R + " " +
            //        ((SolidColorBrush)cp.Brush).Color.G + " " +((SolidColorBrush)cp.Brush).Color.B + " ");
            //    cp.SetValue(BrushProperty, new SolidColorBrush(Color.FromArgb(cp.A, cp.R, cp.G, cp.B)));
            //}
            //else
            //{
            //    Console.WriteLine(e.Property + " Unchanged | cp.GetValue(e.Property) = " + cp.GetValue(e.Property) + " | e.NewValue = " + e.NewValue);
            //    Console.WriteLine(cp.Brush);
            //    Console.WriteLine(((SolidColorBrush)cp.Brush).Color.A + " " + ((SolidColorBrush)cp.Brush).Color.R + " " +
            //        ((SolidColorBrush)cp.Brush).Color.G + " " + ((SolidColorBrush)cp.Brush).Color.B + " ");
            //}
            Console.WriteLine("[<" + e.Property + "]");
        }

        private static void OnBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cp = d as ColorPicker;
            if (cp.Brush != null)
            {
                Console.WriteLine("[Brush>]" + e.Property + " Changed. NewValue = " + e.NewValue);
                cp.SetValue(BrushProperty, e.NewValue);
                cp.SetValue(AProperty, ((SolidColorBrush)cp.Brush).Color.A);
                cp.SetValue(RProperty, ((SolidColorBrush)cp.Brush).Color.R);
                cp.SetValue(GProperty, ((SolidColorBrush)cp.Brush).Color.G);
                cp.SetValue(BProperty, ((SolidColorBrush)cp.Brush).Color.B);
                Console.WriteLine("[<Brush]" + cp.Brush);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}