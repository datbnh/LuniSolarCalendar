using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Augustine.VietnameseCalendar.Core;
using System.Windows;

namespace Augustine.VietnameseCalendar.UI
{
    public class CalendarDayToolTipModel : INotifyPropertyChanged
    {
        public CalendarDayToolTipModel(LunarDate lunarDate, string title = "", string decorativeText = "")
        {
            LunarDate = lunarDate;
            Title = title;
            DecorativeText = decorativeText;//"♥";
        }

        private LunarDate lunarDate;
        public LunarDate LunarDate
        {
            get => lunarDate;
            set
            {
                if (value != lunarDate)
                {
                    lunarDate = value;
                    OnPropertyChanged("LunarDate");
                    OnPropertyChanged("SolarDate");
                    OnPropertyChanged("DayOfWeek");
                    OnPropertyChanged("LunarDateString"); // xx tháng Giêng năm xxxx
                }
            }
        }

        public DateTime SolarDate { get => lunarDate.SolarDate; }
        public string DayOfWeek { get => SolarDate.DayOfWeek.ToString(); }
        public string LunarDateString {
            get => string.Format("Ngày {0} tháng {1} năm {2}", 
                lunarDate.Day, lunarDate.MonthShortName, lunarDate.Year);
        }

        private string decorativeText;
        public string DecorativeText
        {
            get => decorativeText;
            set
            {
                if (value != decorativeText)
                {
                    decorativeText = value;
                    OnPropertyChanged("DecorativeText");
                }
            }
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged("Title");
                    OnPropertyChanged("TitleBlockVisibility");
                }
            }
        }

        public Visibility TitleBlockVisibility
        {
            get => string.IsNullOrEmpty(title) ? Visibility.Collapsed : Visibility.Visible;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

}
