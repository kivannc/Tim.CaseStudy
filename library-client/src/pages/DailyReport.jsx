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
    refetchOnWindowFocus: false,
    refetchOnMount: false,
  });

  if (isLoading) return <p className="text-center">Loading...</p>;
  if (error) return <p>An error has occurred: {error.message}</p>;

  const { lateTransactions, upcomingTransactions } = data;

  return (
    <Container>
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
                  <td>{formatDate(transaction.dueDate)}</td>
                  <td>{transaction.lateDays}</td>
                  <td>
                    <Currency value={transaction.penalty} />
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        )}

        <hr />
        <ToggleButton
          label="Upcoming Books"
          show={showUpcoming}
          setShow={setShowUpcoming}
        />
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
                  <td>{formatDate(transaction.borrowDate)}</td>
                  <td>{formatDate(transaction.dueDate)}</td>
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
