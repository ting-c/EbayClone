import { combineReducers } from 'redux';
import userReducer from './auth/userReducer';
import jwtReducer from './auth/jwtReducer';
import basketReducer from './basket/basketReducer';
import displayItemsReducer from './displayItems/displayItemsReducer';
import errorReducer from './error/errorReducer';

export default combineReducers({
	user: userReducer,
	jwt: jwtReducer,
	basket: basketReducer,
	displayItems: displayItemsReducer,
	error: errorReducer
});