import React from 'react'
import './App.css';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from './pages/home/Home';
import SignIn from './pages/signin/SignIn';
import SignUp from './pages/signup/SignUp';
import NavBar from './layouts/navbar/NavBar';
import SearchResults from './pages/searchResults/SearchResults';
import Item from './pages/item/Item';
import UploadImage from './pages/uploadImage/UploadImage';
import Basket from './pages/basket/Basket';
import Sell from './pages/sell/Sell';
import ProtectedRoute from './ProtectedRoute';
import Checkout from './pages/checkout/Checkout';
import Order from './pages/order/Order';
import AlertMessage from './components/alertMessage/AlertMessage';
import SellingItems from './pages/sellingItems/SellingItems';

function App() {
  
  return (
    <Router>
      <div className="App">
      <NavBar />
        <AlertMessage />
        <Switch>
          <Route exact path='/'><Home /></Route>
          <Route path='/results'><SearchResults /></Route>
          <Route path='/item/:id'><Item /></Route>
          <Route path='/signin'><SignIn /></Route>
          <Route path='/signup'><SignUp /></Route>
          <Route path='/basket'><Basket /></Route>
          <Route path='/selling'><SellingItems /></Route>
          <ProtectedRoute path='/checkout' component={Checkout} />
          <ProtectedRoute path='/order/:orderId' component={Order} />
          <ProtectedRoute path='/sell' component={Sell} />
          <ProtectedRoute path='/upload/:itemId' component={UploadImage} />
        </Switch>
      </div>
    </Router>   
  );
}

export default App
