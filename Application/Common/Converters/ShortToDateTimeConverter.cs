using System;

namespace Application.Common.Converters
{
    internal static class ShortToDateTimeConverter
    {
        internal static DateTime Convert(short source)
        {
            var dateNow = DateTime.Now;
            var hour = source / 60;
            var minute = source % 60;
            return new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, hour, minute, 0);
        }
    }
}