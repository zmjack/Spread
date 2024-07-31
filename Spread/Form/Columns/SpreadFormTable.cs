namespace Spread.Form.Columns;

[Flags]
public enum SpreadFormTableSetting
{
    None = 0,
    Addible = 0b01,
    Removable = 0b10,
    All = Addible | Removable,
}

public class SpreadFormTable : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.FormTable;

    /// <inheritdoc />
    public string DataIndex { get; set; }

    /// <inheritdoc />
    public string Label { get; set; }

    /// <inheritdoc />
    public SpreadRule? Rule { get; set; }

    public SpreadFormTableSetting Setting { get; set; }

    public ISpreadColumn[] Columns { get; set; }

    public Dictionary<string, SpreadOptionValue[]> ColumnOptions { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
