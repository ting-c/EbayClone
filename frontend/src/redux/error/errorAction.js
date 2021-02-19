const errorActionCreator = (errorType, errorMessage) => {
	return {
		type: errorType,
		errorMessage
	}
}

export const errorActionTypes = {
	CLEAR_ERROR_MESSAGE : 'CLEAR_ERROR_MESSAGE'
};

export const clearErrorMessage = () => {
	return {
		type: errorActionTypes.CLEAR_ERROR_MESSAGE,
		payload: null
	}
}

export const clearError = () => {
	return async function (dispatch, getState) {
		dispatch(clearErrorMessage())
	};
};

export const BASE_URL = "https://localhost:5001/api/items";



export default errorActionCreator