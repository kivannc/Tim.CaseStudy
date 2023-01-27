import { Form, FormControl, Button, Row, Col } from 'react-bootstrap';
import { useState } from 'react';

const SearchForm = ({ handleSearch, handleClear }) => {
  const [isbn, setIsbn] = useState('');
  const [name, setName] = useState('');
  const [author, setAuthor] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    handleSearch({ isbn, name, author });
  };

  const handleClearClick = () => {
    setIsbn('');
    setName('');
    setAuthor('');
    handleClear();
  };

  const buttonEnabled = isbn || name || author;
  return (
    <Form onSubmit={handleSubmit}>
      <Row>
        <Col md={12} lg={4}>
          <Form.Group controlId="formIsbn">
            <Form.Label>ISBN</Form.Label>
            <FormControl
              type="text"
              placeholder="Enter ISBN"
              value={isbn}
              onChange={(e) => setIsbn(e.target.value)}
            />
          </Form.Group>
        </Col>
        <Col md={12} lg={4}>
          <Form.Group controlId="formName">
            <Form.Label>Name</Form.Label>
            <FormControl
              type="text"
              placeholder="Enter name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </Form.Group>
        </Col>
        <Col md={12} lg={4}>
          <Form.Group controlId="formAuthor">
            <Form.Label>Author</Form.Label>
            <FormControl
              type="text"
              placeholder="Enter author"
              value={author}
              onChange={(e) => setAuthor(e.target.value)}
            />
          </Form.Group>
        </Col>
      </Row>
      <div className="justify-content-start">
        <Button
          disabled={!buttonEnabled}
          variant="primary"
          type="submit"
          style={{ margin: '20px 0px', marginRight: '20px' }}
        >
          Search
        </Button>

        <Button
          disabled={!buttonEnabled}
          variant="secondary"
          onClick={handleClearClick}
          style={{ margin: '20px 0px' }}
        >
          Clear
        </Button>
      </div>
    </Form>
  );
};

export default SearchForm;
