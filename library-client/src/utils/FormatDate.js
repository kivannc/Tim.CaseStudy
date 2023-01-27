import moment from 'moment';

const formatDate = (date) => {
  moment.locale();
  return moment(date).format('LL');
};

export default formatDate;
