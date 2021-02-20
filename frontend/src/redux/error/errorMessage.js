const getErrorMessage = (error) => {
	if (error.response) {
		// Request made and server responded
		return error.response.data.title;
	} else if (error.request) {
		// The request was made but no response was received
		return error.request;
	} else {
		// Something happened in setting up the request that triggered an Error
		return error.message;
	}
}

export default getErrorMessage;