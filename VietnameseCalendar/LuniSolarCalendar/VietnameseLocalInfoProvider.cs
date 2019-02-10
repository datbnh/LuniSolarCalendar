namespace Augustine.VietnameseCalendar.Core.LuniSolarCalendar
{
    public class VietnameseLocalInfoProvider : ILocalInfoProvider
    {
        public override string[] MonthNames { get => _monthNames; internal set => _monthNames = value; }
        public override string[] StemNames { get => _stemNames; internal set => _stemNames = value; }
        public override string[] BranchNames { get => _branchNames; internal set => _branchNames = value; }
        public override string[] SolarTerms { get => _solarTerms; internal set => _solarTerms = value; }
        public override float TimeZone { get => _timeZone; internal set => _timeZone = value; }

        private string[] _monthNames;
        private string[] _stemNames;
        private string[] _branchNames;
        private string[] _solarTerms;
        private float _timeZone;

        //public override float TimeZone = 7.0f;
        //public override string[] MonthNames
        //    = new string[]{"Giêng", "Hai", "Ba", "Tư", "Năm", "Sáu",
        //        "Bảy", "Tám", "Chín", "Mười", "Một", "Chạp"};
        //public override string[] StemNames
        //    = new string[]{"Giáp", "Ất", "Bính", "Đinh", "Mậu",
        //        "Kỷ", "Canh", "Tân", "Nhâm", "Quý"};
        //public override string[] BranchNames
        //    = new string[]{"Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ",
        //     "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi"};
        //public override string[] SolarTerms
        //    = new string[]{"Xuân Phân", "Thanh Minh",
        //        "Cốc Vũ", "Lập Hạ",
        //        "Tiểu Mãn", "Mang Chủng",
        //        "Hạ Chí", "Tiểu Thử",
        //        "Đại Thử", "Lập Thu",
        //        "Xử Thử", "Bạch Lộ",
        //        "Thu Phân", "Hàn Lộ",
        //        "Sương Giáng", "Lập Đông",
        //        "Tiểu Tuyết", "Đại Tuyết",
        //        "Đông Chí", "Tiểu Hàn",
        //        "Đại Hàn", "Lập Xuân",
        //        "Vũ Thủy", "Kinh Trập",
        //    };
        private static ILocalInfoProvider _instance;
        public static ILocalInfoProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VietnameseLocalInfoProvider();
                return _instance;
            }
        }

        private VietnameseLocalInfoProvider()
        {
            TimeZone = 7f;
            MonthNames = new string[]{"Giêng", "Hai", "Ba", "Tư", "Năm", "Sáu",
                "Bảy", "Tám", "Chín", "Mười", "Một", "Chạp"};
            StemNames = new string[]{"Giáp", "Ất", "Bính", "Đinh", "Mậu",
                "Kỷ", "Canh", "Tân", "Nhâm", "Quý"};
            BranchNames = new string[]{"Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ",
             "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi"};
            SolarTerms = new string[]{"Xuân Phân", "Thanh Minh",
                "Cốc Vũ", "Lập Hạ",
                "Tiểu Mãn", "Mang Chủng",
                "Hạ Chí", "Tiểu Thử",
                "Đại Thử", "Lập Thu",
                "Xử Thử", "Bạch Lộ",
                "Thu Phân", "Hàn Lộ",
                "Sương Giáng", "Lập Đông",
                "Tiểu Tuyết", "Đại Tuyết",
                "Đông Chí", "Tiểu Hàn",
                "Đại Hàn", "Lập Xuân",
                "Vũ Thủy", "Kinh Trập",
            };
        }
    }
}
