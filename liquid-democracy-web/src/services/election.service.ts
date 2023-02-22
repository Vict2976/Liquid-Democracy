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
    } catch (error) {
        console.log(error);
        return Promise.reject();
    }
  }
}





