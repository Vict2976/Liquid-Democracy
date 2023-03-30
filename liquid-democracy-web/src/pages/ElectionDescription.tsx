import { useState, useEffect } from "react";
import { ElectionService } from "../services/election.service";
import { Election } from "../builder/Interface";
import { AuthenticateService } from "../services/authenticate.service";
import '../styling/ElectionDescription.css';


function endDate(created: string): string {
  const dateObj = new Date(created);
  dateObj.setDate(dateObj.getDate() + 14); 
  return dateObj.toISOString().slice(0, 10); 
}

function GoToSigning() {
  const url = window.location.href;
  const parts = url.split("/");
  const currentElection = parts[parts.length - 1];
  const authenticateService = new AuthenticateService();
  const data = authenticateService.Authenticate(Number(currentElection)).then((response) => response)
  return data
}

export default function ElectionDescription() {
  const electionService = new ElectionService();

  const url = window.location.href;
  const parts = url.split("/");
  const currentElection = parts[parts.length - 1];

  const [election, setElection] = useState<Election>();

  useEffect(() => {
    electionService.GetElectionFromId(Number(currentElection)).then((messages) => {
      setElection(messages);
    });
  }, []);

  if (election?.isEnded) {
    return (
      <h1>Election has ended</h1>
    )
  }else if (election != null){
    return (
      <view>

        <div className="Top-bar-container">
          <div className="Title"> {election.name}
          </div>
        </div>

        <div className="Body-container">

          <div className="Description-container">
            <p className="Lower-title"> Description</p>
            {election.description}
          </div>

          <div className = "Date-container">
          <p className="Lower-title"> Created date:</p>
            {election.createdDate}
          <p className="Lower-title"> Ending date:</p>
            {endDate(election.createdDate)}
          </div>

        <div className="Button-container">
          <button className="Vote-button"
            onClick={() => { GoToSigning() }}>
            Vote for this election
          </button>
        </div>
        </div>

      </view >
    );
  }else {
    return(
      <div></div>
    )
  }
}