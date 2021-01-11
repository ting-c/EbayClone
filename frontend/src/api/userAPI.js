import errorHandling from './errorHandling';
const axios = require('axios');

const url = "https://localhost:5001/api/users";

const userAPI = {

	get: async function() {
		try {
			const response = await axios.get(url);
			const users = response.data;
			return users;
		} catch (error) {
			errorHandling(error);
		}
	},

	getByUserId: async function(userId) {
		try {
			const response = await axios.get(`${url}/${userId}`);
			const user = response.data;
			return user;
		} catch (error) {
			errorHandling(error);
		}
	},

	post: async function(data) {
		try {
			const response = await axios.post(url, data);
			const user = response.data;
			return user;
		} catch (error) {
			errorHandling(error);
		}
	},

	put: async function(userId, data) {
		try {
			const response = await axios.put(userId, data);
			const user = response.data;
			return user;
		} catch (error) {
			errorHandling(error);
		}
	},

	delete: async function(userId) {
		try {
			const response = await axios.delete(userId);
			const user = response.data;
			return user;
		} catch (error) {
			errorHandling(error);
		}
	}
};

export default userAPI;