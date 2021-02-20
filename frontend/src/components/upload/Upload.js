import React, { useState } from 'react'
import { connect } from 'react-redux'
import './styles.scss'
import axios from 'axios';
import { withRouter } from 'react-router-dom';

const Upload = ({ itemId, jwt, history }) => {

	const [selectedImages, setSelectedImages] = useState(null)

	async function handleUpload(){
		const data = new FormData();
		// loop through images and append to form data
		for (var i = 0; i < selectedImages.length; i++) {
			data.append("files", selectedImages[i]);
		};
		const url = `https://localhost:5001/api/upload/${itemId}`;
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};

		const response = await axios.post(url, data, config);
		if (response.status === 200) {
			history.push(`/item/${itemId}`);
		}
	}

	function onChangeHandler(event) {
		setSelectedImages(event.target.files);
	};
	
	return (
		<form className="upload">
			<div className="input">
				<input
					type="file"
					className="file-input"
					accept="image/*"
					id="inputGroupFile"
					multiple
					onChange={onChangeHandler}
				/>
			</div>
			<button className="upload-button btn btn-primary" type="button" onClick={handleUpload}>
				Upload
			</button>
		</form>
	);
};

const mapStateToProps = (state) => {
	return { jwt: state.jwt }
};

export default withRouter(connect(mapStateToProps)(Upload))
