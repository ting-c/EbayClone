import React, { useState } from 'react'
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

  return (
    <Router>
      <div className="App">
      <NavBar user={user}/>
      <SearchBar setItems={setItems}/>
        <Switch>
          <Route exact path='/'><Home items={items} setItems={setItems}/></Route>
          <Route path='/results'><SearchResults items={items}/></Route>
          <Route path='/item/:id'><Item /></Route>
          <Route path='/signin'><SignIn /></Route>
          <Route path='/signup'><SignUp /></Route>
        </Switch>
      </div>
    </Router>   
  );
}

export default App;
