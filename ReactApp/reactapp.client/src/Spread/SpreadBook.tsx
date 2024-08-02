/* eslint-disable @typescript-eslint/no-explicit-any */
import { Tabs } from 'antd';
import * as React from 'react';
import { ReactNode, useEffect, useState } from 'react'
import SpreadSheet from './SpreadSheet';

export default function SpreadBook(props: {
    book?: API.ISpreadBook,
    extraContent?: any,
    fullWidth?: boolean,
    scopes?: Record<string, Record<string, (cell: API.ICell) => ReactNode>>
}) {
    const [sheet, setSheet] = useState<API.ISpreadSheet>();
    const [scopes, setScopes] = useState<Record<string, (cell: API.ICell) => ReactNode>>();

    useEffect(() => {
        if (props.book?.sheets!.length! > 0) {
            setSheet(props.book!.sheets![0]);
        }
    }, [props.book]);

    useEffect(() => {
        if (sheet) {
            if (props.scopes) {
                const scopes = props.scopes![sheet.name!];
                setScopes(scopes);
            } else {
                setScopes(undefined);
            }
        }
    }, [sheet]);

    return (
        <>
            <Tabs
                items={props.book?.sheets?.map(sheet => {
                    return {
                        label: sheet.name,
                        key: sheet.name,
                    } as any;
                })}
                onChange={key => {
                    let sheet = props.book?.sheets!.find(x => x.name == key);
                    setSheet(sheet);
                }}
                activeKey={sheet?.name}
                tabBarExtraContent={props.extraContent}
            />

            <SpreadSheet
                fullWidth={props.fullWidth}
                sheet={sheet}
                scopes={scopes}
            />
        </>
    );
}
