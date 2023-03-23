import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import { ElectionService } from "../services/election.service";
import { Election, Candidate, User } from "../builder/Interface";
import { UserService } from "../services/user.service";
import { SigningService } from "../services/signing.service";



export default function ElectionFunc() {

  const electionService = new ElectionService(); 
  const signingService = new SigningService()

  const [election, setElection] = useState<Election>();
  const [candidates, setCandidates] = useState<Candidate[]>();

  let {state} = useLocation()
  let id = Number(state)


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


  if (election?.isEnded){
    return(
      <h1>Election has ended</h1>
    )
  }else if(candidates!= undefined){
    return (
      <view>
        <ul>
          <li>{election?.name}</li>
          <li>{election?.description}</li>
          <li>{election?.createdDate}</li>
          <li>{election?.electionId}</li>
          <li>{election?.votings}</li>
        </ul>

        <div>
          <h3>Candidates</h3>
        {candidates.map((can) => (
          <ul>
            <li>{can.name}</li>
            <button onClick={()=> signingService.SignVoteForCandidate(Number(0), election?.electionId, can.candidateId)}> Vote For: {can.name}</button>
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