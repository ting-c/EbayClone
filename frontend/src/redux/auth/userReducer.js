import { authActionTypes } from './authAction';

const initialState = null;

export default function userReducer(state = initialState, action) {
	// The reducer normally looks at the action type field to decide what happens
	switch (action.type) {
		case authActionTypes.ADD_USER: {
			return action.user;
		}

		case authActionTypes.REMOVE_USER: {
			return null;
		}

		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
