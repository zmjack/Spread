using Spread.Form;

namespace Spread.Table;

public interface ISpreadTable
{
    object? Source { get; set; }
    ISpreadColumn[] Columns { get; set; }
    ISpreadSheet[] Expand { get; set; }
    SpreadPagination Pagination { get; set; }
}
