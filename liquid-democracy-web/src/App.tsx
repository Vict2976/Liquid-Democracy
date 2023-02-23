import React from 'react';
import logo from './logo.svg';
import './App.css';
import { fetchStartMitIDSession } from './fetch';
import { time } from 'console';

import ReactDOM from "react-dom";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './index.css';
import Login from "./pages/Login";
import SignUp from "./pages/SignUp";
import MitID from './pages/MitID';
import HomePage from './pages/HomePage';
import Register from './pages/Register';
import CreateElection from './pages/CreateElection';

export default function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/MitID" element={<MitID />} />
        <Route path="/CreateElection" element={<CreateElection />} />
      </Routes>
    </>
  );
}
