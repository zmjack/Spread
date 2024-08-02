using NStandard.Text.Json;
using System.Text.Json.Serialization;

namespace Spread;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(SpreadSheet), nameof(SpreadSheet))]
public interface ISpreadSheet
{
    object? Source { get; set; }
    string Name { get; set; }
    int RowLength { get; set; }
    int ColumnLength { get; set; }
    IEnumerable<IRow> Rows { get; set; }

    IRow this[int row] { get; }
    ICell this[Cursor cursor] { get; }
    IRange this[Cursor start, Cursor end] { get; }

    void Merge(Cursor start, Cursor end);
}
