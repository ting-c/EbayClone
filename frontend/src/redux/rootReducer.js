import userReducer from './auth/userReducer';
import jwtReducer from './auth/jwtReducer';
import basketReducer from './basket/basketReducer';
import displayItemsReducer from './displayItems/displayItemsReducer';

const initialState = {
	user: null,
	jwt: null,
	basket: [],
	displayItems: []
};

export default function rootReducer(state = initialState, action) {
	return {
		user : userReducer(state.user, action),
		jwt : jwtReducer(state.jwt, action),
		basket : basketReducer(state.basket, action),
		displayItems : displayItemsReducer(state.displayItems, action),
	}
}