using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contest
{
    public static class Converters
    {
        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        public static int ParseInt(string value)
        {
            return Int32.Parse(value, formatProvider);
        }
    }
}
