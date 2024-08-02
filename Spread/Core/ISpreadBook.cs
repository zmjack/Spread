using NStandard.Text.Json;

namespace Spread;

public interface ISpreadBook
{
    ISpreadSheet[] Sheets { get; set; }
    ISpreadSheet this[string name] { get; }
}
