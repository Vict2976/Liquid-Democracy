import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import { ElectionService } from "../services/election.service";
import { Election, Candidate, User } from "../builder/Interface";
import {Form, Button, Alert} from 'react-bootstrap';
import { FinalizeVoteForCandidate, FinalizeVoteForDelegate } from "../fetch";
import { UserService } from "../services/user.service";
import { SigningService } from "../services/signing.service";



export default function ElectionFunc() {

  const electionService = new ElectionService(); 

  const [election, setElection] = useState<Election>();
  const [candidates, setCandidates] = useState<Candidate[]>();
  const [delegates, setDelegates] = useState<User[]>();
  const userId = localStorage.getItem("userId")


  let { state } = useLocation();
  let id = Number(state);



  useEffect(() => {
    electionService.GetElectionFromId(id).then((messages) => {
      setElection(messages);
    });
  }, []);

  useEffect(()=>{
    electionService.GetAllCandidatesFromElecttion(id).then((candidates) => {
      setCandidates(candidates);
    });
  },[]);

  useEffect(()=>{
    electionService.GetAllDelegatesFromElecttion(id).then((del) => {
      setDelegates(del);
    });
  },[]);

  function checkLogin(voteForCandidate : boolean, votedForId : number){
    const userSerivce = new UserService();
    const signingService = new SigningService()

    let userId = sessionStorage.getItem('userId')

    if (userId == "undefined" || userId == null){
      alert("You must be logged in to vote")
      return
    }
    userSerivce.CheckIfUserIsLoggedIn(Number (userId)).then((result) => {
      const isUserLoggedIn = result;
      if(!isUserLoggedIn){
        alert("You must be logged in to vote")
        return
      }
    })
    
    if (voteForCandidate){
      signingService.SignVoteForCandidate(Number(userId), election?.electionId, votedForId)
    }else{
      signingService.SignVoteForDelegate(Number(userId), election?.electionId, votedForId)
    }
  }

  if (election?.isEnded){
    return(
      <h1>Election has ended</h1>
    )
  }else if(candidates!= undefined && delegates!==undefined){
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
            <button onClick={()=> checkLogin(true, can.candidateId)}> Vote For: {can.name}</button>
          </ul>
        ))}
        </div>
      
        <div>
          <h3>Delegates</h3>
        
        {delegates.map((del) => (
          <ul>
            <li>{del.userName}</li>
            <button onClick={()=> checkLogin(false, del.userId)}>Delegate Vote To: {del.userName}</button>
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