import React from 'react';
import { useContext, useEffect, useState } from 'react';
import { Election } from '../builder/Interface';
import { ElectionService } from '../services/election.service';


//bruges til at finde dato
export function getCurrentDate(separator=''){
    let newDate = new Date()
    let date = newDate.getDate();
    let month = newDate.getMonth() + 1;
    let year = newDate.getFullYear();
    
    return `${date}` + "/" +  `${separator}${month<10?`0${month}`:`${month}`}${separator}` + "-" +`${year}`
}


function CreateElection() {
  const [singleCandidate, setSingleCandidates] = useState('');
  const [candidates, setCandidates] = useState<string[]>([]);

  const electionService = new ElectionService(); 
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  const handleAddCandidate = () => {
      setCandidates([...candidates, singleCandidate]);
      setSingleCandidates('');
  }

  const handleConfirm = () => {
      electionService.CreateElection(name, description, new Date(), candidates);
  }

  return(
      <div className='CreateElection' style={{display: 'flex',  justifyContent:'center', alignItems:'center', height: '100vh'}}>
          <div>
              <h1>
                  Make your new election:
              </h1>
              <form>
                  <input 
                      className='name' 
                      type={"text"}
                      placeholder="Election name"
                      onChange={e => setName(e.target.value)} />
              </form>
              <label className="dateLabel"> {getCurrentDate()}</label>
              <form>
                  <textarea 
                      className='description' 
                      rows={4} cols={40}
                      placeholder="Write a description"
                      onChange={e => setDescription(e.target.value)} />
              </form>
              <form>
                  <textarea 
                      className='Add new candidate' 
                      rows={4} cols={40}
                      placeholder="Add new candidate"
                      value={singleCandidate}
                      onChange={e => setSingleCandidates(e.target.value)} />
              </form>
              <button 
                  onClick={handleAddCandidate}
                  >
                Add candidate
              </button>

              <ul>
                {candidates.map(candidate => (
                  <li key={candidate}>{candidate}</li>
                ))}
              </ul>

              <button 
                  onClick={handleConfirm}
                  >
                Please confirm your choices
              </button>
              <button 
                  onClick={() => window.location.replace("/")}
                  >
                Go back to the other elections
              </button>
          </div>
      </div>
  );
}



export default CreateElection
