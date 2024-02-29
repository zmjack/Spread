namespace Mung.Form;

[Flags]
public enum MungFormTableOption
{
    None = 0,
    Addible = 0b01,
    Removable = 0b10,
    All = Addible | Removable,
}

public class MungFormTable : IMungFormColumn
{
    /// <inheritdoc cref="IMungFormColumn.Type" />
    public required string Type { get; set; }

    /// <inheritdoc cref="IMungFormColumn.DataIndex" />
    public required string DataIndex { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Label" />
    public required string Label { get; set; }

    public required MungFormTableOption Option { get; set; }

    public required IMungFormColumn[] Columns { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Rule" />
    public MungFormRule? Rule { get; set; }

    public Dictionary<string, KeyValuePair<string, string>[]> ColumnOptions { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
