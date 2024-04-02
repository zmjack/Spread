namespace Spread.Form;

public class SpreadFormLabel : ISpreadFormColumn
{
    /// <inheritdoc />
    public required string Type { get; set; }

    /// <inheritdoc />
    public required string DataIndex { get; set; }

    /// <inheritdoc />
    public required string Label { get; set; }

    /// <inheritdoc />
    public SpreadFormRule? Rule { get; set; }

    public required string Content { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
