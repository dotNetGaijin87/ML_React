import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h3>Available algorithms are:</h3>
        <h4>Regression</h4>
        <ul>
            <li>Fast Forest</li>
            <li>Fast Tree</li>
            <li>Sdca</li>
            </ul>
            <h4>Forecasting</h4>
            <ul>
                <li>Ssa</li>
            </ul>
      </div>
    );
  }
}
