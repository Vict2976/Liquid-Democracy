import { useState, useEffect } from "react";
import { Link, Navigate, To, useLocation } from "react-router-dom";
import { ElectionService } from "../services/election.service";
import { Election, Candidate, User } from "../builder/Interface";
import {Form, Button, Alert} from 'react-bootstrap';
import { FinalizeVoteForCandidate, FinalizeVoteForDelegate } from "../fetch";
import { UserService } from "../services/user.service";
import { SigningService } from "../services/signing.service";
import ReactDOM from "react-dom";


function GoToSigning(){
  const userService = new UserService();
  userService.CreateUser()
}


export default function ElectionDescription() {

  const electionService = new ElectionService();

  const [election, setElection] = useState<Election>();

  let { state } = useLocation();
  let id = Number(state);

  useEffect(() => {
    electionService.GetElectionFromId(id).then((messages) => {
      setElection(messages);
    });
  }, []);

  if (election?.isEnded){
    return(
      <h1>Election has ended</h1>
    )
  }else{
    return (
      <view>
        <ul>
          <li>{election?.name}</li>
          <li>{election?.description}</li>
          <li>{election?.createdDate}</li>
        </ul>

        <div>
        <button onClick={ () => GoToSigning() }> Sign in with MitID</button>
        <Link to = "/Election" state={id} onClick={()=> console.log(id)}>
                      See Election
        </Link>
        </div>
      </view>
    );
  }
}