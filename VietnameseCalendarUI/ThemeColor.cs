using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Augustine.VietnameseCalendar.UI
{
    [DataContract]
    public class ThemeColor : INotifyPropertyChanged //, ICloneable
    {
        private Brush border;
        [DataMember(Name = "Border")]
        internal string SBorder
        {
            get => Helper.ToColorHexString(Border);
            set => Border = Helper.ToBrush(value);
        }

        private Brush selectedBorder;
        [DataMember(Name = "SelectedBorder")]
        internal string SSelectedBorder
        {
            get => Helper.ToColorHexString(SelectedBorder);
            set => SelectedBorder = Helper.ToBrush(value);
        }

        private Brush background;
        [DataMember(Name = "Background")]
        internal string SBackground
        {
            get => Helper.ToColorHexString(Background);
            set => Background = Helper.ToBrush(value);
        }

        private Brush foreground;
        [DataMember(Name = "Foreground")]
        internal string SForeground
        {
            get => Helper.ToColorHexString(Foreground);
            set => Foreground = Helper.ToBrush(value);
        }

        private Brush normalBackground;
        [DataMember(Name = "NormalBackground")]
        internal string SNormalBackground
        {
            get => Helper.ToColorHexString(NormalBackground);
            set => NormalBackground = Helper.ToBrush(value);
        }

        private Brush mouseOverBackground;
        [DataMember(Name = "MouseOverBackground")]
        internal string SMouseOverBackground
        {
            get => Helper.ToColorHexString(MouseOverBackground);
            set => MouseOverBackground = Helper.ToBrush(value);
        }

        private Brush highlightForeground;
        [DataMember(Name = "HighlightForeground")]
        internal string SHighlightForeground
        {
            get => Helper.ToColorHexString(HighlightForeground);
            set => HighlightForeground = Helper.ToBrush(value);
        }

        private Brush saturdayBackground;
        [DataMember(Name = "SaturdayBackground")]
        internal string SSaturdayBackground
        {
            get => SaturdayBackground.ToColorHexString();
            set => SaturdayBackground = Helper.ToBrush(value);
        }

        private Brush saturdayForeground;
        [DataMember(Name = "SaturdayForeground")]
        internal string SSaturdayForeground
        {
            get => Helper.ToColorHexString(SaturdayForeground);
            set => SaturdayForeground = Helper.ToBrush(value);
        }

        private Brush sundayBackground;
        [DataMember(Name = "SundayBackground")]
        internal string SSundayBackground
        {
            get => Helper.ToColorHexString(SundayBackground);
            set => SundayBackground = Helper.ToBrush(value);
        }

        private Brush sundayForeground;
        [DataMember(Name = "SundayForeground")]
        internal string SSundayForeground
        {
            get => Helper.ToColorHexString(SundayForeground);
            set => SundayForeground = Helper.ToBrush(value);
        }

        private Brush specialLevel1Background;
        [DataMember(Name = "SpecialLevel1Background")]
        internal string SSpecialLevel1Background
        {
            get => Helper.ToColorHexString(SpecialLevel1Background);
            set => SpecialLevel1Background = Helper.ToBrush(value);
        }

        private Brush specialLevel1Foreground;
        [DataMember(Name = "SpecialLevel1Foreground")]
        internal string SSpecialLevel1Foreground
        {
            get => Helper.ToColorHexString(SpecialLevel1Foreground);
            set => SpecialLevel1Foreground = Helper.ToBrush(value);
        }

        private Brush specialLevel2Background;
        [DataMember(Name = "SpecialLevel2Background")]
        internal string SSpecialLevel2Background
        {
            get => Helper.ToColorHexString(SpecialLevel2Background);
            set => SpecialLevel2Background = Helper.ToBrush(value);
        }

        private Brush specialLevel2Foreground;
        [DataMember(Name = "SpecialLevel2Foreground")]
        internal string SSpecialLevel2Foreground
        {
            get => Helper.ToColorHexString(SpecialLevel2Foreground);
            set => SpecialLevel2Foreground = Helper.ToBrush(value);
        }

        private Brush specialLevel3Background;
        [DataMember(Name = "SpecialLevel3Background")]
        internal string SSpecialLevel3Background
        {
            get => Helper.ToColorHexString(SpecialLevel3Background);
            set => SpecialLevel3Background = Helper.ToBrush(value);
        }

        private Brush specialLevel3Foreground;
        [DataMember(Name = "SpecialLevel3Foreground")]
        internal string SSpecialLevel3Foreground
        {
            get => Helper.ToColorHexString(SpecialLevel3Foreground);
            set => SpecialLevel3Foreground = Helper.ToBrush(value);
        }

        private Brush grayedOutBackground;
        [DataMember(Name = "GrayedOutBackground")]
        internal string SGrayedOutBackground
        {
            get => Helper.ToColorHexString(GrayedOutBackground);
            set => GrayedOutBackground = Helper.ToBrush(value);
        }

        private Brush grayedOutForeground;
        [DataMember(Name = "GrayedOutForeground")]
        internal string SGrayedOutForeground
        {
            get => Helper.ToColorHexString(GrayedOutForeground);
            set => GrayedOutForeground = Helper.ToBrush(value);
        }

        public Brush Border { get => border; set { if (value != border) { border = value; OnPropertyChanged("Border"); } } }
        public Brush SelectedBorder { get => selectedBorder; set { if (value != selectedBorder) { selectedBorder = value; OnPropertyChanged("SelectedBorder"); } } }
        public Brush Background { get => background; set { if (value != background) { background = value; OnPropertyChanged("Background"); } } }
        public Brush Foreground { get => foreground; set { if (value != foreground) { foreground = value; OnPropertyChanged("Foreground"); } } }
        public Brush NormalBackground { get => normalBackground; set { if (value != normalBackground) { normalBackground = value; OnPropertyChanged("NormalBackground"); } } }
        public Brush MouseOverBackground { get => mouseOverBackground; set { if (value != mouseOverBackground) { mouseOverBackground = value; OnPropertyChanged("MouseOverBackground"); } } }
        public Brush HighlightForeground { get => highlightForeground; set { if (value != highlightForeground) { highlightForeground = value; OnPropertyChanged("HighlightForeground"); } } }
        public Brush SaturdayBackground { get => saturdayBackground; set { if (value != saturdayBackground) { saturdayBackground = value; OnPropertyChanged("SaturdayBackground"); } } }
        public Brush SaturdayForeground { get => saturdayForeground; set { if (value != saturdayForeground) { saturdayForeground = value; OnPropertyChanged("SaturdayForeground"); } } }
        public Brush SundayBackground { get => sundayBackground; set { if (value != sundayBackground) { sundayBackground = value; OnPropertyChanged("SundayBackground"); } } }
        public Brush SundayForeground { get => sundayForeground; set { if (value != sundayForeground) { sundayForeground = value; OnPropertyChanged("SundayForeground"); } } }
        public Brush SpecialLevel1Background { get => specialLevel1Background; set { if (value != specialLevel1Background) { specialLevel1Background = value; OnPropertyChanged("SpecialLevel1Background"); } } }
        public Brush SpecialLevel1Foreground { get => specialLevel1Foreground; set { if (value != specialLevel1Foreground) { specialLevel1Foreground = value; OnPropertyChanged("SpecialLevel1Foreground"); } } }
        public Brush SpecialLevel2Background { get => specialLevel2Background; set { if (value != specialLevel2Background) { specialLevel2Background = value; OnPropertyChanged("SpecialLevel2Background"); } } }
        public Brush SpecialLevel2Foreground { get => specialLevel2Foreground; set { if (value != specialLevel2Foreground) { specialLevel2Foreground = value; OnPropertyChanged("SpecialLevel2Foreground"); } } }
        public Brush SpecialLevel3Background { get => specialLevel3Background; set { if (value != specialLevel3Background) { specialLevel3Background = value; OnPropertyChanged("SpecialLevel3Background"); } } }
        public Brush SpecialLevel3Foreground { get => specialLevel3Foreground; set { if (value != specialLevel3Foreground) { specialLevel3Foreground = value; OnPropertyChanged("SpecialLevel3Foreground"); } } }
        public Brush GrayedOutBackground { get => grayedOutBackground; set { if (value != grayedOutBackground) { grayedOutBackground = value; OnPropertyChanged("GrayedOutBackground"); } } }
        public Brush GrayedOutForeground { get => grayedOutForeground; set { if (value != grayedOutForeground) { grayedOutForeground = value; OnPropertyChanged("GrayedOutForeground"); } } }

        public ThemeColor()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        //public object Clone()
        //    => this.MemberwiseClone();
    }
}
