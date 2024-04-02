namespace Spread.Form;

public class SpreadFormOption : ISpreadFormColumn
{
    /// <inheritdoc />
    public required string Type { get; set; }

    /// <inheritdoc />
    public required string DataIndex { get; set; }

    /// <inheritdoc />
    public required string Label { get; set; }

    /// <inheritdoc />
    public SpreadFormRule? Rule { get; set; }

    public SpreadOptionPair[]? Options { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
