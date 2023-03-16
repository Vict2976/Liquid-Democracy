import axios, { AxiosRequestConfig } from 'axios';
import { isRouteErrorResponse } from 'react-router-dom';

export class AdminService {
  
  public async VerifyElection(electionId : number)  : Promise<any> {
      const config: AxiosRequestConfig = {
        method: 'get',
        maxBodyLength: Infinity,
        url: 'https://localhost:7236/Verify/' + electionId,
        headers: { }
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

