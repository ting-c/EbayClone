const addJwt = (payload) => {
	return {
		type: "JWT/ADD_JWT",
		payload,
	};
};

const removeUser = () => {
	return {
		type: "JWT/REMOVE_JWT",
	};
};

export { addJwt, removeUser };
