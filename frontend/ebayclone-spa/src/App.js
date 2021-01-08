import React, { useState } from 'react'
import './App.css';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from './pages/home/Home';
import SignIn from './pages/signin/SignIn';
import SignUp from './pages/signup/SignUp';
import NavBar from './layouts/navbar/NavBar';
import SearchBar from './layouts/searchbar/SearchBar';
import SearchResults from './pages/searchResults/SearchResults';

function App() {

  const [items, setItems] = useState([]);

  return (
    <Router>
      <NavBar />
      <div className="App">
        <SearchBar setItems={setItems}/>
        <Switch>
          <Route path='/'><Home /></Route>
          <Route path='/'><SearchResults items={items}/></Route>
          <Route path='/signin'><SignIn /></Route>
          <Route path='/signup'><SignUp /></Route>
        </Switch>
      </div>
    </Router>   
  );
}

export default App;
