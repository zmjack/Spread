using NStandard.Text.Json;

namespace Spread;

public interface ISpreadObject
{
    public string[] Headers { get; }
    public object[] Values { get; }
}
