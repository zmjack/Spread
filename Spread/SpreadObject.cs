namespace Spread;

public static class SpreadObject
{
    public static SpreadObject<T> Create<T>(T value)
    {
        return new SpreadObject<T>(value);
    }
}

public sealed class SpreadObject<T> : ISpreadObject
{
    public string[] Headers { get; }
    public object[] Values { get; }

    public SpreadObject(T obj)
    {
        Headers = SpreadHeader<T>.Values;
        Values =
        [
            ..
            from prop in SpreadHeader<T>.Properties
            select prop.GetValue(obj)
        ];
    }
}
