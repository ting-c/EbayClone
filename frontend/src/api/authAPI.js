import errorHandling from "./errorHandling";
const axios = require("axios");

const url = "https://localhost:5001/api/auth";

const authAPI = {

	signup: async function (data) {
		try {
			const route = 'signup';
			const response = await axios.post(`${url}/${route}`, data);
			return response.data;
		} catch (error) {
			errorHandling(error);
		}
	},

	signin: async function (data) {
		try {
			const route = 'signin';
			const response = await axios.post(`${url}/${route}`, data);
			return response.data;
		} catch (error) {
			errorHandling(error);
		}
	}
};

export default authAPI;
