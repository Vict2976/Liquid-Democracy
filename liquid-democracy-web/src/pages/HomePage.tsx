import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { getElectionsArray } from '../builder/Functions';
import { Election } from '../builder/Interface';
import { AppService } from '../services/app.service';


function HomePage() {
  const [elections, setElections] = useState<Election[]>();

  const appService = new AppService();

  useEffect(() => {
      appService.getAllElections().then((response) => {
      console.log(response);
      setElections(response);
    });
  }, []);

  if (elections != undefined) {
  return (
    <div className="App">
      <header className="App-header">
        {elections.map((ele) => (
          <view key={ele.electionid}>
            <view>
            <Card style={{ width: '18rem' }}>
              <Card.Img variant="top" src="holder.js/100px180" />
              <Card.Body>
                <Card.Title>{ele.name}</Card.Title>
                <Card.Text>
                  Votes: {ele.amountOfVotes}
                </Card.Text>
                <Button
                  onClick={ () => {
                    console.log(elections);
                  }}
                >
                Cliek here
                </Button>
              </Card.Body>
            </Card>
            </view>
          </view>))}
    </header>
    </div>
  );
  }else {
    return (
      <div className="page">
        <p>Error</p>
      </div>
    );
  }
}

export default HomePage
