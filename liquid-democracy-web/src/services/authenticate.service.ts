import axios, { AxiosRequestConfig } from 'axios';

export class AuthenticateService {

  public async Authenticate(electionId: number): Promise<any> {
    const sessionId = sessionStorage.getItem("sessionId")
    if (sessionId == "" || sessionId == undefined) {
      this.ExecuteMitIdSession(electionId)
    } else {
      this.GetSessionInformation(String(sessionId)).then((sessionResponse) => {
        const status = sessionResponse.status
        if (status == "success") {
          sessionStorage.clear()
        } else {
          alert("Your Sessions is not succesfull, click to start a new session")
          sessionStorage.clear()
          return sessionResponse
        }
      });
    }
  }

  public async ExecuteMitIdSession(electionId: number): Promise<any> {
    const config: AxiosRequestConfig = {
      method: 'get',
      maxBodyLength: Infinity,
      url: 'https://localhost:7236/MitId/Auth/' + electionId,
      headers: {
        'Content-Type': 'application/json'
      },
    };
    try {
      const data = await axios(config).then((response) => response.data);
      sessionStorage.setItem("sessionId", data.id)
      window.location.href = data.url
      return data
    } catch (error) {
      console.log(error);
      return Promise.reject();
    }
  }

  public async GetSessionInformation(sessionId: string) {
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

  public async UpdateSession(providerId: string, sessionExpires: Date) {
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
      data: data
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




