/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ActionType } from '@ant-design/pro-components';
import {
    FooterToolbar,
} from '@ant-design/pro-components';
import { Table, Button, Modal, Popconfirm, Space, message, Tabs, Card, Row, Col } from 'antd';
import React, { ReactNode, useEffect, useRef, useState } from 'react';
import { uniqueId } from 'lodash';
import SpreadForm from './SpreadForm';
import { PlusOutlined } from '@ant-design/icons';

interface Props<T> {
    title?: string;

    readRequest: (params?: any) => Promise<API.ISpreadTable>;
    createRequest?: (record: T) => Promise<API.JSend>;
    updateRequest?: (record: T) => Promise<API.JSend>;
    deleteRequest?: (params?: any) => Promise<API.JSend>;

    searchFormRequest?: () => Promise<API.ISpreadSheet>;
    createFormRequest?: () => Promise<API.ISpreadSheet>;
    updateFormRequest?: (params?: any) => Promise<API.ISpreadSheet>;

    toolBarRender?: () => ReactNode;

    actions?: { label: string, action: (record: T) => void }[];
}

export default <T extends object>(props: React.PropsWithChildren<Props<T>>) => {
    const actionRef = useRef<ActionType>();

    const [current, setCurrent] = useState(1);
    const [pageSize, setPageSize] = useState(20);
    const [total, setTotal] = useState(0);

    const [mode, setMode] = useState<'create' | 'update'>('create');
    const [addOrUpdateModal, setAddOrUpdateModal] = useState<boolean>(false);
    const [addOrUpdateForm, setAddOrUpdateForm] = useState<API.ISpreadSheet>();
    const addOrUpdateFormRef = useRef();

    const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

    const [columns, setColumns] = useState<any>([]);
    const [data, setData] = useState<any[]>([]);
    const [expand, setExpand] = useState<API.ISpreadSheet[]>([]);

    const [searchForm, setSearchForm] = useState<API.ISpreadSheet>();

    const onSelectChange = (keys: React.Key[]) => {
        setSelectedRowKeys(keys);
    };

    const rowSelection = {
        selectedRowKeys,
        onChange: onSelectChange,
    };

    const getSearchParams = () => {
        let dict: any = {};
        for (let key in searchForm?.source) {
            const value = searchForm!.source[key];
            if (value) {
                dict[key] = value;
            }
        }
        return dict;
    };

    const refresh = async () => {
        const table = await props.readRequest({
            "current": current,
            "pageSize": pageSize,
            ...getSearchParams()
        });

        setColumns([
            ...(table.columns!
                .filter(c => c.type != 'hide')
                .map(c => {
                    if (c.type == 'option') {
                        return {
                            dataIndex: c.dataIndex,
                            key: c.dataIndex,
                            title: c.label,
                            render: (value: any) => {
                                return ((c as any)['options'] as { key: any, value: string }[]).filter(x => x.key == value)[0]?.value;
                            }
                        };
                    }
                    else {
                        return {
                            dataIndex: c.dataIndex,
                            key: c.dataIndex,
                            title: c.label,
                        };
                    }
                })
            ),

            {
                title: '操作',
                valueType: 'option',
                render: (_: any, record: any) => (
                    <Space>
                        {
                            props.updateFormRequest ? (
                                <a
                                    key={uniqueId()}
                                    onClick={e => {
                                        e.stopPropagation();
                                        props
                                            .updateFormRequest!({
                                                id: record.id
                                            })
                                            .then(form => {
                                                setMode('update');
                                                setAddOrUpdateForm(form);
                                            })
                                            .then(() => {
                                                setAddOrUpdateModal(true);
                                            });
                                    }}
                                >
                                    编辑
                                </a>
                            ) : undefined
                        }

                        {
                            props.actions?.map(action => {
                                return (
                                    <a
                                        key={uniqueId()}
                                        onClick={() => {
                                            action.action?.(record);
                                        }}
                                    >
                                        {action.label}
                                    </a>
                                );
                            })
                        }
                    </Space>
                ),
            }] as any
        );

        setTotal(table.pagination?.total!)
        setData(table.source);
        setExpand(table.expand!);
    };

    const handleRemove = async (keys: React.Key[]) => {
        const hide = message.loading('正在删除');
        if (!keys) return true;
        try {
            await props.deleteRequest?.({ ids: keys });
            hide();
            message.success('已删除');
            refresh();
            return true;
        } catch (error) {
            hide();
            console.log(error);
            message.error('仅可删除自己创建的记录。');
            return false;
        }
    };

    useEffect(() => {
        refresh();

        props.searchFormRequest?.()
            .then(sheet => {
                setSearchForm(sheet);
            });
    }, []);

    useEffect(() => {
        refresh();
    }, [current, pageSize]);

    return (
        <>
            <Card style={{ marginBottom: 24 }}>
                <SpreadForm sheet={searchForm} />

                <div style={{ float: 'right' }}>
                    <Button type="primary" onClick={() => {
                        refresh();
                        setCurrent(1);
                    }}>
                        查询
                    </Button>
                </div>
            </Card>

            <Card>
                <Row>
                    <Col span={24}>
                        <div style={{ float: 'right', marginBottom: 12 }}>
                            {[
                                props.toolBarRender?.() ?? undefined,
                                props.createFormRequest ? (
                                    <Button
                                        type="primary"
                                        key="primary"
                                        onClick={() => {
                                            props
                                                .createFormRequest!()
                                                .then(form => {
                                                    setAddOrUpdateForm(form);
                                                    setMode('create');
                                                    setAddOrUpdateModal(true);
                                                });
                                        }}
                                    >
                                        <PlusOutlined /> 新建
                                    </Button>
                                ) : undefined,
                            ]}
                        </div>
                    </Col>
                </Row>

                <Row>
                    <Col span={24}>
                        <Table
                            pagination={{
                                current,
                                pageSize,
                                total,
                                showSizeChanger: true,
                                showQuickJumper: true,
                                showTotal: (total, range) => {
                                    if (range[0] == range[1]) {
                                        return `第 ${range[0]} 条，共 ${total} 条`;
                                    } else {
                                        return `第 ${range[0]}~${range[1]} 条，共 ${total} 条`;
                                    }
                                },
                                onChange: (page, pageSize) => {
                                    setCurrent(page);
                                    setPageSize(pageSize);
                                }
                            }}
                            size="middle"
                            columns={columns}
                            dataSource={data}
                            rowKey={"id"}
                            rowSelection={rowSelection}
                            expandable={expand?.length > 0 ? {
                                expandRowByClick: true,
                                expandedRowRender: (record) => {
                                    return (
                                        <Tabs
                                            tabPosition={'left'}
                                            items={expand.map(x => {
                                                let sheet = { ...x };
                                                sheet.source = record;

                                                return {
                                                    label: x.name,
                                                    key: uniqueId(),
                                                    children: <SpreadForm sheet={sheet} readonly />,
                                                };
                                            })}
                                        />
                                    );
                                },
                            } : undefined}
                        />
                    </Col>
                </Row>
            </Card>

            {selectedRowKeys?.length > 0 && (
                <FooterToolbar
                    extra={
                        <div>
                            已选择{' '}
                            <a
                                style={{
                                    fontWeight: 600,
                                }}
                            >
                                {selectedRowKeys.length}
                            </a>{' '}
                            项 &nbsp;&nbsp;
                        </div>
                    }
                >
                    <Popconfirm
                        title="删除？"
                        placement="topRight"
                        onConfirm={async () => {
                            await handleRemove(selectedRowKeys);
                            setSelectedRowKeys([]);
                            actionRef.current?.reloadAndRest?.();
                        }}
                    >
                        <Button danger>
                            批量删除
                        </Button>
                    </Popconfirm>
                </FooterToolbar>
            )}

            <Modal
                open={addOrUpdateModal}
                title={mode == 'create' ? '新建' : mode == 'update' ? '更新' : '未知'}
                width={'70%'}
                onCancel={() => setAddOrUpdateModal(false)}
                onOk={() => {
                    const model = (addOrUpdateFormRef.current! as any).getModel();

                    if (mode == 'create') {
                        props
                            .createRequest?.(model)
                            .then(jsend => {
                                if (jsend.status == 'success') {
                                    message.success('已提交');
                                    actionRef.current?.reloadAndRest?.();
                                    setAddOrUpdateModal(false);
                                    refresh();
                                } else {
                                    message.error('提交发生错误');
                                }
                            })
                            .catch(e => {
                                console.log(e);
                            });
                    }
                    else if (mode == 'update') {
                        props
                            .updateRequest?.(model)
                            .then(jsend => {
                                if (jsend.status == 'success') {
                                    message.success('已提交');
                                    actionRef.current?.reloadAndRest?.();
                                    setAddOrUpdateModal(false);
                                    refresh();
                                } else {
                                    message.error('提交发生错误');
                                }
                            })
                            .catch(e => {
                                console.log(e);
                            });
                    }
                }
                }
            >
                <SpreadForm
                    fullWidth
                    ref={addOrUpdateFormRef}
                    sheet={addOrUpdateForm}
                />
            </Modal>
        </>
    );
};
