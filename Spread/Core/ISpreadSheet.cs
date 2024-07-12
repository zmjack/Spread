using NStandard.Text.Json;

namespace Spread;

public interface ISpreadSheetSerializable
{
    object? Source { get; set; }
    string Name { get; set; }
    int RowLength { get; set; }
    int ColumnLength { get; set; }
    IEnumerable<IRow> Rows { get; set; }
}

[JsonImpl<ISpreadSheet, SpreadSheet>]
public interface ISpreadSheet
{
    object? Source { get; set; }
    string Name { get; set; }
    int RowLength { get; set; }
    int ColumnLength { get; set; }
    IEnumerable<IRow> Rows { get; set; }

    IRow this[int row] { get; }
    ICell this[Cursor cursor] { get; }
    ISpreadRange this[Cursor start, Cursor end] { get; }

    void Merge(Cursor start, Cursor end);
}
