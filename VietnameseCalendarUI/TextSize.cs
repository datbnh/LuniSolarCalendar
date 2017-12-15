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

        public SizeSet GetSizeSet(double clientWidth, double clientHeight)
        {
            switch (GetSizeMode(clientWidth, clientHeight))
            {
                case SizeModes.Small:
                    return Small;
                case SizeModes.Large:
                    return Large;
                default:
                    return Normal;
            }
        }

        public SizeModes GetSizeMode(double clientWidth, double clientHeight)
        {
            if (clientWidth > Normal.ClientWidthMax 
                && clientHeight > Normal.ClientHeightMax)
                return SizeModes.Large;
            else if (clientWidth < Small.ClientWidthMax
                && clientHeight < Small.ClientHeightMax)
                return SizeModes.Small;
            else
                return SizeModes.Normal;
        }

        [DataContract]
        public enum SizeModes
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
            public double ClientWidthMax = 800;
            [DataMember]
            public double ClientHeightMax = 600;
            [DataMember]
            public double DayTileSolarTextSize = 24;
            [DataMember]
            public double DayTileLunarTextSize = 26;
            [DataMember]
            public double DayTileLabelTextSize = 10;
            [DataMember]
            public double MonthSolarLabelTextSize = 18;
            [DataMember]
            public double MonthLunarLabelTextSize = 16;
        }
    }
}
