import React, { useState } from 'react';
import { UserService } from '../services/user.service';
import { useNavigate } from 'react-router-dom';

function Register() {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const userService = new UserService();

  const navigate = useNavigate();
  
  const goToLogin= () => {
    navigate('/login'); 
  };

  const submit = (e: React.FormEvent) => {

    if(!email.includes('@')){
      alert("Wrong Email Format")
      return
    }

    let promise = userService.Register(username, email, password);
    promise.catch( () => alert("An error occured, all input fields must be filled"))
    promise.then((response) => {
        goToLogin()
        sessionStorage.setItem("userId", response.userId)
    })
    };

return (
    <div className="App">
      <header className="App-header">
      <h2>Sign Up</h2>
        <label htmlFor="username">Username</label>
        <input 
          type="text"
          placeholder="Username"
          name="username"
          required
          onChange={e => setUsername(e.target.value)}
        />
        <label htmlFor="email">Email</label>
        <input 
          type="text"
          placeholder="Email"
          name="email"
          required
          onChange={e => setEmail(e.target.value)}
        />
        <label htmlFor="password">Password</label>
        <input
          type="password"
          placeholder="Password"
          name="password"
          required
          onChange={e => setPassword(e.target.value)}
        />
        <button onClick={submit}>Sign Up</button>
      </header>
    </div>
);
}
export default Register;
