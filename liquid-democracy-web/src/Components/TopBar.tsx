import Button from 'react-bootstrap/Button';
import '../styling/TopBar.css';

export default function TopBar() {
  return (
    <div className="Container">
      <div className='TitleAndButton'>
        <div className="Title"> Verified Elections</div>
        <button className="TopButton" onClick={() => window.location.href = "/CreateElection"}><span>Create new election</span><i></i></button>
      </div>
    </div>
  );
}


{/* <div>
<button className="top-bar-button" onClick={() => window.location.replace("/CreateElection")}> Create New Election </button>
</div> */}