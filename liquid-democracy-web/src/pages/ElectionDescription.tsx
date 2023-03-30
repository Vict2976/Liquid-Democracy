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
  } else if (election != null) {
    return (
      <div>

        <div className="Top-bar-container">
          <div className="Title"> {election.name}
          </div>
        </div>

        <div className="Body-container-Description">

          <div className="Inner-container-info-Description">
            <p className="Lower-title-Description"> Description</p>
            {election.description}
          </div>

          <div className="Inner-container-Description">
            <p className="Lower-title-Description"> Created date:</p>
            {election.createdDate}
            <p className="Lower-title-Description"> Ending date:</p>
            {endDate(election.createdDate)}
          </div>

          <div className="Inner-container-button-Description">
            <button className="Vote-button-Description"
              onClick={() => { GoToSigning() }}>
              Vote for this election
            </button>
          </div>

        </div>
      </div>
    );
  } else {
    return (
      <div></div>
    )
  }
}
