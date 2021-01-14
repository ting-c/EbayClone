const initialState = [];

export default function displayItemsReducer(state = initialState, action) {
	// The reducer normally looks at the action type field to decide what happens
	switch (action.type) {
		case "DISPLAY_ITEMS/UPDATE": {
			return {
				...state,
				displayItems: action.payload,
			};
		}
		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
