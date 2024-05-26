using Spread.Form;

namespace Spread.Table;

public interface ISpreadTable
{
    public object? Source { get; set; }

    public ISpreadColumn[] Columns { get; set; }

    public ISpreadSheet[] Expand { get; set; }
}
