import React from 'react';
import { Table } from 'react-bootstrap';
import { BookStatus, getColor } from '../utils/BookStatus';

const BookTable = ({ data, handleBookClick }) => {
  return (
    <Table>
      <thead>
        <tr>
          <th>Name</th>
          <th>ISBN</th>
          <th>Author</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        {data?.map((book) => (
          <tr key={book.isbn} onClick={() => handleBookClick(book.isbn)}>
            <td>{book.name}</td>
            <td>{book.isbn}</td>
            <td>{book.author}</td>
            <td
              style={{
                color: getColor(book.bookStatus),
              }}
            >
              {BookStatus[book.bookStatus]}
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};

export default BookTable;
