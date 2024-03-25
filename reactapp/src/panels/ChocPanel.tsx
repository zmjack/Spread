import { useEffect, useState } from 'react';
import ChocTable from '../componets/ChocTable';

export default function ChocPanel() {
  const [book, setBook] = useState<Spread.IChocBook>();

  async function populateData() {
    const response = await fetch('http://localhost:5132/Choc/Get');
    const data = await response.json();
    setBook(data);
  }

  useEffect(() => {
    populateData();
  }, []);

  return (
    <div>
      <ChocTable
        book={book}
        scopes={{
          data: cell => {
            return <> {cell.text} + 123 </>
          }
        }}
      />
    </div >
  );
}
