import { VoteService } from "../services/vote.service";


function VoteAfterMitId() {  
  localStorage.removeItem("sessionId");

  var electionId = localStorage.getItem("electionId");
  var delegateId = localStorage.getItem("delegateId");
  var candidateId = localStorage.getItem("candidateId");
  var userId = localStorage.getItem("userId");

  var voteService = new VoteService();

  function postVote(){
    //make a new vote in the vote table with the userName   
    var response = voteService.PostVote(Number(userId), Number(electionId));
     response.then((data)=> {
      if (data == -1){
      alert("You already have a vote for this election")
      }else if (candidateId != ""){
        //insert data into voteUsedOn with candidateId
        voteService.postVoteUsedOnCandidate(data.voteId, Number(candidateId));
        console.log("You successfully voted for a CANDIDATE")

      } else {
        //voteUsed on with delegateId
        voteService.postVoteUsedOnDelegate(data.voteId, Number(delegateId), Number(electionId));
        console.log("You successfully voted for a DELEGATE")
      }
    
    })
  } 

  if (candidateId != ""){
    //så skal der stemmes på candidateId
    return (
      <div>
        <button onClick={()=> postVote()}>
          Confirm vote for candidate
        </button>

      </div>
    )
  } else {
    //Så skal der stemmes på delegate
    return (
      <div>
        <button onClick={()=>postVote()}>
          Confirm vote for delegate
        </button>
      </div>
    )
  }

  }
  export default VoteAfterMitId;
