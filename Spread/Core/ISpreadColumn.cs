using NStandard.Text.Json;
using Spread.Form.Columns;
using System.Text.Json.Serialization;

namespace Spread.Form;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
[JsonDerivedType(typeof(SpreadDate), nameof(SpreadDate))]
[JsonDerivedType(typeof(SpreadDateTime), nameof(SpreadDateTime))]
[JsonDerivedType(typeof(SpreadFormTable), nameof(SpreadFormTable))]
[JsonDerivedType(typeof(SpreadHide), nameof(SpreadHide))]
[JsonDerivedType(typeof(SpreadImage), nameof(SpreadImage))]
[JsonDerivedType(typeof(SpreadLabel), nameof(SpreadLabel))]
[JsonDerivedType(typeof(SpreadNumber), nameof(SpreadNumber))]
[JsonDerivedType(typeof(SpreadOption), nameof(SpreadOption))]
[JsonDerivedType(typeof(SpreadPercent), nameof(SpreadPercent))]
[JsonDerivedType(typeof(SpreadText), nameof(SpreadText))]
public interface ISpreadColumn
{
    /// <summary>
    /// See <see cref="UIComponent" />.
    /// </summary>
    string Type { get; }
    string DataIndex { get; set; }
    string Label { get; set; }
    ISpreadRule? Rule { get; set; }
}
