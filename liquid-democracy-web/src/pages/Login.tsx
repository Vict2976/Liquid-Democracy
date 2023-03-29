import '../styling/Login.css'
import React, { useState } from 'react';
import { AppService } from '../services/app.service';
import { useNavigate } from 'react-router-dom';
import { UserService } from '../services/user.service';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const userService = new UserService();

  const navigate = useNavigate();
  
  const goToHome= () => {
    navigate('/'); 
  };

  const submit = (e: React.FormEvent) => {
      let promise = userService.Login(username, password);
      promise.catch( () => alert("Wrong credentials"))
      promise.then((response) => {
        sessionStorage.setItem("userId", response.userId)
        userService.AddMitIdSession(response.userId);
      })
  };

  return (
    <div className="App">
      <header className="App-header">
        <h2>Sign In</h2>
          <label htmlFor="username">Username</label>
          <input 
            type="text"
            placeholder="Username"
            name="username"
            required
            onChange={e => setUsername(e.target.value)}
          />
          <label htmlFor="password">Password</label>
          <input
            type="password"
            placeholder="Password"
            name="password"
            required
            onChange={e => setPassword(e.target.value)}
          />
          <button onClick={submit}>Login</button>
      </header>
   </div>
  );
}
export default Login