using System.Collections.Generic;
using System.Linq;

namespace DW.ELA.Utility.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> items) where T : class =>
        items.Where(x => x is not null)!;
}