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

    public object? StyledValue
    {
        get
        {
            if (Value is null) return null;
            if (Style is null) return Value;
            if (!Style.Round.HasValue) return Value;

            var round = Style.Round.Value;
            var value = Value;
            if (value is float f) return float.Round(f, round);
            if (value is double d) return double.Round(d, round);
            if (value is decimal dec) return decimal.Round(dec, round);
            return value;
        }
    }

    private const string THOUSAND_FORMAT = "#,##0";

    public string? Format
    {
        get
        {
            if (Style is null) return null;

            var value = Value;
            var thousandSeparator = Style.ThousandSeparator;
            if (thousandSeparator is true)
            {
                var isNumber = false;
                if (value is float f)
                {
                    if (float.IsInteger(f)) return THOUSAND_FORMAT;
                    isNumber = true;
                }
                if (value is double d)
                {
                    if (double.IsInteger(d)) return THOUSAND_FORMAT;
                    isNumber = true;
                }
                if (value is decimal dec)
                {
                    if (decimal.IsInteger(dec)) return THOUSAND_FORMAT;
                    isNumber = true;
                }

                if (isNumber)
                {
                    if (!Style.Round.HasValue) return THOUSAND_FORMAT;

                    var round = Style.Round.Value;
                    if (round == 0) return THOUSAND_FORMAT;

                    return $"#,##0.{"#".Repeat(round)}";
                }
            }

            return Style.Format;
        }
    }

    public string? Text
    {
        get
        {
            var value = StyledValue;
            if (value is null) return null;

            var format = Format;
            if (format is null) return value.ToString();

            return value switch
            {
                short v => v.ToString(format),
                int v => v.ToString(format),
                long v => v.ToString(format),
                ushort v => v.ToString(format),
                uint v => v.ToString(format),
                ulong v => v.ToString(format),
                DateTime v => v.ToString(format),
                float v => v.ToString(format),
                double v => v.ToString(format),
                decimal v => v.ToString(format),
                _ => value.ToString(),
            };
        }
    }

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

}
