export class AppService {
  
  public async registerUser(username : string, email : string, pw : string) : Promise<any> {
    alert ("heyo")
  }

  public async Login(username: string, password : string ) : Promise<any> {
    const data = { username: username, pw:password };
    alert(data)
  }
}
