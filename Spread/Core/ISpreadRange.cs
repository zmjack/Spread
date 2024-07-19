using System.Text.Json.Serialization;

namespace Spread;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(SpreadRange), nameof(SpreadRange))]
public interface ISpreadRange
{
    ISpreadSheet Sheet { get; }
    Cursor Start { get; }
    Cursor End { get; }

    IEnumerable<ICell> Cells { get; }
    void Merge();
}
