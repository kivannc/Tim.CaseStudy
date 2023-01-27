import React from 'react';
import { NumericFormat } from 'react-number-format';

//This component is used to format currency values
//It uses the react-number-format package
//https://www.npmjs.com/package/react-number-format
//This could be updated according to locale currency

const Currency = ({ value }) => (
  <NumericFormat
    value={value}
    displayType={'text'}
    thousandSeparator={true}
    prefix={'$'}
    decimalScale={2}
    fixedDecimalScale={true}
  />
);

export default Currency;
