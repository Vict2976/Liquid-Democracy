import React from 'react';
import logo from './logo.svg';
import './App.css';
import { fetchStartMitIDSession } from './fetch';
import { time } from 'console';

function App() {
  return (
    <section>
    <h1> Welcome to the authentication process </h1>
        <button onClick={() => {
          var fetchData = fetchStartMitIDSession();
          console.log(fetchData);
        }
          }>
          Login with MitId
        </button>
  </section>
  );
}

export default App;
