namespace Spread;

public interface ISpreadBook
{
    ISpreadSheet[] Sheets { get; }
    ISpreadSheet this[string name] { get; }
}
