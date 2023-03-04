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
import Election from './pages/Election';
import Register from './pages/Register';
import Admin from './pages/Admin';
import CreateElection from './pages/CreateElection';
import VoteAfterMitId from './pages/VoteAfterMitId';


export default function App() {
  let id = localStorage.getItem("sessionId")

  return (
    <>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/MitID" element={<MitID />} />
        <Route path="/CreateElection" element={<CreateElection />} />
        <Route path="/Election" element={<Election/>} />
        <Route path="/VoteAfterMitId" element={ id==null ? <Login/> :(<VoteAfterMitId/> ) } />
        <Route path="/Admin" element={<Admin/>} />
      </Routes>
    </>
  );
}
