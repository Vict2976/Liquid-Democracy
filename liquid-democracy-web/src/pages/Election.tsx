import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import { ElectionService } from "../services/election.service";
import { Election, Candidate, User } from "../builder/Interface";
import {Form, Button, Alert} from 'react-bootstrap';



export default function ElectionFunc() {

  const electionService = new ElectionService(); 

  const [election, setElection] = useState<Election>();
  const [candidates, setCandidates] = useState<Candidate[]>();
  const [delegates, setDelegates] = useState<User[]>();

  let { state } = useLocation();
  let id = Number(state);
  /*     const singleElection = () => {
        GetElectionFromId(Number(state)).then((elect) => {
            setElection(elect)
          });
    } */

  useEffect(() => {
    electionService.GetElectionFromId(id).then((messages) => {
      setElection(messages);
    });
  }, []);


  useEffect(()=>{
    electionService.GetAllCandidatesFromElecttion( id).then((candidates) => {
      setCandidates(candidates);
    });
  },[]);

  useEffect(()=>{
    electionService.GetAllDelegatesFromElecttion( id).then((del) => {
      setDelegates(del);
    });
  },[]);


  if(candidates!= undefined && delegates!==undefined){
    return (
      <view>
        <ul>
          <li>{election?.name}</li>
          <li>{election?.description}</li>
          <li>{election?.createdDate}</li>
          <li>{election?.electionId}</li>
          <li>{election?.candidates}</li>
          <li>{election?.votings}</li>
        </ul>
  
        <div>
          <h3>Candidates</h3>
        {candidates.map((can) => (
          <ul>
            <li>{can.name}</li>
            <button> Vote For: {can.name}</button>
          </ul>
        ))}
        </div>
      
        <div>
          <h3>Delegates</h3>
        
        {delegates.map((del) => (
          <ul>
            <li>{del.userName}</li>
            <button>Delegate Vote To: {del.userName}</button>
          </ul>

        ))}
        </div>
      </view>
    );
  }else {
    return (
      <div className="page">
        <p>Error</p>
      </div>
    );
  }
  
}