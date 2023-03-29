import './App.css';
import { Routes, Route } from "react-router-dom";
import './index.css';
import HomePage from './pages/HomePage';
import Admin from './pages/Admin';
import CreateElection from './pages/CreateElection';
import ElectionDescription from './pages/ElectionDescription';
import ElectionSign from './pages/ElectionSign';
import Error from './pages/Error';



export default function App() {

  return (
    <>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/CreateElection" element={<CreateElection />} />
        <Route path="/Election/:electionId" element={<ElectionDescription/>}></Route>
        <Route path="/Admin" element={<Admin/>} />
        <Route path="/Error" element={<Error/>} />
        <Route path="/Election/sign/:electionId" element={<ElectionSign/>} />
      </Routes>
    </>
  );
}
