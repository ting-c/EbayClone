import React, { useState, useEffect } from 'react'
import './App.css';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from './pages/home/Home';
import SignIn from './pages/signin/SignIn';
import SignUp from './pages/signup/SignUp';
import NavBar from './layouts/navbar/NavBar';
import SearchBar from './layouts/searchbar/SearchBar';
import SearchResults from './pages/searchResults/SearchResults';
import Item from './pages/item/Item';

function App() {

  const [items, setItems] = useState(null);
  const [user, setUser] = useState(null);
  const [jwt, setJwt] = useState(null);

  console.log(user, jwt);
  useEffect(() => {
    handleInitialState();
  });

  function handleSetUserAndJwt(user, jwt){
    setUser(user);
    setJwt(jwt);
    localStorage.setItem("user", JSON.stringify(user));
    localStorage.setItem("jwt", jwt);
  }

  function handleInitialState(){
    if (user == null && jwt == null){
      const storedUser = JSON.parse(localStorage.getItem("user"));
      const storedJwt = localStorage.getItem("jwt");
      if (storedUser && storedJwt) 
        setUser(storedUser);
        setJwt(storedJwt);
    }
  }

  return (
    <Router>
      <div className="App">
      <NavBar user={user} setUserAndJwt={handleSetUserAndJwt}/>
      <SearchBar setItems={setItems}/>
        <Switch>
          <Route exact path='/'><Home items={items} setItems={setItems}/></Route>
          <Route path='/results'><SearchResults items={items}/></Route>
          <Route path='/item/:id'><Item /></Route>
          <Route path='/signin'><SignIn setUserAndJwt={handleSetUserAndJwt}/></Route>
          <Route path='/signup'><SignUp /></Route>
        </Switch>
      </div>
    </Router>   
  );
}

export default App;
