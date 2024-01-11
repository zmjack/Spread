using System.Diagnostics;

namespace Mung;

public interface ICoCell
{
    int RowIndex { get; }
    int ColIndex { get; }
    string Name { get; }
    bool Merged { get; }

    bool Ignored { get; set; }
    int RowSpan { get; set; }
    int RowOffset { get; set; }
    int ColSpan { get; set; }
    int ColOffset { get; set; }

    MungStyle? Style { get; set; }
    string? Comment { get; set; }

    public string? Scope { get; set; }
    public object? Source { get; set; }
    object? Value { get; set; }
    MungValue CoValue { set; }
    string? Text { get; }
    Grid Grid { set; }
}

[DebuggerDisplay("{Name}: <{Scope}> {Text}")]
public class Cell : ICoCell
{
    internal Row _row;

    private readonly ColumnIndex _columnIndex;
    public int RowIndex => _row.Index;
    public int ColIndex => _columnIndex.Value;
    public string Name => $"{_columnIndex.Name}{_row.Name}";

    internal Cell(Row row, int index)
    {
        _row = row;
        _columnIndex = new(index);
    }

    public bool Merged
    {
        get
        {
            if (Ignored) return true;
            else if (RowSpan > 1 || ColSpan > 1) return true;
            else return false;
        }
    }
    public bool Ignored { get; set; }
    public int RowSpan { get; set; } = 1;
    public int RowOffset { get; set; }
    public int ColSpan { get; set; } = 1;
    public int ColOffset { get; set; }

    private MungStyle? _style;
    public MungStyle? Style
    {
        get => _style ?? MungStyle.Default;
        set => _style = value;
    }
    public string? Comment { get; set; }

    public string? Scope { get; set; }
    public object? Source { get; set; }
    public object? Value { get; set; }

    public MungValue CoValue
    {
        set
        {
            Source = value.Source;
            Value = value.Value;
            Comment = value.Comment;
        }
    }

    public Grid Grid
    {
        set
        {
            var render = new GridRender(value);
            render.Render(_row._sheet, (_row.Index, ColIndex));
        }
    }

    public string? Text
    {
        get
        {
            if (Value is null) return null;
            if (Style is not null) return Style.FormatValue(Value);

            return Value.ToString();
        }
    }

}
