import axios, { AxiosRequestConfig } from 'axios';

export class UserService {
  
  public async Register(username : string, email : string, password : string) : Promise<any> {
    var data = JSON.stringify({
        "username": username,
        "email": email,
        "password": password
      });    
      const config: AxiosRequestConfig = {
        method: 'post',
      maxBodyLength: Infinity,
        url: 'https://localhost:7236/register',
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
        return Promise.reject();
    }
  }

  public async Login(username : string, password : string) : Promise<any> {
    var data = JSON.stringify({
        "username": username,
        "password": password
      });    
      const config: AxiosRequestConfig = {
        method: 'post',
      maxBodyLength: Infinity,
        url: 'https://localhost:7236/login',
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
        return Promise.reject();
    }
  }
}
