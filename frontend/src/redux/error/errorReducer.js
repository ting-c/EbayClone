import { basketActionTypes } from "../basket/basketAction";
import { authActionTypes } from "../auth/authAction";
import { displayItemsActionTypes } from "../displayItems/displayItemsAction";
import { errorActionTypes } from "../error/errorAction";

export const initialState = null;

export default function errorReducer(state = initialState, action) {
	switch (action.type) {
		case basketActionTypes.ADD_ITEM_ERROR: {
			return action.errorMessage;
		}
		case basketActionTypes.REMOVE_ITEM_ERROR: {
			return action.errorMessage;
		}
		case basketActionTypes.UPDATE_QUANTITY_ERROR: {
			return action.errorMessage;
		}
		case basketActionTypes.UPDATE_BASKET_ERROR: {
			return action.errorMessage;
		}
		case authActionTypes.SIGNIN_ERROR: {
			return action.errorMessage;
		}
		case authActionTypes.SIGNUP_ERROR: {
			return action.errorMessage;
		}
		case displayItemsActionTypes.UPDATE_ERROR: {
			return action.errorMessage;
		}

		case errorActionTypes.CLEAR_ERROR_MESSAGE: {
			return action.payload; // payload = null
		}
		
		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
