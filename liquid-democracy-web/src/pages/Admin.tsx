import { Component } from 'react';
import { Election } from '../builder/Interface';
import { AdminService } from '../services/admin.service';
import { CandidateService } from '../services/candidate.service';
import { ElectionService } from '../services/election.service';
import { VoteService } from '../services/vote.service';
import '../styling/HomePage.css';

interface AdminProps { }

interface AdminState {
  elections: Election[] | undefined;
}

class Admin extends Component<AdminProps, AdminState> {
  electionService = new ElectionService();
  adminService = new AdminService();
  candidateService = new CandidateService();
  voteService = new VoteService();

  constructor(props: AdminProps) {
    super(props);
    this.state = {
      elections: undefined
    };
  }

  VerifyElection = async (electionId: number) => {
    const isVerified = this.adminService.VerifyElection(electionId).then((response) =>
      response
    )
    if (await isVerified) {
      alert("This election is Verified, you can close it!")
    } else {
      alert("This elction has been tampered with!!")
    }
  }

  FindElectionWinner = async (electionId: number) => {
    const candidateWinnder = this.candidateService.FindCandidateWinner(electionId).then((response) => response)
    alert(await candidateWinnder)
  }

  VerifyAmountOfVotesInSystem = async (electionId: number) => {
    const candidateVotes = this.candidateService.CountVotesForCandidates(electionId).then((response) => response);
    const amountOfVotes = this.voteService.CountAllVotesForElection(electionId).then((response) => response);
    if (await candidateVotes == await amountOfVotes) {
      alert("This election is has correct votes, you can close it!")
    } else {
      alert("This elction has been tampered with!!")
    }
  }

  VerifyRootHash = async (electionId: number) => {
    const isVerified = this.adminService.VerifyRootHash(electionId).then((response) =>
      response
    )
    if (await isVerified) {
      alert("This election is Verified, you can close it!")
    } else {
      alert("This elction has been tampered with!!")
    }
  }

  CloseElection = (election: Election) => {
    if (election.isEnded) {
      alert("Election has already ended");
    } else {
      this.forceUpdate()
      this.electionService.EndElection(election.electionId);
    }
  };

  componentDidMount() {
    this.electionService.getAllElections().then((response) => {
      this.setState({ elections: response });
    });
  }

  render() {
    const { elections } = this.state;

    if (elections != undefined) {
      return (
        <div className="App">
          <header className="App-header">
            {elections.map((ele) => (
              <div key={ele.electionId}>
                {ele.name}
                <button onClick={() => this.VerifyElection(ele.electionId)}>Verify the elction signatures</button>
                <button onClick={() => this.VerifyRootHash(ele.electionId)}>Verify the root hash of election</button>
                <button onClick={() => this.VerifyAmountOfVotesInSystem(ele.electionId)}>Verify amount of votes in system election</button>
                <button onClick={() => this.FindElectionWinner(ele.electionId)}>Find Winner</button>
                <button onClick={() => this.CloseElection(ele)}>End election</button>
              </div>
            ))}
          </header>
        </div>
      );
    } else {
      return (
        <div className="page">
          <p>Sry, we couldnt find any elections</p>
        </div>
      );
    }
  }
}

export default Admin;