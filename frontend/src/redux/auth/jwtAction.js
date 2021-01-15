const addJwt = (payload) => {
	return {
		type: "JWT/ADD_JWT",
		payload,
	};
};

const removeJwt = () => {
	return {
		type: "JWT/REMOVE_JWT",
	};
};

export { addJwt, removeJwt};
