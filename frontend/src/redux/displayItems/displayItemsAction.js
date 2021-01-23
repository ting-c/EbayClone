import axios from "axios";
import errorActionCreator from "../error/errorAction";
import getErrorMessage from "../error/errorMessage";

export const displayItemsActionTypes = {
	UPDATE: "DISPLAY_ITEMS/UPDATE",
	UPDATE_ERROR: "DISPLAY_ITEMS/UPDATE_ERROR"
};

export const updateDisplayItems = (displayItems) => {
	return {
		type: displayItemsActionTypes.UPDATE,
		displayItems,
	};
};

export const BASE_URL = "https://localhost:5001/api/items";

// Action that returns thunk functions
export const fetchItemsAsync = (searchTerm=null) => {
	return async function (dispatch, getState) {
		try {
			let response;
			if (searchTerm) {
				const url = `${BASE_URL}/search/${searchTerm}`;
				response = await axios.get(url)
			} else {
				// get all items if searchTerm == null
				response = await axios.get(BASE_URL)
			}
			const displayItems = response.data;
			if (!displayItems) throw new Error("Failed to fetch data");
			dispatch(updateDisplayItems(displayItems));
		} catch (error) {	
			const errorMessage = getErrorMessage(error);
			dispatch(errorActionCreator(displayItemsActionTypes.UPDATE_ERROR, errorMessage))
		}
	};
}
