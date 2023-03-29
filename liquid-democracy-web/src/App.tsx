import './App.css';
import { Routes, Route } from "react-router-dom";
import './index.css';
import HomePage from './pages/HomePage';
import Election from './pages/ElectionSign';
import Admin from './pages/Admin';
import CreateElection from './pages/CreateElection';
import VoteAfterMitId from './pages/VoteAfterMitId';
import ElectionDescription from './pages/ElectionDescription';
import ElectionSign from './pages/ElectionSign';


export default function App() {
  let id = localStorage.getItem("sessionId")

  return (
    <>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/CreateElection" element={<CreateElection />} />
        <Route path="/Election/:electionId" element={<ElectionDescription/>}></Route>
        <Route path="/VoteAfterMitId" element={ id==null ? <HomePage/> :(<VoteAfterMitId/> ) } />
        <Route path="/Admin" element={<Admin/>} />
        <Route path="/Election/sign/:electionId" element={<ElectionSign/>} />
      </Routes>
    </>
  );
}
