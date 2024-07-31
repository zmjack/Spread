using Spread.Form;

namespace Spread.Table;

public interface ISpreadTable
{
    object? Source { get; set; }
    ISpreadColumn[] Columns { get; set; }
    SpreadSheet[] Expand { get; set; }
    SpreadPagination Pagination { get; set; }
}
