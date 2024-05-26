namespace Spread.Form;

public struct SpreadOptionValue(object key, string value)
{
    public object Key { get; set; } = key;
    public string Value { get; set; } = value;
}
