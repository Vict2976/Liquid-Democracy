import { useState, useEffect } from "react";
import { ElectionService } from "../services/election.service";
import { Election, Candidate } from "../builder/Interface";
import { AuthenticateService } from "../services/authenticate.service";
import { SigningService } from "../services/signing.service";

async function finalizeSign(electionId: number, candidateId: number) {
  const signingService = new SigningService()
  const authenticateService = new AuthenticateService()

  const sessionId = sessionStorage.getItem("sessionId")
  if (sessionId == "" || sessionId == undefined) {
    alert("Your session has expired")
    return
  }
  const sessionResponse = await authenticateService.GetSessionInformation(sessionId)
  const proivderId = sessionResponse.identity.providerId
  signingService.SignVoteForCandidate(proivderId, electionId, candidateId)
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
        <ul>
          <li>{election.name}</li>
          <li>{election.description}</li>
          <li>{election.createdDate}</li>
          <li>{election.electionId}</li>
          <li>{election.votings}</li>
        </ul>

        <div>
          <h3>Candidates</h3>
          {candidates.map((can) => (
            <ul>
              <li>{can.name}</li>
              <button onClick={() => finalizeSign(election.electionId, can.candidateId)}> Vote For: {can.name}</button>
            </ul>
          ))}
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