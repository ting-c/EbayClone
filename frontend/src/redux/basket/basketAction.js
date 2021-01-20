import basketAPI from "../../api/basketAPI"

// Action types 
export const basketActionTypes = {
	ADD_ITEM : 'BASKET/ADD_ITEM',
	REMOVE_ITEM : 'BASKET/REMOVE_ITEM',
	UPDATE_QUANTITY : 'BASKET/UPDATE_QUANTITY',
	UPDATE_BASKET : 'BASKET/UPDATE_ITEMS',
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
		basketItem,
	};
}

export const updateQuantity = (basketItem, quantity) => {
	return {
		type: basketActionTypes.UPDATE_QUANTITY,
		basketItem,
		quantity,
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


// Actions with a thunk function before dispatch
export const addBasketItemAsync = (jwt, item, quantity) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - add item to db
			await basketAPI.post(jwt, item.id, quantity);
			// get updated basket items from db
			const basketItems = await basketAPI.get(jwt);
			// update basket with updated basket items
			dispatch(updateBasket(basketItems));
		} else {
			// create basket item 
			const basketItem = BasketItem(item, quantity);
			dispatch(addBasketItem(basketItem));
		}
	}
}

export const removeBasketItemAsync = (jwt, basketItem) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - remove item from db
			await basketAPI.delete(jwt, basketItem.id);
			// get updated basket items from db
			const basketItems = await basketAPI.get(jwt);
			// update basket with updated basket items
			dispatch(updateBasket(basketItems));
		} else {
			dispatch(removeBasketItem(basketItem));
		}
	};
}

export const updateQuantityAsync = (jwt, basketItem, quantity) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - update basketItem quantity in db
			await basketAPI.post(basketItem.itemId, quantity);
			// get updated basket items from db
			const basketItems = await basketAPI.get();
			// update basket with updated basket items
			dispatch(updateBasket(basketItems));
		} else {
			dispatch(updateQuantity(basketItem, quantity));
		}
	};
}

