namespace Spread.Form;

public class SpreadFormImage : ISpreadFormColumn
{
    /// <inheritdoc />
    public required string Type { get; set; }

    /// <inheritdoc />
    public required string DataIndex { get; set; }

    /// <inheritdoc />
    public required string Label { get; set; }

    public required string Url { get; set; }

    /// <inheritdoc />
    public SpreadFormRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
