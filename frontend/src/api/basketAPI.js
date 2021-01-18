import errorHandling from "./errorHandling";
const axios = require("axios");

const url = "https://localhost:5001/api/basket";

const basketAPI = {

	get: async function (jwt) {
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};
		try {
			const response = await axios.get(url, config);
			const basketItems = response.data;
			return basketItems;
		} catch (error) {
			errorHandling(error);
		}
	},

	post: async function (jwt, itemId, quantity=1) {
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};
		try {
			const response = await axios.post(
				`${url}/${itemId}/${quantity}`,
				config
			);
			const basketItem = response.data;
			return basketItem;
		} catch (error) {
			errorHandling(error);
		}
	},

	put: async function (jwt, basketItemId, quantity) {
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};
		try {
			await axios.post(
				`${url}/${basketItemId}/${quantity}`,
				config
			);
		} catch (error) {
			errorHandling(error);
		}
	},

	delete: async function (jwt, basketItemId) {
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};
		try {
			await axios.post(
				`${url}/${basketItemId}`,
				config
			);
		} catch (error) {
			errorHandling(error);
		}
	},
};

export default basketAPI;
