import React from 'react'
import ImageCarousel from '../../components/imageCarousel/ImageCarousel'
import './styles.scss'

const ImagePanel = ({ imageUrls }) => {
	return (
		<div className='image-panel'>
			<ImageCarousel imageUrls={imageUrls} />
		</div>
	);
}

export default ImagePanel
