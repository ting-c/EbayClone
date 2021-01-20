import { basketActionTypes } from './basketAction'

export const initialState = [];

export default function basketReducer(state = initialState, action) {

	switch (action.type) {
		case basketActionTypes.ADD_ITEM: {
			return [...state, action.basketItem];
		}

		case basketActionTypes.REMOVE_ITEM: {
			// copy current basket to new array
			const currentBasket = [...state];
			// filter out item
			const newBasket = currentBasket.filter(
				(basketItem) => basketItem.itemId !== action.basketItem.itemId
			);
			return newBasket;
		}

		case basketActionTypes.UPDATE_QUANTITY: {
			// copy current basket to new array
			const currentBasket = [...state];
			const basketItem = currentBasket.find(
				(basketItem) => basketItem.itemId === action.basketItem.itemId
			);
			// filter out the basket item to be updated
			const updatedBasket = currentBasket.filter(
				(basketItem) => basketItem.itemId !== action.basketItem.itemId
			);
			// update quantity 
			const updatedBasketItem = {...basketItem, quantity: action.quantity};
			return [...updatedBasket, updatedBasketItem];
		}

		case basketActionTypes.UPDATE_BASKET: {
			return action.basketItems;
		}

		default:
			// Action type not recognise, return unchanged state
			return state;
	}
}
