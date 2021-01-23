import axios from 'axios'
import errorActionCreator from '../error/errorAction'
import getErrorMessage from '../error/errorMessage';

// Action types 
export const basketActionTypes = {
	ADD_ITEM : 'BASKET/ADD_ITEM',
	REMOVE_ITEM : 'BASKET/REMOVE_ITEM',
	UPDATE_QUANTITY : 'BASKET/UPDATE_QUANTITY',
	UPDATE_BASKET : 'BASKET/UPDATE_BASKET',
	
	ADD_ITEM_ERROR: 'BASKET/ADD_ITEM_ERROR',
	REMOVE_ITEM_ERROR : 'BASKET/REMOVE_ITEM_ERROR',
	UPDATE_QUANTITY_ERROR : 'BASKET/UPDATE_QUANTITY_ERROR',
	UPDATE_BASKET_ERROR : 'BASKET/UPDATE_BASKET_ERROR',
}

// Actions for dispatch
export const addBasketItem = (basketItem) => {
	return {
		type : basketActionTypes.ADD_ITEM,
		basketItem
	};
}

export const removeBasketItem = (basketItem) => {
	return {
		type: basketActionTypes.REMOVE_ITEM,
		basketItem
	};
}

export const updateQuantity = (basketItem, quantity) => {
	return {
		type: basketActionTypes.UPDATE_QUANTITY,
		basketItem,
		quantity
	};
}

export const updateBasket = (basketItems) => {
	return {
		type: basketActionTypes.UPDATE_BASKET,
		basketItems
	};
}

// create basket item with similar properties as BasketItem model
export const BasketItem = (item, quantity) => {
	return {
		item,
		itemId: item.id,
		quantity
	}
}

// Axios config
const BASE_URL = "https://localhost:5001/api/basket";

// For authorized requests
const getConfig = (jwt) => {
	return {
		headers: { Authorization: `Bearer ${jwt}` },
	};
};

// Actions creators
export const addBasketItemAsync = (jwt, item, quantity) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - add item to db
			const url = `${BASE_URL}/${item.id}/${quantity}`;
			try {
				await axios.post(url, getConfig(jwt));
				// get updated basket items from db
				const response = await axios.get(BASE_URL, getConfig(jwt));
				const basketItems = response.data;
				// update basket with updated basket items
				dispatch(updateBasket(basketItems));
			} catch (error) {
				const errorMessage = getErrorMessage(error);
				dispatch(errorActionCreator(basketActionTypes.ADD_ITEM_ERROR, errorMessage));
			}
		} else {
			// User is not signed in - create a basket item and add to basket
			const basketItem = BasketItem(item, quantity);
			dispatch(addBasketItem(basketItem));
		}
	}
}

export const removeBasketItemAsync = (jwt, basketItem) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - remove item from db
			const url = `${BASE_URL}/${basketItem.id}`;
			try {
				await axios.delete(url, getConfig(jwt));
				// get updated basket items from db
				const response = await axios.get(BASE_URL, getConfig(jwt));
				const basketItems = response.data;
				// update basket with updated basket items
				dispatch(updateBasket(basketItems));
			} catch (error) {
				const errorMessage = getErrorMessage(error);
				dispatch(errorActionCreator(basketActionTypes.REMOVE_ITEM_ERROR, errorMessage));
			}
		} else {
			// User is not signed in - remove the basket item from basket
			dispatch(removeBasketItem(basketItem));
		}
	};
}

export const updateQuantityAsync = (jwt, basketItem, quantity) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - update basketItem quantity in db
			const url = `${BASE_URL}/${basketItem.id}/${quantity}`;
			try {
				await axios.put(url, getConfig(jwt));
				// get updated basket items from db
				const response = await axios.get(BASE_URL, getConfig(jwt));
				const basketItems = response.data;
				// update basket with updated basket items
				dispatch(updateBasket(basketItems));
			} catch (error) {
				const errorMessage = getErrorMessage(error);
				dispatch(errorActionCreator(basketActionTypes.UPDATE_QUANTITY_ERROR, errorMessage))
			}
		} else {
			// User is not signed in - update quantity of the basket item
			dispatch(updateQuantity(basketItem, quantity));
		}
	};
}

