using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Spread;

internal static class SpreadHeader
{
    internal static readonly MemoryCache _propertiesCache = new(new MemoryCacheOptions());
    internal static readonly MemoryCache _valuesCache = new(new MemoryCacheOptions());
}

public static class SpreadHeader<T>
{
    public static PropertyInfo[] Properties
    {
        get
        {
            var type = typeof(T);
            return SpreadHeader._propertiesCache.GetOrCreate(type.FullName!, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                return type.GetProperties();
            })!;
        }
    }

    public static string[] Values
    {
        get
        {
            var type = typeof(T);
            return SpreadHeader._valuesCache.GetOrCreate(type.FullName!, entry =>
            {
                return
                (
                    from prop in Properties
                    let display = prop.GetCustomAttribute<DisplayAttribute>()
                    select display is not null ? display.Name : prop.Name
                ).ToArray();
            })!;
        }
    }
}

