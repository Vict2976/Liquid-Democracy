import './App.css';
import { Routes, Route } from "react-router-dom";
import './index.css';
import HomePage from './pages/HomePage';
import Election from './pages/Election';
import Admin from './pages/Admin';
import CreateElection from './pages/CreateElection';
import VoteAfterMitId from './pages/VoteAfterMitId';
import ElectionDescription from './pages/ElectionDescription';


export default function App() {
  let id = localStorage.getItem("sessionId")

  return (
    <>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/CreateElection" element={<CreateElection />} />
        <Route path="/Election" element={<Election />} />
        <Route path="/VoteAfterMitId" element={ id==null ? <HomePage/> :(<VoteAfterMitId/> ) } />
        <Route path="/Admin" element={<Admin/>} />
        <Route path="/ElectionDescription" element={<ElectionDescription/>} />
      </Routes>
    </>
  );
}
