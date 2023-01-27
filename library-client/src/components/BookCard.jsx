import { Card } from 'react-bootstrap';
import { BookStatus, getColor } from '../utils/BookStatus';

const BookCard = ({ book }) => {
  return (
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
  );
};

export default BookCard;
