import basketAPI from "../../api/basketAPI"

// Actions for dispatch
const addBasketItem = (basketItem) => {
	return {
		type : "BASKET/ADD_ITEM",
		basketItem
	};
}

const removeBasketItem = (basketItem) => {
	return {
		type: "BASKET/REMOVE_ITEM",
		basketItem,
	};
}

const updateQuantity = (basketItem, quantity) => {
	return {
		type: "BASKET/UPDATE_QUANTITY",
		basketItem,
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

// create basket item with similar properties as BasketItem model
const BasketItem = (item, quantity) => {
	return {
		item,
		itemId: item.id,
		quantity
	}
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
			const basketItem = BasketItem(item, quantity);
			dispatch(addBasketItem(basketItem));
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
			dispatch(removeBasketItem(basketItem));
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
			dispatch(updateQuantity(basketItem, quantity));
		}
	};
}

export {
	addBasketItemAsync,
	removeBasketItemAsync,
	updateQuantityAsync
}

