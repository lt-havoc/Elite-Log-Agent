using System;
using System.Collections.Generic;
using System.Linq;
using DW.ELA.Utility.Extensions;

namespace DW.ELA.Utility;

public static class Functional
{
    public static Exception? ExecuteAndCatch(Action action)
    {
        try
        {
            action();
            return null;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static Exception? ExecuteAndCatch<T>(Action<T> action, T argument)
    {
        try
        {
            action(argument);
            return null;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static void ExecuteManyWithAggregateException<T>(this IEnumerable<T> items, Action<T> function)
    {
        var exceptions = items
            .Select(i => ExecuteAndCatch(function, i))
            .WhereNotNull()
            .ToArray();
        if (exceptions.Length == 1)
            throw exceptions.Single();
        if (exceptions.Length > 1)
            throw new AggregateException(exceptions);
    }
}