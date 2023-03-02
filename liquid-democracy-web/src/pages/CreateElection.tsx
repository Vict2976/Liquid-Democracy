import React from 'react';
import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { Election } from '../builder/Interface';
import { ElectionService } from '../services/election.service';




interface CheckboxProps {
  label: string;
}

interface CheckboxListProps {
  checkboxes: CheckboxProps[];
  onAddCheckbox: () => void;
  onRemoveCheckbox: (index: number) => void;
  onUpdateCheckboxLabel: (index: number, label: string) => void;
}

const CheckboxList: React.FC<CheckboxListProps> = ({
  checkboxes,
  onAddCheckbox,
  onRemoveCheckbox,
  onUpdateCheckboxLabel,
}) => {
  const handleAddCheckbox = () => {
    onAddCheckbox();
  };

  const handleRemoveCheckbox = (index: number) => {
    onRemoveCheckbox(index);
  };

  const [candidates, setCandidates] = useState('');
  const handleUpdateCheckboxLabel = (index: number, event: React.ChangeEvent<HTMLInputElement>) => {
    onUpdateCheckboxLabel(index, event.target.value);
    setCandidates(event.target.value)
  };

  
  return (
    <div>
      {checkboxes.map((checkbox, index) => (
        <div key={index}>
          <input
            type="text"
            placeholder={`Candidate ${index + 1}`}
            value={checkbox.label}
            onChange={(event) => handleUpdateCheckboxLabel(index, event)}
          />
          <button onClick={() => handleRemoveCheckbox(index)}>Remove</button>
        </div>
      ))}
      <button onClick={handleAddCheckbox}>Add Checkbox</button>
    </div>
  );
};

export function getCurrentDate(separator=''){
    let newDate = new Date()
    let date = newDate.getDate();
    let month = newDate.getMonth() + 1;
    let year = newDate.getFullYear();
    
    return `${date}` + "/" +  `${separator}${month<10?`0${month}`:`${month}`}${separator}` + "-" +`${year}`
}



function CreateElection() {
  const [checkboxes, setCheckboxes] = useState<CheckboxProps[]>([
    { label: '' },
    { label: '' },
  ]);

  const handleAddCheckbox = () => {
    setCheckboxes([...checkboxes, { label: '' }]);
  };

  const handleRemoveCheckbox = (index: number) => {
    const newCheckboxes = [...checkboxes];
    newCheckboxes.splice(index, 1);
    setCheckboxes(newCheckboxes);
  };

  const handleUpdateCheckboxLabel = (index: number, label: string) => {
    const newCheckboxes = [...checkboxes];
    newCheckboxes[index] = { label };
    setCheckboxes(newCheckboxes);
  };

    const [elections, setElection] = useState<Election[]>();
   
    const electionService = new ElectionService(); 

    const [name, setName] = useState('');
    const [userId, setUserId] = useState('');
    const [desciption, setDescription] = useState('');

  
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
                        onChange={e =>setName(e.target.value)} />
                </form>
                <label className="dateLabel"> {getCurrentDate()}</label>
                <form>
                    <textarea 
                        className='desciption' 
                        rows={4} cols={40}
                        placeholder="Write a description"
                        onChange={e =>setDescription(e.target.value)} />
                </form>

                <div>
                <CheckboxList
                  checkboxes={checkboxes}
                  onAddCheckbox={handleAddCheckbox}
                  onRemoveCheckbox={handleRemoveCheckbox}
                  onUpdateCheckboxLabel={handleUpdateCheckboxLabel}/>
                </div>
                <button 
                    onClick={()=>{ 
                      electionService.CreateElection(name, desciption, new Date())}}
                    //onc ={()=> {window.location.replace("/")}}
                    >
                  Please confirm your choices
                </button>
                <button 
                    onClick={()=>{ 
                      window.location.replace("/")}}
                    >
                  Go back to the other elections
                </button>
            </div>
        </div>
    );
}



export default CreateElection
