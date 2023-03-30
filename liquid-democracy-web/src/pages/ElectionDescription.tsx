import { useState, useEffect } from "react";
import { ElectionService } from "../services/election.service";
import { Election } from "../builder/Interface";
import { AuthenticateService } from "../services/authenticate.service";


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
  } else {
    return (
      <view>
        <ul>
          <li>{election?.name}</li>
          <li>{election?.description}</li>
          <li>{election?.createdDate}</li>
        </ul>

        <div>
          <button
            onClick={() => { GoToSigning() }}>
            Vote for this electiom
          </button>
        </div>
      </view>
    );
  }
}