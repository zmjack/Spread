using System.Text.Json.Serialization;

namespace Spread.Form;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(SpreadRule), nameof(SpreadRule))]
public interface ISpreadRule
{
    public bool Required { get; set; }
}
