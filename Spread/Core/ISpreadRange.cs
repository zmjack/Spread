using System.Text.Json.Serialization;

namespace Spread;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(SpreadRange), nameof(SpreadRange))]
public interface ISpreadRange
{
    SpreadSheet Sheet { get; }
    Cursor Start { get; }
    Cursor End { get; }

    IEnumerable<Cell> Cells { get; }
    void Merge();
}
