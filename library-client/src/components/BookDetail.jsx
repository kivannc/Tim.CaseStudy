import React from 'react';
import axios from 'axios';
import { useQuery } from '@tanstack/react-query';
import { Container, Row, Col, Button, Form } from 'react-bootstrap';
import BookTransaction from './BookTransaction';
import BookCard from './BookCard';
import BookReserve from './BookReserve';

const BookDetail = ({ isbn, handleClose }) => {
  const {
    isLoading,
    error,
    data: book,
  } = useQuery({
    queryKey: ['book', isbn],
    queryFn: () =>
      axios
        .get(`https://localhost:7133/api/books/${isbn}`)
        .then((res) => res.data),
    enabled: !!isbn, // only run if bookIsbn is truthy, otherwise don't run
  });

  const { data: transaction } = useQuery({
    queryKey: ['bookDetail', book?.bookStatus],
    queryFn: () =>
      axios
        .get(`https://localhost:7133/api/transactions/isbn/${isbn}`)
        .then((res) => res.data),
    enabled: !!book?.bookStatus, // only run if bookIsbn is truthy, otherwise don't run
    refetchOnWindowFocus: false,
    refetchOnMount: false,
  });

  const handleReserve = (userId) => {};

  if (isLoading) return <p>Loading...</p>;
  if (error) return <p>An error has occurred: {error.message}</p>;

  const showReserve = book?.bookStatus === 0;

  return (
    <>
      <Row className="mt-2">
        <Col md={{ span: 8, offset: 2 }}>
          <BookCard book={book} />
        </Col>
      </Row>
      <Row className="mt-2">
        <Col md={{ span: 8, offset: 2 }}>
          {transaction && <BookTransaction transaction={transaction} />}
        </Col>
      </Row>
      <Row className="mt-2">
        <Col md={{ span: 8, offset: 2 }}>
          <Button onClick={handleClose}>Close</Button>
        </Col>
      </Row>

      <Row className="mt-2">
        <Col md={{ span: 8, offset: 2 }}>
          <hr />
          {!transaction && <BookReserve handleReserve={handleReserve} />}
        </Col>
      </Row>
    </>
  );
};

export default BookDetail;
