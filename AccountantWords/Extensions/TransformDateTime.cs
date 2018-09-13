using System;
using System.Globalization;

namespace AccountantWords.Extensions
{
    public static class TransformDateTime
    {
        public static DateTime StringToDateTime(this string date)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var format = DateTimeFormatInfo.CurrentInfo.GetAllDateTimePatterns('R')[0];
            DateTime dt = DateTime.ParseExact(date.Replace("+0000", "GMT"), format, provider);
            return dt;
        }
    }
}
