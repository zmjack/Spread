using System.Diagnostics;

namespace Spread;

[DebuggerDisplay("{Name}: <{Scope}> {Text}")]
public class Cell : ICell
{
    internal Row _row;

    private readonly ColumnIndex _columnIndex;
    public int RowIndex => _row.Index;
    public int ColIndex => _columnIndex.Value;
    public string Name => $"{_columnIndex.Name}{_row.Name}";

    public Cell()
    {

    }

    public Cell(Row row, int index)
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

    private SpreadStyle? _style;
    public SpreadStyle? Style
    {
        get => _style ?? SpreadStyle.Default;
        set => _style = value;
    }
    public string? Comment { get; set; }

    public string? Scope { get; set; }
    public object? Source { get; set; }
    public object? Value { get; set; }

    public SpreadValue SpreadValue
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
            render.Render(_row._sheet, new(_row.Index, ColIndex));
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
