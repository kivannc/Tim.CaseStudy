import { Table } from 'react-bootstrap';
import Currency from './Currency';
import formatDate from '../utils/FormatDate';

const BookTransaction = ({ transaction }) => {
  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Member Id</th>
          <th>Member</th>
          <th>Borrow Date</th>
          <th>Due Date</th>
          <th>Late Days</th>
          <th>Penalty</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{transaction.member.id}</td>
          <td>
            {transaction.member.firstName} {transaction.member.lastName}
          </td>
          <td>{formatDate(transaction.borrowDate)}</td>
          <td>{formatDate(transaction.dueDate)}</td>
          <td>{transaction.lateDays}</td>
          <td>
            <Currency value={transaction.penalty} />
          </td>
        </tr>
      </tbody>
    </Table>
  );
};

export default BookTransaction;
