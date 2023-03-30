import { useContext, useEffect, useState } from 'react';
import { Election } from '../builder/Interface';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import '../styling/HomePage.css';
import '../styling/TopBar.css';
import { ElectionService } from '../services/election.service';

function HomePage() {
  const [elections, setElections] = useState<Election[]>();

  const electionService = new ElectionService();

  useEffect(() => {
    electionService.getAllElections().then((response) => {
      //console.log(response);
      setElections(response);
    });
  }, []);

  if (elections != undefined) {
    return (
      <div className="Home-page-container">

        <div className="Container">
          <div className='TitleAndButton'>
            <div className="Title"> Verified Elections</div>
            <button className="TopButton" onClick={() => window.location.href = "/CreateElection"}><span>Create new election</span><i></i></button>
          </div>
        </div>

        <Grid container spacing={2} className='grid-box'>
          {elections.map((ele) => (
            <Grid item xs={12} sm={6} md={3} key={ele.electionId}>
              <Card className='single-card' onClick={ () => window.location.href = "/Election/" + ele.electionId}>
                <div className="card-content-wrapper">
                  <div className="card-header">
                    <Typography gutterBottom variant="h5" component="div">
                      {ele.name}
                    </Typography>
                  </div>
                  <Typography className="card-date">
                    {ele.createdDate}
                  </Typography>
                </div>
              </Card>

            </Grid>
          ))}
        </Grid>
      </div>
    );
  } else {
    return (
      <div className="page">
        <p></p>
      </div>
    );
  }
}

export default HomePage           
