using System;

namespace Application.Common.Converters
{
    internal static class DateTimeToShortConverter
    {
        internal static short Convert(DateTime source)
        {
            var timeInMinutes = (source.Hour * 60 + source.Minute);
            return (short)timeInMinutes;
        }
    }
}