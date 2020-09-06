import React, {Component} from 'react';
import './App.css';
import { cars } from './demo';
import CarItem from './CarItem';

class App extends Component {
  render() {
    return (
      <div className="App">
        <ul>
        {cars.map((car) => (
          <CarItem car={car} />
        ))}
        </ul>
      </div>
    );
  }
}

export default App;
