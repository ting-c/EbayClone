const addItem = (payload) => {
	return {
		type : "BASKET/ADD_ITEM",
		payload
	}
}

const removeItem = (itemId) => {
	return {
		type: "BASKET/REMOVE_ITEM",
		id : itemId
	};
}

const increaseQuantity = (itemId) => {
	return {
		type: "BASKET/INCREASE_QUANTITY",
		id : itemId
	};
}

const decreaseQuantity = (itemId) => {
	return {
		type: "BASKET/DECREASE_QUANTITY",
		id : itemId
	};
}

export {
	addItem,
	removeItem,
	increaseQuantity,
	decreaseQuantity
}

