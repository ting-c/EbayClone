const initialState = null;

export default function userReducer(state = initialState, action) {
	// The reducer normally looks at the action type field to decide what happens
	switch (action.type) {
		case "USER/ADD_USER": {
			return {
				...state,
				user: action.payload,
			};
		}
		case "USER/REMOVE_USER": {
			return {
				...state,
				user: null,
			};
		}
		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
