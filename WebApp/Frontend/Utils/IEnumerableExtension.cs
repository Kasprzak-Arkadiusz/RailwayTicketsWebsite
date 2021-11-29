using System.Collections.Generic;
using System.Linq;

namespace WebApp.Frontend.Utils
{
    public static class IEnumerableExtension
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }
}
