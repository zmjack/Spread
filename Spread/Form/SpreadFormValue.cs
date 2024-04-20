namespace Spread.Form;

public class SpreadFormValue : ISpreadFormColumn
{
    /// <inheritdoc />
    public required string Type { get; set; }

    /// <inheritdoc />
    public required string DataIndex { get; set; }

    /// <inheritdoc />
    public required string Label { get; set; }

    /// <inheritdoc />
    public ISpreadFormRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
