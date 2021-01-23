import { displayItemsActionTypes } from './displayItemsAction';

const initialState = [];

export default function displayItemsReducer(state = initialState, action) {
	// The reducer normally looks at the action type field to decide what happens
	switch (action.type) {
		case displayItemsActionTypes.UPDATE: {
			return action.displayItems;
		}
		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
