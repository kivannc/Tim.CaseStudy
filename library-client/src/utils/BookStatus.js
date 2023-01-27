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

export { BookStatus, getColor };
