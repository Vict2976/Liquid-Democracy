import axios, { AxiosRequestConfig } from 'axios';

export class UserService {


  public async CreateUser(){
    const sessionId = sessionStorage.getItem("sessionId")
    if (sessionId == "" || sessionId == undefined){
      this.ExecuteMitIdSession()
    }else{
      this.GetSessionInformation(String(sessionId)).then((sessionResponse) => {
        const status = sessionResponse.status
        if(status == "success"){
          sessionStorage.clear()
          const providerId = sessionResponse.identity.providerId
          const sessionExpires = sessionResponse.expires
          this.UserExists(providerId).then((userResponse) => {
            console.log(userResponse)
            if(userResponse == 204){
              this.Register(providerId, sessionExpires)
              alert("User has been created - yay")
            }else{
              alert("User exists - check for vote")
            }
          })          
        }else{
          alert("Your Sessions is not succesfull")
        }
      });
    }
  }


  public async ExecuteMitIdSession() : Promise<any> { 
    const config: AxiosRequestConfig = {
      method: 'get',
    maxBodyLength: Infinity,
      url: 'https://localhost:7236/MitId/Auth',
      headers: { 
        'Content-Type': 'application/json'
      },
    };
  try {
    const data = await axios(config).then((response) => response.data);
    sessionStorage.setItem("sessionId", data.id)
    window.open(data.url)
    return data
  } catch (error) {
      console.log(error);
      return Promise.reject();
  }
}
  
  public async GetSessionInformation(sessionId : string){
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/MitId/Session/' + sessionId,
      headers: { 
        'Content-Type': 'application/json'
      },
    };
    try {
      const data = await axios(config).then((response) => response.data);
      return data
    } catch (error) {
      console.log(error);
      return Promise.reject();
    }
  }

  public async UserExists(sessionId : string){
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: '  https://localhost:7236/GetUser/' + sessionId,
      headers: { 
        'Content-Type': 'application/json'
      },
    };
    try {
      const data = await axios(config).then((response) => response.status);
      return data
    } catch (error) {
      console.log(error);
      return Promise.reject();
    }
  }

  public async Register(providerId : string, sessionExpires : Date){
    var data = JSON.stringify({
      "proiverId": providerId,
      "sessionExpires": sessionExpires
    });

    const config: AxiosRequestConfig = {
      method: 'post',
    maxBodyLength: Infinity,
      url: 'https://localhost:7236/User',
      headers: { 
        'Content-Type': 'application/json'
      },
      data : data
    };
    try {
      const data = await axios(config).then((response) => response.data);
      return data
   } catch (error) {
      console.log(error);
      return Promise.reject();
    }
  }

  public async UpdateSession(providerId : string, sessionExpires : Date){
    var data = JSON.stringify({
      "proiverId": providerId,
      "sessionExpires": sessionExpires
    });

    const config: AxiosRequestConfig = {
      method: 'put',
    maxBodyLength: Infinity,
      url: 'https://localhost:7236/UpdateSession',
      headers: { 
        'Content-Type': 'application/json'
      },
      data : data
    };
    try {
      const data = await axios(config).then((response) => response.data);
      return data
   } catch (error) {
      console.log(error);
      return Promise.reject();
    }
  }
}




