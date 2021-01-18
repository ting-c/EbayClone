import basketAPI from "../../api/basketAPI";

// Actions for dispatch
const addItem = (item) => {
	return {
		type : "BASKET/ADD_ITEM",
		item
	}
}

const removeItem = (itemId) => {
	return {
		type: "BASKET/REMOVE_ITEM",
		itemId,
	};
}

const updateQuantity = (itemId, quantity) => {
	return {
		type: "BASKET/UPDATE_QUANTITY",
		itemId,
		quantity,
	};
}

// update basket with 
const updateBasket = (basketItems) => {
	return {
		type: "BASKET/UPDATE_ITEMS",
		basketItems
	};
}


// Actions with a thunk function before dispatch
const addBasketItemAsync = (jwt, item, quantity) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - add item to db
			await basketAPI.post(jwt, item.id, quantity);
			// get updated basket items from db
			const basketItems = await basketAPI.get(jwt);
			// update basket with updated basket items
			dispatch(updateBasket(basketItems));
		} else {
			dispatch(addItem(item));
		}
	}
}

const removeBasketItemAsync = (jwt, basketItem) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - remove item from db
			await basketAPI.delete(jwt, basketItem.id);
			// get updated basket items from db
			const basketItems = await basketAPI.get(jwt);
			// update basket with updated basket items
			dispatch(updateBasket(basketItems));
		} else {
			dispatch(removeItem(basketItem.id));
		}
	};
}

const updateQuantityAsync = (jwt, basketItem, quantity) => {
	return async function (dispatch, getState) {
		if (jwt) {
			// User is signed in - update basketItem quantity in db
			await basketAPI.post(basketItem.itemId, quantity);
			// get updated basket items from db
			const basketItems = await basketAPI.get();
			// update basket with updated basket items
			dispatch(updateBasket(basketItems));
		} else {
			dispatch(updateQuantity(basketItem.id, quantity));
		}
	};
}

export {
	addBasketItemAsync,
	removeBasketItemAsync,
	updateQuantityAsync
}

