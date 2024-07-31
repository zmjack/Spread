using NStandard.Text.Json;

namespace Spread;

public interface ISpreadBookSerializable
{
    SpreadSheet[] Sheets { get; set; }
}

public interface ISpreadBook
{
    SpreadSheet[] Sheets { get; }
    SpreadSheet this[string name] { get; }
}
