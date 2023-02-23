import axios, { AxiosRequestConfig } from 'axios';

export class CreateElectionService {
  public async CreateElection(name: string, userid: any) : Promise<any> {
    var data = JSON.stringify({
      "name": name,
      "userid": userid,
    });  
    const config: AxiosRequestConfig = {
      method: 'post',
    maxBodyLength: Infinity,
      url: 'https://localhost:7236/Election',
      headers: { 
        'Content-Type': 'application/json'
      },
      data: data
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








