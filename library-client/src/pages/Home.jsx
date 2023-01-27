import React, { useState } from 'react';
import axios from 'axios';
import { useQuery } from '@tanstack/react-query';
import { Container, Row, Col } from 'react-bootstrap';
import SearchForm from '../components/SearchForm';
import BookTable from '../components/BookTable';
import BookDetail from '../components/BookDetail';

const Home = () => {
  const [bookSearch, setBookSearch] = useState(null);
  const [bookIsbn, setBookIsbn] = useState('');

  const { error, data } = useQuery({
    queryKey: ['searchBooks', bookSearch],
    queryFn: () =>
      axios
        .post('https://localhost:7133/api/Books', bookSearch)
        .then((res) => res.data),
    enabled: bookSearch != null, // only run if search is truthy, otherwise don't run
    refetchOnWindowFocus: false,
    refetchOnMount: false,
    refetchOnReconnect: false,
    refetchInterval: false,
    refetchIntervalInBackground: false,

    // only run if search is truthy, otherwise don't run
  });

  const handleBookClick = (isbn) => {
    setBookIsbn(isbn);
  };

  const handleBookClose = () => {
    setBookIsbn(''); // clear search
  };
  const handleSearch = (book) => {
    setBookSearch(book);
  };

  if (error) return 'An error has occurred: ' + error.message;

  return (
    <Container>
      <Row>
        <Col xs={12} md={bookIsbn ? 6 : 12}>
          <SearchForm handleSearch={handleSearch} />
          <BookTable data={data} handleBookClick={handleBookClick} />
        </Col>
        {bookIsbn ? (
          <Col xs={12} md={6}>
            <BookDetail isbn={bookIsbn} handleClose={handleBookClose} />
          </Col>
        ) : null}
      </Row>
    </Container>
  );
};

export default Home;
