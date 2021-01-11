import React from 'react'
import { withRouter } from 'react-router-dom';
import Upload from '../../components/upload/Upload'

const UploadImage = ({ match, jwt }) => {

	const itemId = match.params.itemId;

	return (
		<div className="upload-image">
			<Upload itemId={itemId} jwt={jwt}/>
		</div>
	)
}

export default withRouter(UploadImage)
