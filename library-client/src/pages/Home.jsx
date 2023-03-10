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

  const { error, data, refetch } = useQuery({
    queryKey: ['searchBooks', bookSearch],
    queryFn: () =>
      axios
        .post('https://localhost:7133/api/Books', bookSearch)
        .then((res) => res.data),
    enabled: bookSearch != null, // only run if search is truthy, otherwise don't run
    refetchOnWindowFocus: false,
    refetchOnMount: true,
    refetchOnReconnect: false,
    refetchInterval: false,
    refetchIntervalInBackground: false,
  });

  const handleBookClick = (isbn) => {
    setBookIsbn(isbn);
  };

  const handleBookClose = () => {
    setBookIsbn('');
    refetch();
  };
  const handleSearch = (book) => {
    setBookSearch(book);
  };

  const handleClear = () => {
    setBookSearch(null);
  };

  if (error) return 'An error has occurred: ' + error.message;

  const renderBooks = () => (
    <Col>
      <SearchForm
        bookSearch={bookSearch}
        handleSearch={handleSearch}
        handleClear={handleClear}
      />
      {data && data.length > 0 ? (
        <BookTable data={data} handleBookClick={handleBookClick} />
      ) : null}
    </Col>
  );

  const renderBookDetail = () => (
    <Col>
      <BookDetail isbn={bookIsbn} handleClose={handleBookClose} />
    </Col>
  );

  return (
    <Container className="mt-4">
      <Row>{bookIsbn ? renderBookDetail() : renderBooks()}</Row>
    </Container>
  );
};

export default Home;
