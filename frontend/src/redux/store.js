import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './rootReducer';

// convert object to string and store in localStorage
function saveToLocalStorage(state) {
  try {
    const jsonState = JSON.stringify(state);
    localStorage.setItem("appState", jsonState);
  } catch (err) {
    console.warn(err);
  }
}

// load string from localStarage and convert into an Object
function loadFromLocalStorage() {
  try {
    const jsonState = localStorage.getItem("appState");
    if (jsonState === null) 
      return undefined;
    return JSON.parse(jsonState);
  } catch (e) {
    console.warn(e);
    return undefined;
  }
};

const initialState = {
  user: null,
  jwt: null,
  basket: [],
  displayItems: []
}

const preloadedState = loadFromLocalStorage() || initialState;

const store = createStore(rootReducer, preloadedState, applyMiddleware(thunk));

// listen for store changes and use saveToLocalStorage to
// save them to localStorage
store.subscribe(() => saveToLocalStorage(store.getState()));

export default store;

export { initialState }