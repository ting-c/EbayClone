import './App.css';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import SignIn from './components/signin/SignIn';
import SignUp from './components/signup/SignUp';
import NavBar from './components/navbar/NavBar';
import SearchBar from './components/searchbar/SearchBar';


function App() {
  return (
    <Router>
      <NavBar />
      <div className="App">
        <SearchBar />
        <Switch>
          <Route path='/signin'><SignIn /></Route>
          <Route path='/signup'><SignUp /></Route>
        </Switch>
      </div>
    </Router>   
  );
}

export default App;
