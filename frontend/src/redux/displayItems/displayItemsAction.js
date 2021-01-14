const updateDisplayItems = (payload) => {
	return {
		type: "DISPLAY_ITEMS/UPDATE",
		// payload = list of display items
		payload,
	};
};

export { updateDisplayItems };
