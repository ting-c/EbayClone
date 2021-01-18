const initialState = [];

export default function basketReducer(state = initialState, action) {

	switch (action.type) {
		case "BASKET/ADD_ITEM": {
			return [...state, action.item];
		}

		case "BASKET/REMOVE_ITEM": {
			// copy current basket to new array
			const currentBasket = [...state.basket];
			// filter out item
			const newBasket = currentBasket.filter(
				(item) => item.id !== action.id
			);
			return newBasket;
		}

		case "BASKET/UPDATE_QUANTITY": {
			// copy current basket to new array
			const currentBasket = [...state.basket];
			const item = currentBasket.find((item) => item.id === action.id);
			item.quantity = action.quantity;
			return currentBasket;
		}

		case "BASKET/UPDATE_ITEMS": {
			return action.basketItems;
		}

		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
