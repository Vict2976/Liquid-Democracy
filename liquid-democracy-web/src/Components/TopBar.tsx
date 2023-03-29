import Button from 'react-bootstrap/Button';
import '../styling/TopBar.css';

export default function TopBar() {
  return (
    <div>
      <div className="Container">
        <div className='TitleAndButton'>
          <div className="Title"> Verified Elections</div>
          <button className="CreateButton" onClick={ () => window.location.href = "/CreateElection"} > Create new election </button>{' '}
        </div>
      </div>
    </div>
  );
}


{/* <div>
<button className="top-bar-button" onClick={() => window.location.replace("/CreateElection")}> Create New Election </button>
</div> */}