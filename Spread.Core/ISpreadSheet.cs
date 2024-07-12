using NStandard.Text.Json;

namespace Spread;

public interface ISpreadSheetSerializable
{
    object? Source { get; }
    string Name { get; }
    int RowLength { get; }
    int ColumnLength { get; }
    IEnumerable<IRow> Rows { get; }
}

[JsonImpl<ISpreadSheet, SpreadSheet>]
public interface ISpreadSheet
{
    object? Source { get; }
    string Name { get; }
    int RowLength { get; }
    int ColumnLength { get; }
    IEnumerable<IRow> Rows { get; }

    IRow this[int row] { get; }
    ICell this[Cursor cursor] { get; }
    ISpreadRange this[Cursor start, Cursor end] { get; }

    void Merge(Cursor start, Cursor end);
}
