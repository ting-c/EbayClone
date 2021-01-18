const initialState = null;

export default function jwtReducer(state = initialState, action) {
	// The reducer normally looks at the action type field to decide what happens
	switch (action.type) {
		case "JWT/ADD_JWT": {
			return action.token;
		}
		case "JWT/REMOVE_JWT": {
			return null;
		}
		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
