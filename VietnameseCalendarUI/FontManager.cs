using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    public class FontManager
    {
        public static FontFamily FontFamily(string familyName)
        {
            return new FontFamily(new System.Uri("pack://application:,,,/"), "/Fonts/#" + familyName);
        }

        public static FontFamily Symbol { get; } = FontFamily("Noto Emoji");

        public static FontFamily Heading { get; } = FontFamily("Open Sans");

        public static FontFamily Body { get; } = FontFamily("Open Sans");

    }
}
