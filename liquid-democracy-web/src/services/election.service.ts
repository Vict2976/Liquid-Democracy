import axios, { AxiosRequestConfig } from 'axios';
import { CreateCandidate } from '../builder/Interface';


export class ElectionService {

  public async GetElectionFromId(electionId: number): Promise<any> {
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/election/' + electionId,
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

  public async CreateElection(name: string, description: string, CreatedDate: Date, candidates: string[]): Promise<any> {
    var data = JSON.stringify({
      "Name": name,
      "Description": description,
      "CreatedDate": CreatedDate,
      "Candidates": candidates
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


  public async GetAllCandidatesFromElecttion(electionId: number) {
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/Candidate/' + electionId,
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

  public async getAllElections(): Promise<any> {
    var data = ''
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/Election',
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

  public async EndElection(electionId: number): Promise<any> {
    const config: AxiosRequestConfig = {
      method: 'put',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/Election/' + electionId,
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

