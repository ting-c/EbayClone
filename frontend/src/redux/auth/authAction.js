import authAPI from '../../api/authAPI'

export const authActionTypes = {
	ADD_JWT: "JWT/ADD_JWT",
	REMOVE_JWT: "JWT/REMOVE_JWT",
	ADD_USER: "USER/ADD_USER",
	REMOVE_USER: "USER/REMOVE_USER"
};

// Actions for user and jwt 
export const addJwt = (token) => {
	return {
		type: authActionTypes.ADD_JWT,
		token,
	};
};

export const removeJwt = () => {
	return {
		type: authActionTypes.REMOVE_JWT,
	};
};

export const addUser = (user) => {
	return {
		type:authActionTypes.ADD_USER,
		user,
	};
};

export const removeUser = () => {
	return {
		type: authActionTypes.REMOVE_USER,
	};
};

// Actions creators
export const signInAsync = (data) => {
	return async function (dispatch, getState) {
		// data = { email, password }
		const response = await authAPI.signin(data);
		if (response) {
			const { user, jwtString } = response;
			dispatch(addUser(user));
			dispatch(addJwt(jwtString));
		}
	};
};

export const signOut = () => {
	return function (dispatch, getState) {
		dispatch(removeUser());
		dispatch(removeJwt());
	};
};

