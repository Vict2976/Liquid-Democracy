import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import { ElectionService } from "../services/election.service";
import { Election } from "../builder/Interface";

//Får Id med videre og så kalde en fetch på /GetElectionByID

export default function ElectionFunc() {

  const electionService = new ElectionService(); 

  const [election, setElection] = useState<Election>();
  let { state } = useLocation();
  let id = Number(state);
  /*     const singleElection = () => {
        GetElectionFromId(Number(state)).then((elect) => {
            setElection(elect)
          });
    } */

  useEffect(() => {
    electionService.GetElectionFromId(id).then((messages) => {
      console.log("HHHER:::" + messages);
      setElection(messages);
    });
  }, []);

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
    </view>
  );
}