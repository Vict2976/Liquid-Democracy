import axios, { AxiosRequestConfig } from 'axios';
import { CreateCandidate } from '../builder/Interface';


export class VoteService {

  public async PostVote(userId: number , electionId: number) : Promise<any> {
      var data = JSON.stringify({
          "userId": userId,
          "electionId": electionId,
        }); 
    
      const config: AxiosRequestConfig = {
      method: 'post',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/Vote',
      headers: { 
          'Content-Type': 'application/json'
        },
      data : data
    };

    try {
        const data = await axios(config).then((response) => response.data);
        return data;
    } catch (error) {
        console.log(error);
        return -1;
    }
  }


  public async postVoteUsedOnCandidate(voteId: number , candidateId: number) : Promise<any> {
    var data = JSON.stringify({
      "voteId" : voteId,
      "candidateId" : candidateId
    });

    const config: AxiosRequestConfig = {
      method: 'post',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/ForCandidate',
      headers: { 
          'Content-Type': 'application/json'
        },
      data : data
    };

    try {
        const data = await axios(config).then((response) => response.data);
        return data;
    } catch (error) {
        console.log(error);
    }
  }

  public async postVoteUsedOnDelegate(voteId: number , delegateId: number, electionId:number) : Promise<any> {
    var data = JSON.stringify({
      "voteId" : voteId,
      "delegateId" : delegateId,
      "electionId": electionId
    });

    const config: AxiosRequestConfig = {
      method: 'post',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/ForDelegate',
      headers: { 
          'Content-Type': 'application/json'
        },
      data : data
    };

    try {
        const data = await axios(config).then((response) => response.data);
        return data;
    } catch (error) {
        console.log(error);
    }
  }

}

