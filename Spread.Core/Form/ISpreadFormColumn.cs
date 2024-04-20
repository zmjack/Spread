#if NET7_0_OR_GREATER
using NStandard.Text.Json;

namespace Spread.Form;

[JsonImplConverter<ISpreadFormColumn>]
public interface ISpreadFormColumn
{
    /// <summary>
    /// See <see cref="UIComponent" />.
    /// </summary>
    string Type { get; set; }
    string DataIndex { get; set; }
    string Label { get; set; }
    ISpreadFormRule? Rule { get; set; }
}
#endif
