// For handling api error

function errorHandling(error) {
	if (error.response) {
		// The request was made and the server responded with a status code that falls out of the range of 2xx
		const { data, status, header } = error.response;
		console.log(data, status, header);
		alert(error.response.data)
	} else if (error.request) {
		// The request was made but no response was received and is an instance of XMLHttpRequest
		console.log(error.request);
		alert(error.request);
	} else {
		// An error was triggered when setting up the request
		console.log("Error", error.message);
		alert(error.message);
	}
	return null;
}

export default errorHandling;