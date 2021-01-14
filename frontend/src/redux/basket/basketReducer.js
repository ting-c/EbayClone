const initialState = [];

export default function basketReducer(state = initialState, action) {

	switch (action.type) {
		case "BASKET/ADD_ITEM": {
			return [
				...state,
				// payload = new item with quantity of 1
				action.payload,
			];
		}
		case "BASKET/REMOVE_ITEM": {
			const copiedState = [...state];
			const itemRemovedState = copiedState.filter(item => item.id !== action.id)
			return itemRemovedState;
		}
		case "BASKET/INCREASE_QUANTITY": {
			const copiedState = [...state];
			const itemToUpdate = copiedState.find(item => item.id === action.id);
			itemToUpdate.quantity += 1;
			return copiedState;
		} 
		case "BASKET/DECREASE_QUANTITY": {
			const copiedState = [...state];
			const itemToUpdate = copiedState.find(item => item.id === action.id);
			itemToUpdate.quantity -= 1;
			return copiedState;
		} 
		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
