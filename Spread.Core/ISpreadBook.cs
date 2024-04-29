namespace Spread;

public interface ISpreadBook
{
    IReadOnlyDictionary<string, ISpreadSheet> Sheets { get; }
}
