using NStandard.Text.Json;

namespace Spread.Form;

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
