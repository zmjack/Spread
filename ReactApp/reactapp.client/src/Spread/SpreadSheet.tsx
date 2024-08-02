/* eslint-disable @typescript-eslint/no-explicit-any */
import * as React from 'react';
import { ReactNode } from 'react'

export default function SpreadSheet(props: {
    sheet?: API.ISpreadSheet,
    fullWidth?: boolean,
    scopes?: Record<string, (cell: API.ICell) => ReactNode>
}) {
    function style(style: API.ISpreadStyle) {
        const dict: any = {
            "max-width": "516px",
            "white-space": "normal",
            "word-wrap": "break-word",
        };

        if (!style) return '';

        if (style.backgroundColor) {
            dict['backgroundColor'] = style.backgroundColor;
        }
        if (style.color) {
            dict['color'] = style.color;
        }
        if (style.fontFamily) {
            dict['fontFamily'] = style.fontFamily;
        }
        if (style.fontSize) {
            dict['fontSize'] = style.fontSize;
        }
        if (style.bold) {
            dict['fontWeight'] = 'bold';
        }
        if (style.borderTop) {
            dict['borderTop'] = '1px solid #000000';
        }
        if (style.borderBottom) {
            dict['borderBottom'] = '1px solid #000000';
        }
        if (style.borderLeft) {
            dict['borderLeft'] = '1px solid #000000';
        }
        if (style.borderRight) {
            dict['borderRight'] = '1px solid #000000';
        }
        if (style.padding) {
            dict['padding'] = style.padding;
        }
        if (style.horiAlign) {
            switch (style.horiAlign) {
                case 1: dict['textAlign'] = 'left'; break;
                case 2: dict['textAlign'] = 'center'; break;
                case 3: dict['textAlign'] = 'right'; break;
            }
        }
        if (style.vertAlign) {
            switch (style.vertAlign) {
                case 1: dict['verticalAlign'] = 'top'; break;
                case 2: dict['verticalAlign'] = 'middle'; break;
                case 3: dict['verticalAlign'] = 'bottom'; break;
            }
        }

        return dict;
    }

    return (
        <div style={{ overflowX: 'auto' }}>
            <table
                style={{
                    width: props.fullWidth ? '100%' : 'auto',
                    borderCollapse: 'collapse',
                    wordBreak: 'keep-all',
                    whiteSpace: 'nowrap'
                }}
            >
                <tbody>
                    {
                        props.sheet?.rows?.map((row: API.IRow, i: number) => {
                            return (
                                <tr key={i}>
                                    {
                                        row.cells!.filter(x => !x.ignored).map((cell, i) => {
                                            const tdProps: any = {};
                                            if (cell.rowSpan! > 1) tdProps['rowSpan'] = `${cell.rowSpan}`;
                                            if (cell.colSpan! > 1) tdProps['colSpan'] = `${cell.colSpan}`;
                                            if (!cell.ignored && cell.style) tdProps['style'] = style(cell.style);

                                            return (
                                                <td key={i} {...tdProps}>
                                                    {
                                                        cell.scope && props.scopes?.[cell.scope] ? (
                                                            props.scopes?.[cell.scope](cell)
                                                        ) : cell.text
                                                    }
                                                </td>
                                            );
                                        })
                                    }
                                </tr>
                            );
                        })
                    }
                </tbody>
            </table>
        </div>
    );
}
