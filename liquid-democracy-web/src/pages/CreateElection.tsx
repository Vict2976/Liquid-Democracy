import React from 'react';
import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { Election } from '../builder/Interface';
import { AppService } from '../services/app.service';
import { CreateElectionService } from '../services/createElection.service';


export function getCurrentDate(separator=''){
    let newDate = new Date()
    let date = newDate.getDate();
    let month = newDate.getMonth() + 1;
    let year = newDate.getFullYear();
    
    return `${date}` + "/" +  `${separator}${month<10?`0${month}`:`${month}`}${separator}` + "-" +`${year}`
}


function CreateElection() {
    const [elections, setElection] = useState<Election[]>();
   
    const createElectionService = new CreateElectionService(); 

    const [name, setName] = useState('');
    const [userId, setUserId] = useState('');


    const [desciption, setDescription] = useState('');
    //const [date, setDate] = useState('');
    //const [possibleAnswer, setpossibleAnswer] = useState('');



    const createElection = (e: React.FormEvent) => {
        let promise = createElectionService.CreateElection(name, userId);
        promise.catch( () => alert("An error occured, all input fields must be filled"))
        promise.then((response) => {
            setUserId(response);
            })
        };

    /*
    Overskrift
    dato
    beskrivelse
    svarmuligheder
    */

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
                <form>
                <div className='multAnswer'>
                    <form>

                        <input 
                            type={"text"}
                            placeholder="Write answer"
                            onChange={e => setName(e.target.value)} />
                    </form>
                </div>


              
                </form>

                <Button 
                    onClick={()=> createElectionService.CreateElection(name, 1)}>
                    Press to confirm
                </Button>
            </div>
        </div>
    );
}

/*type CheckProps = {
    label: string;
    value: boolean;
    onChange: any;
  };

const Checkbox: React.FunctionComponent<CheckProps> = ({label, value, onChange }) => {
    return (
      <label>
        <input type="checkbox" checked={value} onChange={onChange} />
        {label}
      </label>


    );
  };*/

  interface InputProps {
    placeholder: string;
  }
  
  const Input: React.FC<InputProps> = ({ placeholder }) => {
    return <input placeholder={placeholder} />;
  };
  
const Form: React.FC = () => {
    const [inputList, setInputList] = useState<JSX.Element[]>([]);
  
    const onAddBtnClick = (event: React.MouseEvent<HTMLButtonElement>) => {
      setInputList(inputList.concat(<Input key={inputList.length} placeholder="Your input here" />));
    };
  
    return (
      <div>
        <button onClick={onAddBtnClick}>Add input</button>
        {inputList}
      </div>
    );
  };



export default CreateElection
