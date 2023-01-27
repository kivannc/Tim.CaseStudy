import React, { useState } from 'react';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { useQuery } from '@tanstack/react-query';
import axios from 'axios';

const BookReserve = ({ handleReserve }) => {
  const [userId, setUserId] = useState('');

  const { data: user } = useQuery({
    queryKey: ['user', userId],
    queryFn: () =>
      axios
        .get(`https://localhost:7133/api/members/${userId}`)
        .then((res) => res.data),
    enabled: !!userId, // only run if bookIsbn is truthy, otherwise don't run
    refetchOnWindowFocus: false,
    refetchOnMount: false,
  });

  return (
    <>
      <Form>
        <Row>
          <Col lg={5}>
            <Form.Group>
              <Form.Label>User ID</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter user ID"
                value={userId}
                onChange={(e) => setUserId(e.target.value)}
              />
            </Form.Group>
          </Col>
          <Col lg={2}>
            <Button
              disabled={!user}
              style={{ marginTop: '32px', float: 'right' }}
              variant="primary"
              onClick={() => handleReserve(userId)}
            >
              Reserve
            </Button>
          </Col>
        </Row>
      </Form>
      {user && (
        <Row>
          <Col lg={5} className="mt-2">
            <strong className="display-6">
              {user.firstName} {user.lastName}
            </strong>
          </Col>
        </Row>
      )}
    </>
  );
};

export default BookReserve;
