import React from 'react';
import DatePicker from 'react-datepicker';
import { useQuery } from '@tanstack/react-query';
import axios from 'axios';
import 'react-datepicker/dist/react-datepicker.css';

const DueDatePicker = ({ selected, onDateChange, className }) => {
  //30 working days has 6 working weeks 30 + 6*2 = 42
  // If api does not return due date, use 42 days as default
  const approximateDueDate = new Date().setDate(new Date().getDate() + 42);

  const { data } = useQuery({
    queryKey: ['getDueDate'],
    queryFn: () =>
      axios
        .get('https://localhost:7133/api/Books/GetDueDate')
        .then((res) => res.data),
    refetchOnWindowFocus: false,
    refetchOnMount: true,
  });

  const dueDate = data?.dueDate ? new Date(data.dueDate) : approximateDueDate;
  const holidays = data?.holidays
    ? data.holidays.map((date) => new Date(date))
    : [];

  const minDate = new Date();
  selected = selected ?? dueDate;

  return (
    <DatePicker
      selected={selected}
      onChange={onDateChange}
      minDate={minDate}
      maxDate={dueDate}
      placeholderText="Select a due date"
      excludeDates={holidays}
      className={className}
      calendarStartDay={1}
    />
  );
};

export default DueDatePicker;
