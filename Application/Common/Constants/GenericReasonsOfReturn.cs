using System.Collections.Generic;

namespace Application.Common.Constants
{
    public static class GenericReasonsOfReturn
    {
        public static readonly List<string> ReasonsList = new()
        {
            "Booking error",
            "My plans have changed",
            "Other"
        };
    }
}