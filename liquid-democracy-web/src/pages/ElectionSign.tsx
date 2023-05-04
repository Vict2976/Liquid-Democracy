import { useState, useEffect } from "react";
import { ElectionService } from "../services/election.service";
import { Election, Candidate } from "../builder/Interface";
import { AuthenticateService } from "../services/authenticate.service";
import { SigningService } from "../services/signing.service";
import '../styling/ElectionSign.css';


async function finalizeSign(electionId: number, candidateId: number) {
  const signingService = new SigningService()
  const authenticateService = new AuthenticateService()

  const sessionId = sessionStorage.getItem("sessionId")
  if (sessionId == "" || sessionId == undefined) {
    alert("Your session has expired")
    return
  }
  const sessionResponse = await authenticateService.GetSessionInformation(sessionId)
  const UUID = sessionResponse.identity.attributes["mitid.uuid"]
  const status = sessionResponse.status
  console.log(status);
  if (status != "success"){
    return
  }
  signingService.SignVoteForCandidate(UUID, electionId, candidateId)
}

export default function ElectionSign() {
  const url = window.location.href;
  const parts = url.split("/");
  const Url = parts[parts.length - 1];
  const currentElection = Url.charAt(0)
  const electionService = new ElectionService();
  const authenticateService = new AuthenticateService()
  const sessionId = sessionStorage.getItem("sessionId")

  const [election, setElection] = useState<Election>();
  const [candidates, setCandidates] = useState<Candidate[]>();
  const [isVerifiedWithMitId, setIsVerifiedWithMitId] = useState(false)

  useEffect(() => {
    const verifyUser = async () => {
      if (sessionId == "" || sessionId == undefined) {
        console.log("IS UNDIFERINED")
        setIsVerifiedWithMitId(false)
        return
      }
      const sessionResponse = await authenticateService.GetSessionInformation(sessionId)
      const status = sessionResponse.status
      if (status == "success") {
        setIsVerifiedWithMitId(true)
      } else {
        setIsVerifiedWithMitId(false)
      }

    };
    verifyUser();
  }, []);

  useEffect(() => {
    electionService.GetElectionFromId(Number(currentElection)).then((messages) => {
      setElection(messages);
    });
  }, []);

  useEffect(() => {
    electionService.GetAllCandidatesFromElecttion(Number(currentElection)).then((candidates) => {
      setCandidates(candidates);
    });
  }, []);

  if (isVerifiedWithMitId == false) {
    return (
      <div></div>
    )
  } else if (election?.isEnded) {
    return (
      <h1>Election has ended</h1>
    )
  } else if (candidates != undefined && election != undefined) {
    return (
      <view>

        <div className="Top-bar-container">
          <div className="Title"> {election.name} </div>
        </div>

        <div className="Description-container">
          <p>On this page you can see the candidates for the chosen election. To vote for a candidate, you must click the sign button below the chosen candidate, and then finalie the signing process. Oncee signed, a confirmation email will be sent</p>
        </div>

        <div className="Body-container">
          <h3>Candidates:</h3>
          <div className="Candidates-container">
            {candidates.map((can) => (
              <ul>
                <li>{can.name}</li>
                <button className="Sign-button" onClick={() => finalizeSign(election.electionId, can.candidateId)}> Sign vote for: {can.name}</button>
              </ul>
            ))}
          </div>
        </div>

      </view>
    );
  } else {
    return (
      <div className="page">
        <p>Error</p>
      </div>
    );
  }
}