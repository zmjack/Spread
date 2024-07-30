using NStandard.Text.Json;

namespace Spread;

public interface ISpreadSheetSerializable
{
    object? Source { get; set; }
    string Name { get; set; }
    int RowLength { get; set; }
    int ColumnLength { get; set; }
    IEnumerable<Row> Rows { get; set; }
}

[JsonImpl<ISpreadSheet, SpreadSheet>]
public interface ISpreadSheet
{
    object? Source { get; set; }
    string Name { get; set; }
    int RowLength { get; set; }
    int ColumnLength { get; set; }
    IEnumerable<Row> Rows { get; set; }

    Row this[int row] { get; }
    Cell this[Cursor cursor] { get; }
    SpreadRange this[Cursor start, Cursor end] { get; }

    void Merge(Cursor start, Cursor end);
}
