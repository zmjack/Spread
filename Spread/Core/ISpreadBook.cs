using NStandard.Text.Json;

namespace Spread;

public interface ISpreadBook
{
    SpreadSheet[] Sheets { get; }
    SpreadSheet this[string name] { get; }
}
