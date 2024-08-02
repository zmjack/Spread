/* eslint-disable @typescript-eslint/no-explicit-any */

type ISpreadBook = {
    sheets?: any[][];
};

type ISpreadColumn = {
    type?: string;
    dataIndex?: string;
    label?: string;
    rule?: SpreadRule;
};

type ISpreadSheet = {
    source?: any;
    name?: string;
    rowLength?: number;
    columnLength?: number;
    rows?: Cell[][];
};

type ISpreadTable = {
    source?: any;
    columns?: ISpreadColumn[];
    expand?: any[][];
    pagination?: SpreadPagination;
};
