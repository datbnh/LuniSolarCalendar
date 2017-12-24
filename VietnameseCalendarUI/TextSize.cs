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
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{  
    [DataContract]
    public class TextSize
    {
        [DataMember(Name = "Small")]
        private SizeSet small = null;
        public SizeSet Small { get => (small ?? Normal); set => small = value; }
        [DataMember(IsRequired = true)]
        public SizeSet Normal { get; set; } = new SizeSet();
        [DataMember(Name = "Large")]
        private SizeSet large = null;
        public SizeSet Large { get => (large ?? Normal); set => large = value; }
        [DataMember]
        public bool IsTileLableHiddenInSmallSize { get; set; } = true;

        public SizeSet GetSizeSet(SizeMode sizeMode)
        {
            switch (sizeMode)
            {
                case SizeMode.Small:
                    return Small;
                case SizeMode.Large:
                    return Large;
                default:
                    return Normal;
            }
        }

        public SizeSet GetSizeSet(double clientWidth, double clientHeight)
        {
            return GetSizeSet(GetSizeMode(clientWidth, clientHeight));
        }

        public SizeMode GetSizeMode(double clientWidth, double clientHeight)
        {
            if (clientWidth > Normal.ClientWidthMax 
                && clientHeight > Normal.ClientHeightMax)
                return SizeMode.Large;
            else if (clientWidth < Small.ClientWidthMax
                || clientHeight < Small.ClientHeightMax)
                return SizeMode.Small;
            else
                return SizeMode.Normal;
        }

        public static TextSize DefaultTextSize()
        {
            TextSize textSize = new TextSize();
            textSize.Small = new SizeSet()
            {
                ClientWidthMax = 480,
                ClientHeightMax = 360,
                DayTileSolarTextSize = 18,
                DayTileLunarTextSize = 12,
                DayTileLabelTextSize = 8,
                MonthSolarLabelTextSize = 16,
                MonthLunarLabelTextSize = 14,
            };
            textSize.Large = new SizeSet()
            {
                ClientWidthMax = 9999,
                ClientHeightMax = 9999,
                DayTileSolarTextSize = 36,
                DayTileLunarTextSize = 24,
                DayTileLabelTextSize = 12,
                MonthSolarLabelTextSize = 24,
                MonthLunarLabelTextSize = 20,
            };
            return textSize;
        }

        [DataContract]
        public enum SizeMode
        {
            [EnumMember]
            Small,
            [EnumMember]
            Normal,
            [EnumMember]
            Large,
        }

        [DataContract]
        public class SizeSet
        {
            [DataMember]
            public double ClientWidthMax = 725;
            [DataMember]
            public double ClientHeightMax = 525;
            [DataMember]
            public double DayTileSolarTextSize = 24;
            [DataMember]
            public double DayTileLunarTextSize = 16;
            [DataMember]
            public double DayTileLabelTextSize = 10;
            [DataMember]
            public double MonthSolarLabelTextSize = 18;
            [DataMember]
            public double MonthLunarLabelTextSize = 16;
        }
    }
}
