import axios, { AxiosRequestConfig } from 'axios';


export class ElectionService {
  
  public async postElection() : Promise<any> {
    var data = ''
    const config: AxiosRequestConfig = {
      method: 'post',
    maxBodyLength: Infinity,
      url: 'https://localhost:7236/Election/',
      headers: { }
    };
    try {
      const data = await axios(config).then((response) => response.data);
      return data;

export async function GetElectionFromId(electionId: number) : Promise<any> {
    var data = JSON.stringify({
        "electionId": electionId
        });

    const config: AxiosRequestConfig = {
    method: 'get',
    maxBodyLength: Infinity,
    url: 'https://localhost:7236/Election/' + String(electionId),
    headers: { 
        'Content-Type': 'application/json'
    },
    //data : data
    };

    try {
        const data = await axios(config).then((response) => response.data);
        return data;

    } catch (error) {
        console.log(error);
        return Promise.reject();
    }

  }
}
    }



