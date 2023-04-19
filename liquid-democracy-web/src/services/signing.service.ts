
import axios, { AxiosRequestConfig } from 'axios';
import { isRouteErrorResponse } from 'react-router-dom';

export class SigningService {

  public async SignVoteForCandidate(providerId: number, electionId: any, candidateId: number): Promise<any> {
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/Sign/Ballot/' + providerId + "/" + electionId + "/" + candidateId,
      headers: {}
    };
    try {
      const URL = await axios(config).then((response) =>
        response.request.responseURL)
      window.location.href = (URL)
    } catch (error) {
      console.log(error);
      return Promise.reject();
    }
  }
}