/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { ReactNode, useEffect, useImperativeHandle, useState } from 'react';

export default function ChocTable(props: {
  ref?: React.MutableRefObject<any>,
  book?: Spread.IChocBook,
  sheetsRender?: (sheets: Spread.IChocSheet[]) => React.ReactNode,
  scopes?: Record<string, (cell: Spread.IChocCell) => ReactNode>
}) {
  const [sheet, setSheet] = useState(null);

  useImperativeHandle(props.ref, () => {
    // eslint-disable-next-line no-unused-labels
    selectSheet: (sheet: Spread.IChocSheet) => {
      setSheet(sheet);
    }
  });

  useEffect(() => {
    if (props.book?.sheets.length > 0) {
      setSheet(props.book.sheets[0]);
    }
  }, [props.book]);

  function style(style: any) {
    const dict = {};

    if (!style) return '';

    if (style.backgroundColor) {
      dict['backgroundColor'] = `rgba(${style.backgroundColor.red},${style.backgroundColor.green},${style.backgroundColor.blue},${style.backgroundColor.alpha / 255})`;
    }
    if (style.color) {
      dict['color'] = `rgba(${style.color.red},${style.color.green},${style.color.blue},${style.color.alpha / 255})`;
    }
    if (style.fontFamily) {
      dict['fontFamily'] = style.fontFamily;
    }
    if (style.fontSize) {
      dict['fontSize'] = style.fontSize;
    }
    if (style.fontWeight) {
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
    if (style.textAlign && style.textAlign !== 0) {
      switch (style.textAlign) {
        case 1: dict['textAlign'] = 'left'; break;
        case 2: dict['textAlign'] = 'center'; break;
        case 3: dict['textAlign'] = 'right'; break;
      }
    }
    if (style.verticalAlign && style.verticalAlign !== 0) {
      switch (style.verticalAlign) {
        case 1: dict['verticalAlign'] = 'top'; break;
        case 2: dict['verticalAlign'] = 'middle'; break;
        case 3: dict['verticalAlign'] = 'bottom'; break;
      }
    }

    return dict;
  }

  return (
    <>
      <ul>
        {
          props.book?.sheets?.map(sheet => {
            return (
              <li key={sheet.name}>
                <button type="button" onClick={() => setSheet(sheet)}> {sheet.name} </button>
              </li>
            );
          })
        }
      </ul>

      <table>
        <tbody>
          {
            sheet?.rows?.map((row: Spread.IChocRow, i: number) => {
              return (
                <tr key={i}>
                  {
                    row.cells.filter(x => !x.ignored).map((cell, i) => {
                      const tdProps = {};
                      if (cell.rowSpan > 1) tdProps['rowSpan'] = `${cell.rowSpan}`;
                      if (cell.colSpan > 1) tdProps['colSpan'] = `${cell.colSpan}`;
                      if (!cell.ignored && cell.value.style) tdProps['style'] = style(cell.value.style);

                      return (
                        <td key={i} {...tdProps}>
                          {
                            cell.value.scope && props.scopes[cell.value.scope] ? (
                              props.scopes[cell.value.scope](cell)
                            ) : cell.text}
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
    </>
  );
}
