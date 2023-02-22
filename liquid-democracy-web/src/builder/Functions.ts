import { Election } from "./Interface";


export function getElectionsArray(json: any) : Election[]{
    let returnArrayOfObject = new Array(json.length);

    for (let i = 0; i< json.length; i++){
        let userId = json[i].userId;
        let name = json[i].name;
        let votings = json[i].votings;
        let elections = json[i].elections;

        let messageObj = {
            userId: userId,
            name: name,
            votings: votings,
            elections: elections,
        }
        returnArrayOfObject[i] = messageObj;
    }
    return returnArrayOfObject;
}