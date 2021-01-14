import React from 'react'
import { withRouter } from 'react-router-dom';
import Upload from '../../components/upload/Upload'

const UploadImage = ({ match }) => {

	const itemId = match.params.itemId;

	return (
		<div className="upload-image">
			<Upload itemId={itemId} />
		</div>
	)
}

export default withRouter(UploadImage)
