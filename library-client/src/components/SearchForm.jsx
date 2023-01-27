import { Form, FormControl, Button } from 'react-bootstrap';
import { useState } from 'react';

const SearchForm = ({ handleSearch }) => {
  const [isbn, setIsbn] = useState('');
  const [name, setName] = useState('');
  const [author, setAuthor] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    handleSearch({ isbn, name, author });
  };

  const buttonEnabled = isbn || name || author;
  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group controlId="formIsbn">
        <Form.Label>ISBN</Form.Label>
        <FormControl
          type="text"
          placeholder="Enter ISBN"
          value={isbn}
          onChange={(e) => setIsbn(e.target.value)}
        />
      </Form.Group>

      <Form.Group controlId="formName">
        <Form.Label>Name</Form.Label>
        <FormControl
          type="text"
          placeholder="Enter name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </Form.Group>

      <Form.Group controlId="formAuthor">
        <Form.Label>Author</Form.Label>
        <FormControl
          type="text"
          placeholder="Enter author"
          value={author}
          onChange={(e) => setAuthor(e.target.value)}
        />
      </Form.Group>

      <Button
        disabled={!buttonEnabled}
        variant="primary"
        type="submit"
        style={{ margin: '20px 0px' }}
      >
        Search
      </Button>
    </Form>
  );
};

export default SearchForm;
