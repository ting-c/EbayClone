const addUser = (payload) => {
	return {
		type: "USER/ADD_USER",
		payload,
	};
};

const removeUser = () => {
	return {
		type: "USER/REMOVE_USER"
	};
};

export {	addUser,	removeUser }