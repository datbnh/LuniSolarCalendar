/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using System.Runtime.Serialization;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Augustine.VietnameseCalendar.UI
{
    [DataContract]
    public class TextAndShadow
    {
        [DataMember]
        public bool IsDropShadow = false;

        [DataMember]
        public double ShadowDepth;

        [DataMember]
        public double ShadowRadius;

        [DataMember(Name = "ShadowColor")]
        internal string SShadowColor { get => ShadowColor.ToColorHexString(); set => ShadowColor = value.ToColor(); }
        public Color ShadowColor;

        [DataMember]
        public TextFormattingMode TextFormattingMode = TextFormattingMode.Ideal;

        private DropShadowEffect shadowEffect;
        public DropShadowEffect ShadowEffect
        {
            get
            {
                if (shadowEffect == null)
                  shadowEffect = new DropShadowEffect()
                  {
                      BlurRadius = ShadowRadius,
                      ShadowDepth = ShadowDepth,
                      Color = ShadowColor,
                  };
                return shadowEffect;
            }
        }
    }
}
