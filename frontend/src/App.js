import React from 'react'
import './App.css';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from './pages/home/Home';
import SignIn from './pages/signin/SignIn';
import SignUp from './pages/signup/SignUp';
import NavBar from './layouts/navbar/NavBar';
import SearchBar from './layouts/searchbar/SearchBar';
import SearchResults from './pages/searchResults/SearchResults';
import Item from './pages/item/Item';
import UploadImage from './pages/uploadImage/UploadImage';

function App() {

  return (
    <Router>
      <div className="App">
      <NavBar />
      <SearchBar />
        <Switch>
          <Route exact path='/'><Home /></Route>
          <Route path='/results'><SearchResults /></Route>
          <Route path='/item/:id'><Item /></Route>
          <Route path='/upload/:itemId'><UploadImage /></Route>
          <Route path='/signin'><SignIn /></Route>
          <Route path='/signup'><SignUp /></Route>
        </Switch>
      </div>
    </Router>   
  );
}

export default App;
