using NStandard;

namespace Mung.Form;

public class MungFormValue : IMungFormColumn
{
    /// <inheritdoc cref="IMungFormColumn.Type" />
    public required string Type { get; set; }

    /// <inheritdoc cref="IMungFormColumn.DataIndex" />
    public required string DataIndex { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Label" />
    public required string Label { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Rule" />
    public MungFormRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}

public class MungFormOption : IMungFormColumn
{
    /// <inheritdoc cref="IMungFormColumn.Type" />
    public required string Type { get; set; }

    /// <inheritdoc cref="IMungFormColumn.DataIndex" />
    public required string DataIndex { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Label" />
    public required string Label { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Rule" />
    public MungFormRule? Rule { get; set; }

    public KeyValuePair<string, string>[]? Options { get; set; }

    public string? OptionsUrl { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}

public class MungFormImage : IMungFormColumn
{
    /// <inheritdoc cref="IMungFormColumn.Type" />
    public required string Type { get; set; }

    /// <inheritdoc cref="IMungFormColumn.DataIndex" />
    public required string DataIndex { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Label" />
    public required string Label { get; set; }

    public required string Url { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Rule" />
    public MungFormRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}

public class MungFormTableOption
{
    public bool Addible { get; set; }
    public bool Removable { get; set; }
}

public class MungFormTable : IMungFormColumn
{
    /// <inheritdoc cref="IMungFormColumn.Type" />
    public required string Type { get; set; }

    /// <inheritdoc cref="IMungFormColumn.DataIndex" />
    public required string DataIndex { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Label" />
    public required string Label { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Rule" />
    public MungFormRule? Rule { get; set; }

    public MungFormTableOption? Option { get; set; }

    public required IMungFormColumn[] Columns { get; set; }

    public Dictionary<string, KeyValuePair<string, string>[]> ColumnOptions { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
