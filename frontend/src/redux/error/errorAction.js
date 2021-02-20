const errorActionCreator = (errorType, errorMessage) => {
	return {
		type: errorType,
		errorMessage
	}
}

export const errorActionTypes = {
	CLEAR_ERROR_MESSAGE: "CLEAR_ERROR_MESSAGE",
	FETCH_SELLING_ITEMS_ERROR: 'FETCH_SELLING_ITEMS_ERROR',
	REMOVE_SELLING_ITEM_ERROR: 'REMOVE_SELLING_ITEM_ERROR',
	SELL_ITEM_ERROR: 'SELL_ITEM_ERROR',
};

export const clearErrorMessage = () => {
	return {
		type: errorActionTypes.CLEAR_ERROR_MESSAGE,
		payload: null
	}
}

// For general error messages
export const displayErrorMessage = (errorType, errorMessage) => {
	return async function (dispatch, getState) {
		dispatch(errorActionCreator(errorType, errorMessage))
	}
}

export const clearError = () => {
	return async function (dispatch, getState) {
		dispatch(clearErrorMessage())
	};
};

export const BASE_URL = "https://localhost:5001/api/items";



export default errorActionCreator