import React, { useState } from 'react';
import axios from 'axios';
import { useQuery } from '@tanstack/react-query';
import { Container, Table } from 'react-bootstrap';
import SearchForm from '../components/SearchForm';

const BookStatus = {
  0: 'Available',
  1: 'Borrowed',
  2: 'Overdue',
};

const getColor = (status) => {
  switch (status) {
    case 0:
      return 'green';
    case 1:
      return 'orange';
    case 2:
      return 'red';
    default:
      return 'black';
  }
};

const Home = () => {
  const [bookSearch, setBookSearch] = useState({
    isbn: '',
    name: '',
    author: '',
  });
  const [bookIsbn, setBookIsbn] = useState('');

  const { error, data } = useQuery({
    queryKey: ['searchBooks', bookSearch],
    queryFn: () =>
      axios
        .post('https://localhost:7133/api/Books', bookSearch)
        .then((res) => res.data),
    enabled: true,
    refetchOnWindowFocus: false,
    // only run if search is truthy, otherwise don't run
  });

  const handleBookClick = (isbn) => {
    setBookIsbn(isbn);
  };

  const handleSearch = (book) => {
    // check if setBookSearch will set and after refetch called;
    setBookSearch(book);
  };

  if (error) return 'An error has occurred: ' + error.message;

  return (
    <Container>
      <SearchForm handleSearch={handleSearch} />
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
      <br />
      {bookIsbn ? <BookDetails isbn={bookIsbn} /> : null}
    </Container>
  );
};

const BookDetails = ({ isbn }) => {
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
    </>
  );
};

export default Home;
