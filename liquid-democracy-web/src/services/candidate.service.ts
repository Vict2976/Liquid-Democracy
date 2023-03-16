import axios, { AxiosRequestConfig } from 'axios';

export class CandidateService {
  
  public async CountVotesForCandidates(electionId : number)  : Promise<any> {
      const config: AxiosRequestConfig = {
        method: 'get',
        maxBodyLength: Infinity,
        url: 'https://localhost:7236/Candidate/countVoters/' + electionId,
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

  public async FindCandidateWinner(electionId : number)  : Promise<any> {
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/Candidate/findWinner/' + electionId,
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

