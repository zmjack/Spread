using System.Text.Json.Serialization;

namespace Spread;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(Range), nameof(Range))]
public interface IRange
{
    ISpreadSheet Sheet { get; }
    Cursor Start { get; }
    Cursor End { get; }

    IEnumerable<ICell> Cells { get; }
    void Merge();
}
