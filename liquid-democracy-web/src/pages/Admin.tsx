import { Component } from 'react';
import { Election } from '../builder/Interface';
import { AppService } from '../services/app.service';
import '../styling/HomePage.css';

interface AdminProps {}

interface AdminState {
  elections: Election[] | undefined;
}

class Admin extends Component<AdminProps, AdminState> {
  appService = new AppService();

  constructor(props: AdminProps) {
    super(props);
    this.state = {
      elections: undefined
    };
  }

  CloseElection = (election: Election) => {
    if (election.isEnded) {
      alert("Election has already ended");
    } else {
      this.forceUpdate()
      this.appService.EndElection(election.electionId);
    }
  };

  componentDidMount() {
    this.appService.getAllElections().then((response) => {
      //console.log(response);
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
                <button onClick={() => this.CloseElection(ele)}>Click here to end election</button>
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