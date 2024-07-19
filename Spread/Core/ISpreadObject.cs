using System.Text.Json.Serialization;

namespace Spread;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(SpreadObject), nameof(SpreadObject))]
public interface ISpreadObject
{
    public string[] Headers { get; }
    public object[] Values { get; }
}
