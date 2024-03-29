import axios, { AxiosRequestConfig } from 'axios';

export class VoteService {

  public async PostVote(userId: number, electionId: number): Promise<any> {
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
      data: data
    };

    try {
      const data = await axios(config).then((response) => response.data);
      return data;
    } catch (error) {
      console.log(error);
      return -1;
    }
  }

  public async postVoteUsedOnCandidate(voteId: number, candidateId: number): Promise<any> {
    var data = JSON.stringify({
      "voteId": voteId,
      "candidateId": candidateId
    });

    const config: AxiosRequestConfig = {
      method: 'post',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/ForCandidate',
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
    }
  }

  public async CountAllVotesForElection(electionId: number): Promise<any> {
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/countVoters/' + electionId,
      headers: {}
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