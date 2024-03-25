namespace Spread.Form;

public class SpreadFormOption : ISpreadFormColumn
{
    /// <inheritdoc cref="ISpreadFormColumn.Type" />
    public required string Type { get; set; }

    /// <inheritdoc cref="ISpreadFormColumn.DataIndex" />
    public required string DataIndex { get; set; }

    /// <inheritdoc cref="ISpreadFormColumn.Label" />
    public required string Label { get; set; }

    /// <inheritdoc cref="ISpreadFormColumn.Rule" />
    public SpreadFormRule? Rule { get; set; }

    public SpreadOptionPair[]? Options { get; set; }

    public string? OptionsUrl { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
