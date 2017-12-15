using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Augustine.VietnameseCalendar.UI
{
    [DataContract]
    public class TextAndShadow
    {
        [DataMember]
        public bool IsDropShadow;

        [DataMember]
        public double ShadowDepth;

        [DataMember]
        public double ShadowRadius;

        [DataMember(Name = "ShadowColor")]
        internal string SShadowColor { get => ShadowColor.ToColorHexString(); set => ShadowColor = value.ToColor(); }
        public Color ShadowColor;

        [DataMember]
        public TextFormattingMode TextFormattingMode;

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

        public TextAndShadow() { }
    }
}
