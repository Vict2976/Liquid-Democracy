import {Link, useLocation} from "react-router-dom";

interface IElection {
    ElectionId: number;
}

//Får Id med videre og så kalde en fetch på /GetElectionByID


export default function Election(){
    let { state } = useLocation();
    return (
        <view>
            <button onClick={()=> console.log(state)}>
                Tryk mig
            </button>
        </view>
      );
}