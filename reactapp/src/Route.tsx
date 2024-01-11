import React, { ReactNode, useState } from 'react';

export function Route(props: {
  components: Record<string, React.ReactNode>
}) {
  const [panel, setPanel] = useState<ReactNode>(<></>);

  return (
    <div>
      <p> Simples </p>
      {
        Object.keys(props.components).map(key => {
          const c = props.components[key];
          return (
            <button key={key} type="button" onClick={() => setPanel(c)}> {key} </button>
          );
        })
      }
      <hr />

      <div>
        {panel}
      </div>
    </div>
  );
}
