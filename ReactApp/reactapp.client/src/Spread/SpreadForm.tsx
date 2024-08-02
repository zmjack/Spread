/* eslint-disable @typescript-eslint/no-explicit-any */
import * as React from 'react';
import { useState, useEffect, useImperativeHandle } from 'react';
import lodash from 'lodash';
import moment from 'moment';
import SpreadSheet from './SpreadSheet';
import { Button, DatePicker, Input, InputNumber, Row, Select } from 'antd';
import { DeleteOutlined } from '@ant-design/icons';
import ImageShower from './ImageShower';

type IRule = {
    required: boolean,
};
type IColumn = {
    type: string,
    dataIndex: string,
    label: string,
    rule?: IRule,
};
enum SpreadFormTableSetting {
    None = 0,
    Addible = 1,
    Removable = 2,
    All = 3
}

const SpreadForm = React.forwardRef((props: {
    fullWidth?: boolean,
    sheet?: API.ISpreadSheet,
    readonly?: boolean,
}, ref) => {
    const readonly = props.readonly ?? false;
    const [source, setSource] = useState<any>();

    const normalize = (obj: any) => {
        if (obj == undefined) return undefined;

        const keys = ['dataIndex', 'options'];

        let item = {};
        for (let key in obj) {
            if (keys.indexOf(key) > -1) continue;

            const value = obj[key];
            if (typeof value == 'object') {
                if (value instanceof Date) (item as any)[key] = moment(value).format('YYYY-MM-DD');
                else if (value instanceof Array) {
                    (item as any)[key] = [];
                    for (let element of value) {
                        (item as any)[key].push(normalize(element));
                    }
                }
                else (item as any)[key] = normalize(value);
            }
            else {
                (item as any)[key] = value;
            }
        }
        return item;
    }

    useImperativeHandle(ref, () => ({
        getModel: () => normalize(source),
    }));

    useEffect(() => {
        if (props.sheet?.source) {
            setSource(props.sheet?.source);
        } else setSource(undefined);
    }, [props.sheet]);

    const refresh = () => {
        setSource({ ...(source as any) });
    }

    const Labeled = (props: {
        label: string,
        newline?: boolean,
        rule?: IRule,
        children: JSX.Element
    }) => {
        if (props.newline) {
            return (
                <>
                    <Row>
                        <span
                            style={{
                                width: 120,
                                marginLeft: 24
                            }}
                        >
                            {
                                props.rule?.required ? (
                                    <span
                                        style={{
                                            display: 'inline-block',
                                            marginRight: '4px',
                                            color: '#ff4d4f',
                                        }}
                                    >
                                        *
                                    </span>
                                ) : undefined
                            }
                            {props.label}
                        </span>
                    </Row>
                    <div>{props.children}</div>
                </>
            );
        }
        else {
            return (
                <Row>
                    <span
                        style={{
                            width: 120,
                            marginLeft: 24
                        }}
                    >
                        {
                            props.rule?.required ? (
                                <span
                                    style={{
                                        display: 'inline-block',
                                        marginRight: '4px',
                                        color: '#ff4d4f',
                                    }}
                                >
                                    *
                                </span>
                            ) : undefined
                        }
                        {props.label}
                    </span>
                    <span>
                        {props.children}
                    </span>
                </Row>
            );
        }
    }

    const LabelCell = (props: {
        value: IColumn & { content: string },
    }) => {
        return (
            <span>{props.value.content}</span>
        );
    };

    const HideCell = (props: {
        value: IColumn,
    }) => {
        const value = lodash.get(source, props.value.dataIndex);
        return (
            <Input
                style={{ display: 'none' }}
                defaultValue={value}
            />
        );
    };

    const TextCell = (props: {
        value: IColumn,
        labeled?: boolean,
    }) => {
        const value = lodash.get(source, props.value.dataIndex);
        const componet = readonly
            ? <span> {value} </span>
            : (
                <Input
                    defaultValue={value}
                    onChange={e => {
                        lodash.set(source, props.value.dataIndex, e.target.value);
                    }}
                />
            );

        if (props.labeled) {
            return (
                <Labeled label={props.value.label} rule={props.value.rule}>
                    {componet}
                </Labeled>
            );
        } else {
            return componet;
        }
    };

    const NumberCell = (props: {
        value: IColumn,
        labeled?: boolean,
    }) => {
        const value = lodash.get(source, props.value.dataIndex);
        const componet = readonly
            ? <span> {value} </span>
            : (
                <InputNumber
                    defaultValue={value}
                    style={{ width: 183 }}
                    onChange={value => {
                        lodash.set(source, props.value.dataIndex, value);
                    }}
                />
            );

        if (props.labeled) {
            return (
                <Labeled label={props.value.label} rule={props.value.rule}>
                    {componet}
                </Labeled>
            );
        } else {
            return componet;
        }
    };

    const PercentCell = (props: {
        value: IColumn,
        labeled?: boolean,
    }) => {
        const value = lodash.get(source, props.value.dataIndex) * 100;
        const componet = readonly
            ? <span> {value} </span>
            : (
                <InputNumber
                    defaultValue={value}
                    min={0}
                    max={100}
                    style={{ width: 183 }}
                    addonAfter="%"
                    onChange={value => {
                        value = (value ?? 0) / 100;
                        lodash.set(source, props.value.dataIndex, value);
                    }}
                />
            );

        if (props.labeled) {
            return (
                <Labeled label={props.value.label} rule={props.value.rule}>
                    {componet}
                </Labeled>
            );
        } else {
            return componet;
        }
    };

    const DateCell = (props: {
        value: IColumn,
        labeled?: boolean,
    }) => {
        const value = lodash.get(source, props.value.dataIndex);
        const componet = readonly
            ? <span> {value} </span>
            : (
                <DatePicker
                    defaultValue={value ? moment(value) : undefined}
                    style={{ width: 183 }}
                    onChange={e => {
                        lodash.set(source, props.value.dataIndex, e?.toDate());
                    }}
                />
            );

        if (props.labeled) {
            return (
                <Labeled label={props.value.label} rule={props.value.rule}>
                    {componet}
                </Labeled>
            );
        } else {
            return componet;
        }
    };

    const OptionCell = (props: {
        value: IColumn & {
            options?: { key: string, value: string }[]
        },
        labeled?: boolean,
    }) => {
        const value = lodash.get(source, props.value.dataIndex);

        let options = [] as { value: string, label: string }[];
        if (props.value.options) {
            options = [{
                value: '00000000-0000-0000-0000-000000000000',
                label: '<请选择>',
            }, ...props.value.options.map(x => {
                return {
                    value: x.key,
                    label: x.value,
                };
            })]
        }

        const componet = readonly
            ? <span> {options.filter(x => x.value == value)[0]?.label} </span>
            : (
                <Select
                    showSearch
                    style={{ width: 183 }}
                    optionFilterProp="children"
                    filterOption={(input, option) => (option?.label ?? '').includes(input)}
                    filterSort={(optionA, optionB) =>
                        (optionA?.label ?? '').toLowerCase().localeCompare((optionB?.label ?? '').toLowerCase())
                    }
                    defaultValue={value}
                    options={options}
                    onChange={value => {
                        lodash.set(source, props.value.dataIndex, value);
                    }}
                />
            );

        if (props.labeled) {
            return (
                <Labeled label={props.value.label} rule={props.value.rule}>
                    {componet}
                </Labeled>
            );
        } else {
            return componet;
        }
    }

    const ImageCell = (props: {
        value: IColumn & { url?: string },
        labeled?: boolean,
    }) => {
        const value = lodash.get(source, props.value.dataIndex);
        const url = value ? props.value.url?.replace('{value}', value) : undefined;

        const componet = readonly
            ? <span> {value} </span>
            : (
                <ImageShower
                    type='test'
                    url={url}
                    onChange={resp => {
                        const value = resp;
                        lodash.set(source, props.value.dataIndex, value);
                    }}
                />
            );

        if (props.labeled) {
            return (
                <Labeled label={props.value.label} rule={props.value.rule}>
                    {componet}
                </Labeled>
            );
        } else {
            return componet;
        }
    };

    const TableCell = (props: {
        value: IColumn & {
            setting: SpreadFormTableSetting,
            columns: {
                type: string,
                dataIndex: string,
                label: string,
                options?: { key: string, value: string }[],
            }[],
        },
        labeled?: boolean,
    }) => {
        let rows = lodash.get(source, props.value.dataIndex) as any[];
        if (!rows) {
            lodash.set(source, props.value.dataIndex, []);
            rows = lodash.get(source, props.value.dataIndex);
        }
        const [columns] = useState<any[]>(props.value?.columns ?? []);

        const componet = (
            <div className="ant-table ant-table-small">
                <table style={{
                    width: '100%',
                    marginTop: 24,
                }}>
                    <thead className="ant-table-thead">
                        <tr>
                            {
                                props.value.setting & SpreadFormTableSetting.Removable ? (
                                    <th style={{ width: 64 }}> </th>
                                ) : undefined
                            }
                            <th style={{ display: 'none' }}></th>
                            {
                                columns
                                    .filter(x => x.type != 'hide')
                                    .map(x => (
                                        <th key={x.dataIndex}>
                                            {x.label}
                                        </th>
                                    ))
                            }
                        </tr>
                    </thead>
                    <tbody className="ant-table-tbody">
                        {
                            rows.map((row, i) => {
                                let index = `${props.value.dataIndex}[${i}]`
                                row.dataIndex = index;

                                return (
                                    <tr key={i}>
                                        {
                                            props.value.setting & SpreadFormTableSetting.Removable ? (
                                                <td>
                                                    <Button
                                                        onClick={() => {
                                                            lodash.set(source, props.value.dataIndex, rows.filter(x => x.dataIndex != index));
                                                            refresh();
                                                        }}
                                                    >
                                                        <DeleteOutlined />
                                                    </Button>
                                                </td>
                                            ) : undefined
                                        }
                                        {
                                            columns
                                                .filter(x => x.type == 'hide')
                                                .map(x => {
                                                    let options = x.options;
                                                    return (
                                                        <td key={x.dataIndex} style={{ display: 'none' }}>
                                                            <AutoCell
                                                                value={{
                                                                    ...x,
                                                                    dataIndex: `${index}.${x.dataIndex}`,
                                                                    ...(options ? { options } : {})
                                                                }}
                                                            />
                                                        </td>
                                                    );
                                                })
                                        }
                                        {
                                            columns
                                                .filter(x => x.type != 'hide')
                                                .map(x => {
                                                    let options = x.options;
                                                    return (
                                                        <td key={x.dataIndex}>
                                                            <AutoCell
                                                                value={{
                                                                    ...x,
                                                                    dataIndex: `${index}.${x.dataIndex}`,
                                                                    ...(options ? { options } : {})
                                                                }}
                                                            />
                                                        </td>
                                                    );
                                                })
                                        }
                                    </tr>
                                );
                            })
                        }
                        {
                            props.value.setting & SpreadFormTableSetting.Addible ? (
                                <tr>
                                    <td colSpan={columns.length + 1}>
                                        <Button
                                            block
                                            size='small'
                                            type='dashed'
                                            onClick={() => {
                                                const item = {};
                                                for (let column of columns) {
                                                    (item as any)[column.dataIndex] = undefined;
                                                }
                                                rows.push(item);
                                                refresh();
                                            }}
                                        >
                                            + 增加行
                                        </Button>
                                    </td>
                                </tr>
                            ) : undefined
                        }
                    </tbody>
                </table>
            </div>
        );

        if (props.labeled) {
            return (
                <Labeled label={props.value.label} rule={props.value.rule} newline>
                    {componet}
                </Labeled>
            );
        } else {
            return componet;
        }
    };

    const AutoCell = (props: {
        value: IColumn,
        labeled?: boolean,
    }) => {
        if (props.value.type == 'label') {
            return <LabelCell value={props.value as any} />
        }
        else if (props.value.type == 'hide') {
            return <HideCell value={props.value} />
        }
        else if (props.value.type == 'text') {
            return <TextCell value={props.value} labeled={props.labeled} />;
        }
        else if (props.value.type == 'number') {
            return <NumberCell value={props.value} labeled={props.labeled} />;
        }
        else if (props.value.type == 'percent') {
            return <PercentCell value={props.value} labeled={props.labeled} />;
        }
        else if (props.value.type == 'date') {
            return <DateCell value={props.value} labeled={props.labeled} />;
        }
        else if (props.value.type == 'option') {
            return <OptionCell value={props.value} labeled={props.labeled} />;
        }
        else if (props.value.type == 'image') {
            return <ImageCell value={props.value} labeled={props.labeled} />;
        }
        else if (props.value.type == 'form-table') {
            return <TableCell value={props.value as any} labeled={props.labeled} />;
        }
        else return <span> faild. </span>;
    };

    return <>
        {
            source ? (
                <SpreadSheet
                    fullWidth={props.fullWidth}
                    sheet={props?.sheet}
                    scopes={{
                        [":input"]: cell => {
                            return <AutoCell value={cell.value} labeled />
                        },
                    }}
                />
            ) : undefined
        }
    </>;
});
export default SpreadForm;
