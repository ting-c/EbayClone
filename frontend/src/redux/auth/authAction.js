import authAPI from '../../api/authAPI'

// Actions for user and jwt 
const addJwt = (token) => {
	return {
		type: "JWT/ADD_JWT",
		token,
	};
};

const removeJwt = () => {
	return {
		type: "JWT/REMOVE_JWT",
	};
};

const addUser = (payload) => {
	return {
		type: "USER/ADD_USER",
		payload,
	};
};

const removeUser = () => {
	return {
		type: "USER/REMOVE_USER",
	};
};

// Actions with a thunk function before dispatch
const signInAsync = (data) => {
	return async function (dispatch, getState) {
		// data = { email, password }
		const response = await authAPI.signin(data);
		if (response) {
			const { user, jwtString } = response;
			dispatch(addUser(user));
			dispatch(addJwt(jwtString));
		}
	}
}

const signOut = () => {
	return function (dispatch, getState) {
		dispatch(removeUser());
		dispatch(removeJwt());
	}
};

export { signInAsync, signOut };
