import errorHandling from './errorHandling';
const axios = require('axios');

const url = "https://localhost:5001/api/items";

const itemAPI = {

	get: async function() {
		try {
			const response = await axios.get(url);
			const items = response.data;
			return items;
		} catch (error) {
			errorHandling(error);
		}
	},

	getByUserId: async function(itemId) {
		try {
			const response = await axios.get(`${url}/${itemId}`);
			const item = response.data;
			return item;
		} catch (error) {
			errorHandling(error);
		}
	},

	getByTitle: async function(title) {
		try {
			const response = await axios.get(`${url}/search/${title}`);
			const items = response.data;
			return items;
		} catch (error) {
			errorHandling(error);
		}
	},

	post: async function(body) {
		try {
			const response = await axios.post(url, body);
			const item = response.data;
			return item;
		} catch (error) {
			errorHandling(error);
		}
	},

	put: async function(itemId, body) {
		try {
			const response = await axios.put(itemId, body);
			const item = response.data;
			return item;
		} catch (error) {
			errorHandling(error);
		}
	},

	delete: async function(itemId) {
		try {
			const response = await axios.delete(itemId);
			const item = response.data;
			return item;
		} catch (error) {
			errorHandling(error);
		}
	}
};

export default itemAPI;