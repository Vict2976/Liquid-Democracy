import React from 'react';
import logo from './logo.svg';
import './App.css';
import { fetchingMaterial } from './fetc';
import { fetchAuthenticationController } from './fetc';

function App() {
  return (
    <section>
    <h1> Welcome to the authentication process </h1>
        <button onClick={() => fetchingMaterial()}>
          Sign in
        </button>

        <button onClick={() => fetchAuthenticationController()}>
          Get session info
        </button>
  </section>
  );
}

export default App;
