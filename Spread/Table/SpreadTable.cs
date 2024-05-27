using Spread.Form;

namespace Spread.Table;

public class SpreadTable(object? source) : ISpreadTable
{
    public object? Source { get; set; } = source;
    public ISpreadColumn[] Columns { get; set; }
    public ISpreadSheet[] Expand { get; set; }
    public required SpreadPagination Pagination { get; set; }
}
