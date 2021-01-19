const initialState = [];

export default function basketReducer(state = initialState, action) {

	switch (action.type) {
		case "BASKET/ADD_ITEM": {
			return [...state, action.basketItem];
		}

		case "BASKET/REMOVE_ITEM": {
			// copy current basket to new array
			const currentBasket = [...state];
			// filter out item
			const newBasket = currentBasket.filter(
				(basketItem) => basketItem.itemId !== action.basketItem.itemId
			);
			return newBasket;
		}

		case "BASKET/UPDATE_QUANTITY": {
			// copy current basket to new array
			const currentBasket = [...state];
			const item = currentBasket.find((basketItem) => basketItem.itemId === action.basketItem.itemId);
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
