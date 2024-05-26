namespace Spread;

public class SpreadObject : ISpreadObject
{
    public string[] Headers { get; protected set; }
    public object[] Values { get; protected set; }
}

public sealed class SpreadObject<T> : SpreadObject
{
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
