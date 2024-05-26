namespace Spread.Table;

public interface ISpreadTableTab
{
    string Name { get; set; }

    ISpreadSheet Sheet { get; set; }
}
