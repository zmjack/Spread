using System.Text.Json.Serialization;

namespace Spread;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(Row), nameof(Row))]
public interface IRow
{
    int Index { get; }
    string Name { get; }
    IEnumerable<ICell> Cells { get; }

    ICell this[int index] { get; }
}
