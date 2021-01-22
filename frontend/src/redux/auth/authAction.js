import axios from 'axios';
import errorActionCreator from '../error/errorAction';

export const authActionTypes = {
	ADD_JWT: "JWT/ADD_JWT",
	REMOVE_JWT: "JWT/REMOVE_JWT",
	ADD_USER: "USER/ADD_USER",
	REMOVE_USER: "USER/REMOVE_USER",

	SIGNIN_ERROR: 'AUTH/SIGNIN_ERROR',
	SIGNUP_ERROR: 'AUTH/SIGNUP_ERROR',
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

// API Base URL for Authentication
const BASE_URL = "https://localhost:5001/api/auth";

// Actions creators
export const signInAsync = (data) => {
	return async function (dispatch, getState) {
		const url = `${BASE_URL}/signin`;
		try {
			const response = await axios.post(url, data);
			const userObject = response.data;
			if (userObject) {
				const { user, jwtString } = userObject;
				dispatch(addUser(user));
				dispatch(addJwt(jwtString));
			}
		} catch (error) {
			let errorMessage;
			if (error.response) {
				// Request made and server responded
				errorMessage = error.response.data;
			} else if (error.request) {
				// The request was made but no response was received
				errorMessage = error.request;
			} else {
				// Something happened in setting up the request that triggered an Error
				errorMessage = error.message;
			}
			dispatch(errorActionCreator(authActionTypes.SIGNIN_ERROR, errorMessage));
		}
	};
};

export const signUpAsync = (data) => {
	return async function (dispatch, getState) {
		const url = `${BASE_URL}/signup`;
		try {
			const response = await axios.post(url, data);
			if (response) {
				const signInData = { email: data.email, password: data.password };
				// auto sign in after sign up success
				return dispatch(signInAsync(signInData));
			}
		} catch (error) {
			let errorMessage;
			if (error.response) {
				// Request made and server responded
				errorMessage = error.response.data;
			} else if (error.request) {
				// The request was made but no response was received
				errorMessage = error.request;
			} else {
				// Something happened in setting up the request that triggered an Error
				errorMessage = error.message;
			}
			dispatch(
				errorActionCreator(authActionTypes.SIGNUP_ERROR, errorMessage)
			);
		}
	};
}

export const signOut = () => {
	return function (dispatch, getState) {
		dispatch(removeUser());
		dispatch(removeJwt());
	};
};

