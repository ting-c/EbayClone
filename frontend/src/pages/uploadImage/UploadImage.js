import React from 'react'
import { withRouter } from 'react-router-dom';
import Upload from '../../components/upload/Upload'
import './styles.scss'

const UploadImage = ({ match, history }) => {

	const itemId = match.params.itemId;
	
	return (
		<div className="upload-image-page">
			<h5>Upload images</h5>
			<Upload itemId={itemId} />
			<button className='btn btn-outline-primary skip-button' onClick={()=>history.push('/selling')}>Skip</button>
		</div>
	)
}

export default withRouter(UploadImage)
