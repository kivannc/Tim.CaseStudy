import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronDown, faChevronUp } from '@fortawesome/free-solid-svg-icons';

const ToggleButton = ({ label, show, setShow }) => {
  const toggleIcon = show ? faChevronUp : faChevronDown;

  return (
    <h1 className="display-6 text-center" onClick={() => setShow(!show)}>
      {label} <FontAwesomeIcon icon={toggleIcon} size="xs" />
    </h1>
  );
};

export default ToggleButton;
