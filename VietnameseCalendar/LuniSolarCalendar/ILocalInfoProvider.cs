using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.VietnameseCalendar.Core.LuniSolarCalendar
{
    public abstract class ILocalInfoProvider
    {
        public static ILocalInfoProvider Instance;

        internal static ILocalInfoProvider GetLocalInfoProvider<T>()
        {
            if (typeof(T) == typeof(VietnameseLocalInfoProvider))
                return VietnameseLocalInfoProvider.Instance;
            return null;
        }

        public abstract string[] MonthNames { get; internal set; }
        public abstract string[] StemNames { get; internal set; }
        public abstract string[] BranchNames { get; internal set; }
        public abstract string[] SolarTerms { get; internal set; }
        public abstract float TimeZone { get; internal set; }
    }
}
