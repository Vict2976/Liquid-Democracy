import React from 'react';
import logo from './logo.svg';
import './App.css';
import { fetchingMaterial } from './fetc';
import { fetchAuthenticationController } from './fetc';
import { time } from 'console';

function App() {
  return (
    <section>
    <h1> Welcome to the authentication process </h1>
        <button onClick={() => {
          var fetchData = fetchAuthenticationController();
        }
          }>
          Login with MitId
        </button>
  </section>
  );
}

export default App;
