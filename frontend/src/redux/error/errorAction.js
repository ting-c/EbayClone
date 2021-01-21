const errorActionCreator = (errorType, errorMessage) => {
	return {
		type: errorType,
		errorMessage
	}
}

export default errorActionCreator