import React, { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { Table, Container, Col, Row, Button } from 'react-bootstrap';
import axios from 'axios';

const DailyReport = () => {
  const [showLate, setShowLate] = useState(true);
  const [showUpcoming, setShowUpcoming] = useState(true);

  const { data, isLoading, error } = useQuery({
    queryKey: ['dailyReport'],
    queryFn: () =>
      axios.get('https://localhost:7133/api/reports').then((res) => res.data),
    refetchOnWindowFocus: false,
    refetchOnMount: false,
  });

  if (isLoading) return <p>Loading...</p>;
  if (error) return <p>An error has occurred: {error.message}</p>;

  const { lateTransactions, upcomingTransactions } = data;

  return (
    <Container>
      <Row>
        <Button onClick={() => setShowLate(!showLate)}>
          {showLate ? 'Hide' : 'Show'} Overdue Books
        </Button>
        {showLate && (
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>ID</th>
                <th>Book Name</th>
                <th>Member Name</th>
                <th>Due Date</th>
                <th>Late Days</th>
                <th>Penalty</th>
              </tr>
            </thead>
            <tbody>
              {lateTransactions.map((transaction) => (
                <tr key={transaction.Id}>
                  <td>{transaction.id}</td>
                  <td>{transaction.book.name}</td>
                  <td>{transaction.member.firstName}</td>
                  <td>{transaction.dueDate}</td>
                  <td>{transaction.lateDays}</td>
                  <td>{transaction.penalty}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        )}

        <br />
        <Button onClick={() => setShowUpcoming(!showUpcoming)}>
          {showUpcoming ? 'Hide' : 'Show'} Upcoming Books
        </Button>
        {showUpcoming && (
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>ID</th>
                <th>Book Name</th>
                <th>Member Name</th>
                <th>Borrow Date</th>
                <th>Due Date</th>
                <th>Book Status</th>
              </tr>
            </thead>
            <tbody>
              {upcomingTransactions.map((transaction) => (
                <tr key={transaction.Id}>
                  <td>{transaction.id}</td>
                  <td>{transaction.book.name}</td>
                  <td>{transaction.member.firstName}</td>
                  <td>{transaction.borrowDate}</td>
                  <td>{transaction.dueDate}</td>
                  <td>{transaction.bookStatus}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        )}
      </Row>
    </Container>
  );
};

export default DailyReport;
