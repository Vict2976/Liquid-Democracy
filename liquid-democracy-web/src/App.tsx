import './App.css';
import { Routes, Route } from "react-router-dom";
import './index.css';
import Login from "./pages/Login";
import SignUp from "./pages/SignUp";
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
        <Route path="/CreateElection" element={<CreateElection />} />
        <Route path="/Election" element={<Election/>} />
        <Route path="/VoteAfterMitId" element={ id==null ? <Login/> :(<VoteAfterMitId/> ) } />
        <Route path="/Admin" element={<Admin/>} />
      </Routes>
    </>
  );
}
