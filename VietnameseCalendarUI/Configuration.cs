using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar.UI
{
    [DataContract]
    public class Configuration
    {
        [DataMember]
        public Theme Theme;

        [DataMember]
        public Location Location;

        [DataMember]
        public CalendarType CalendarType;

        [DataMember]
        public ViewMode ViewMode;

        public static Configuration DefaultConfiguration
        {
            get => new Configuration()
            {
                Theme = Themes.LightSemiTransparent,
                CalendarType = CalendarType.Month,
                ViewMode = ViewMode.Widget,
            };
        }
    }


    [DataContract]
    public class Location
    {
        [DataMember]
        public double Top;
        [DataMember]
        public double Left;
        [DataMember]
        public double Width;
        [DataMember]
        public double Height;
    }

    [DataContract]
    public enum CalendarType : byte
    {
        [EnumMember]
        Day = 0x00,
        [EnumMember]
        Month = 0x01,
    }

    [DataContract]
    public enum ViewMode : byte
    {
        [EnumMember]
        Widget = 0x00,
        [EnumMember]
        Normal = 0x01,
    }
}
