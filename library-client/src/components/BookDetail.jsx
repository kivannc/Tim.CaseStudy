import React from 'react';
import axios from 'axios';
import { useQuery } from '@tanstack/react-query';
import { Container, Row, Col, Card, Table, Button } from 'react-bootstrap';
import Currency from '../components/Currency';
import formatDate from '../utils/FormatDate';
import { BookStatus, getColor } from '../utils/BookStatus';

const BookDetail = ({ isbn, handleClose }) => {
  const {
    isLoading,
    error,
    data: book,
  } = useQuery({
    queryKey: ['book', isbn],
    queryFn: () =>
      axios
        .get(`https://localhost:7133/api/Books/${isbn}`)
        .then((res) => res.data),
    enabled: !!isbn, // only run if bookIsbn is truthy, otherwise don't run
  });

  const { data: transaction, refetch } = useQuery({
    queryKey: ['bookDetail', book],
    queryFn: () =>
      axios
        .get(`https://localhost:7133/api/Transaction/isbn/${isbn}`)
        .then((res) => res.data),
    enabled: false, // only run if bookIsbn is truthy, otherwise don't run
  });

  if (isLoading) return <p>Loading...</p>;
  if (error) return <p>An error has occurred: {error.message}</p>;
  if (book?.bookStatus !== 0) refetch();

  return (
    <Container>
      <Row className="mt-2">
        <Col md={{ span: 8, offset: 2 }}>
          <Card>
            <Card.Body>
              <Card.Title>{book.name}</Card.Title>
              <Card.Text>{book.isbn}</Card.Text>
            </Card.Body>
            <Card.Footer>
              <small className="text-muted">{book.author}</small>
              <strong
                style={{
                  color: getColor(book.bookStatus),
                  float: 'right',
                }}
              >
                {BookStatus[book.bookStatus]}
              </strong>
            </Card.Footer>
          </Card>
        </Col>
      </Row>
      <Row className="mt-2">
        <Col md={{ span: 8, offset: 2 }}>
          {transaction && (
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>Member</th>
                  <th>Borrow Date</th>
                  <th>Due Date</th>
                  <th>Late Days</th>
                  <th>Penalty</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>
                    {transaction.member.firstName} {transaction.member.lastName}
                  </td>
                  <td>{formatDate(transaction.borrowDate)}</td>
                  <td>{formatDate(transaction.dueDate)}</td>
                  <td>{transaction.lateDays}</td>
                  <td>
                    <Currency value={transaction.penalty} />
                  </td>
                </tr>
              </tbody>
            </Table>
          )}
        </Col>
      </Row>
      <Row className="mt-2">
        <Col md={{ span: 8, offset: 2 }}>
          <Button style={{ float: 'right' }} onClick={handleClose}>
            Close
          </Button>
        </Col>
      </Row>
    </Container>
  );
};

// return (
//   <>
//     {book ? (
//       <div>
//         <h3>{book.name}</h3>
//         <p>{book.isbn}</p>
//         <p>{book.author}</p>
//       </div>
//     ) : null}
//     <Button onClick={handleClose}>Close</Button>
//   </>
// );
//};

export default BookDetail;
