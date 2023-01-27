import React from 'react';
import axios from 'axios';
import { useQuery } from '@tanstack/react-query';
import { Button } from 'react-bootstrap';

const BookDetail = ({ isbn, handleClose }) => {
  const { data: book } = useQuery({
    queryKey: ['bookDetails', isbn],
    queryFn: () =>
      axios
        .get(`https://localhost:7133/api/Books/${isbn}`)
        .then((res) => res.data),
    enabled: !!isbn, // only run if bookIsbn is truthy, otherwise don't run
  });

  return (
    <>
      {book ? (
        <div>
          <h3>{book.name}</h3>
          <p>{book.isbn}</p>
          <p>{book.author}</p>
        </div>
      ) : null}
      <Button onClick={handleClose}>Close</Button>
    </>
  );
};

export default BookDetail;
