import React, { useState } from 'react'
import uploadAPI from '../../api/uploadAPI';
import './styles.scss'

const Upload = ({ itemId, jwt }) => {

	const [selectedImages, setSelectedImages] = useState(null)

	function handleUpload(){
		const data = new FormData();
		// loop through images and append to form data
		for (var i = 0; i < selectedImages.length; i++) {
			data.append("files", selectedImages[i]);
		};
		uploadAPI.upload(jwt, parseInt(itemId), data);
	}

	function onChangeHandler(event) {
		setSelectedImages(event.target.files);
	};
	
	return (
		<form className="upload">
			<h4>Upload images</h4>
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

export default Upload
