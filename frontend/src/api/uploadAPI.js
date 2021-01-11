import errorHandling from "./errorHandling";
const axios = require("axios");

const url = "https://localhost:5001/api/upload";

const uploadAPI = {
	upload: async function (jwt, itemId, data) {

		const config = {
			headers: { 'Authorization': `Bearer ${jwt}` },
		};
		
		try {
			const response = await axios.post(
				`${url}/${itemId}`, data, config
			);
			return response.data;
		} catch (error) {
			errorHandling(error);
		}
	}
};

export default uploadAPI;
