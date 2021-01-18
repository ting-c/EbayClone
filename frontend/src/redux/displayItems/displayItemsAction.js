import itemAPI from "../../api/itemAPI";

const updateDisplayItems = (displayItems) => {
	return {
		type: "DISPLAY_ITEMS/UPDATE",
		displayItems,
	};
};


// Action that returns thunk functions
const fetchItemsAsync = (searchTerm) => {
	return async function (dispatch, getState) {
		const displayItems = searchTerm ? 
			await itemAPI.getByTitle(searchTerm) :
			// get all items if searchTerm == null
			await itemAPI.get();

		dispatch(updateDisplayItems(displayItems));
	};
}

export { fetchItemsAsync };
