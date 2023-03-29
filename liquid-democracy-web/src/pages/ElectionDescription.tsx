import { useState, useEffect } from "react";
import { Link, Navigate, To, useLocation } from "react-router-dom";
import { ElectionService } from "../services/election.service";
import { Election, Candidate, User } from "../builder/Interface";
import {Form, Button, Alert} from 'react-bootstrap';
import { FinalizeVoteForCandidate, FinalizeVoteForDelegate } from "../fetch";
import { UserService } from "../services/user.service";
import { SigningService } from "../services/signing.service";
import ReactDOM from "react-dom";


function GoToSigning() {
  const url = window.location.href;
  const parts = url.split("/");
  const currentElection = parts[parts.length - 1];
  const userService = new UserService();
  const data = userService.Authenticate(Number (currentElection)).then((response) => response)
  return data
}


export default function ElectionDescription() {

  const url = window.location.href;
  const parts = url.split("/");
  const currentElection = parts[parts.length - 1];

  const electionService = new ElectionService();

  const [election, setElection] = useState<Election>();
  const [goToUrl, setGoToUrl] = useState("");


  

  useEffect(() => {
    electionService.GetElectionFromId(Number (currentElection)).then((messages) => {
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
        <button
            onClick={ () => 
              {GoToSigning()}  }>
          Vote for this electiom
        </button>
        </div>
      </view>
    );
  }
}