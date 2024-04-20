namespace Spread.Form;

[Flags]
public enum SpreadFormTableOption
{
    None = 0,
    Addible = 0b01,
    Removable = 0b10,
    All = Addible | Removable,
}

public class SpreadFormTable : ISpreadFormColumn
{
    /// <inheritdoc />
    public required string Type { get; set; }

    /// <inheritdoc />
    public required string DataIndex { get; set; }

    /// <inheritdoc />
    public required string Label { get; set; }

    public required SpreadFormTableOption Option { get; set; }

    public required ISpreadFormColumn[] Columns { get; set; }

    /// <inheritdoc />
    public ISpreadFormRule? Rule { get; set; }

    public Dictionary<string, SpreadOptionPair[]> ColumnOptions { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
