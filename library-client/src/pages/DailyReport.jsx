import React, { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { Table, Container, Row } from 'react-bootstrap';
import axios from 'axios';

import ToggleButton from '../components/ToggleButton';
import formatDate from '../utils/FormatDate';
import Currency from '../components/Currency';

const DailyReport = () => {
  const [showLate, setShowLate] = useState(true);
  const [showUpcoming, setShowUpcoming] = useState(true);

  const { data, isLoading, error } = useQuery({
    queryKey: ['dailyReport'],
    queryFn: () =>
      axios.get('https://localhost:7133/api/reports').then((res) => res.data),
  });

  if (isLoading) return <p className="text-center">Loading...</p>;
  if (error) return <p>An error has occurred: {error.message}</p>;

  const { lateTransactions, upcomingTransactions } = data;

  return (
    <Container className="mt-4">
      <Row>
        <ToggleButton
          label="Overdue Books"
          show={showLate}
          setShow={setShowLate}
        />
        {showLate && (
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>ISBN</th>
                <th>Book Name</th>
                <th>Member Name</th>
                <th>Due Date</th>
                <th>Late Days</th>
                <th>Penalty</th>
              </tr>
            </thead>
            <tbody>
              {lateTransactions.map((transaction) => (
                <tr key={transaction.id}>
                  <td>{transaction.book.isbn}</td>
                  <td>{transaction.book.name}</td>
                  <td>
                    {transaction.member.firstName} {transaction.member.lastName}
                  </td>
                  <td>{formatDate(transaction.dueDate)}</td>
                  <td>{transaction.lateDays}</td>
                  <td>
                    <strong>
                      <Currency value={transaction.penalty} />
                    </strong>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        )}
      </Row>
      <Row className="mt-5">
        <ToggleButton
          className="mt-4"
          label="Upcoming Books"
          show={showUpcoming}
          setShow={setShowUpcoming}
        />
        {showUpcoming && (
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>ISBN</th>
                <th>Book Name</th>
                <th>Member Name</th>
                <th>Borrow Date</th>
                <th>Due Date</th>
              </tr>
            </thead>
            <tbody>
              {upcomingTransactions.map((transaction) => (
                <tr key={transaction.id}>
                  <td>{transaction.book.isbn}</td>
                  <td>{transaction.book.name}</td>
                  <td>{transaction.member.firstName}</td>
                  <td>{formatDate(transaction.borrowDate)}</td>
                  <td>
                    <strong>{formatDate(transaction.dueDate)}</strong>
                  </td>
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
