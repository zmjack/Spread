namespace Spread.Form;

public struct SpreadOptionPair(object key, string value)
{
    public object Key { get; set; } = key;
    public string Value { get; set; } = value;
}
