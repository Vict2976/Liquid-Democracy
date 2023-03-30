import { useContext, useEffect, useState } from 'react';
import { Election } from '../builder/Interface';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import '../styling/HomePage.css';
import TopBar from '../Components/TopBar';
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
      <div className="App">
        <header className="App-header">
          <TopBar />
          <Grid container spacing={2} className='grid-box'>
            {elections.map((ele) => (
              <Grid item xs={12} sm={6} md={3} key={ele.electionId}>
                <Card className='single-card'>
                  <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                      {ele.name}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {ele.description}
                    </Typography>
                  </CardContent>
                  <CardActions>
                    <p>
                      <a href={"/Election/" + ele.electionId}> Hallo </a>
                    </p>
                    <Typography >
                      {ele.createdDate}
                    </Typography>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        </header>
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
