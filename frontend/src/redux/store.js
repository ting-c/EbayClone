import { createStore } from 'react-redux';
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
	user: loadFromLocalStorage().user || null,
	jwt: loadFromLocalStorage().jwt || null,
	basket: loadFromLocalStorage().basket || null,
	displayItems: loadFromLocalStorage().basket || null
};

const store = createStore(rootReducer, initialState);

// listen for store changes and use saveToLocalStorage to
// save them to localStorage
store.subscribe(() => saveToLocalStorage(store.getState()));

export default store;