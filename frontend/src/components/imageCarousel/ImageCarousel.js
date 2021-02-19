import React, { useState, useEffect } from 'react'
import './styles.scss';
import EmptyImage from '../../images/empty.png';

const ImageCarousel = ({ imageUrls }) => {

	const [indices, setIndices] = useState([]);

	useEffect(() => {
		const indices = [];
		if (imageUrls.length > 1) {
			for (let i = 1; i < imageUrls.length; i++) {
				indices.push(i);
			}
			setIndices(indices);
		}
	},[imageUrls]);

	return (
		<React.Fragment> { 
			imageUrls ? (
				<div id="carouselIndicators" className="carousel slide" data-ride="carousel">
					<ol className="carousel-indicators">
						<li data-target="#carouselIndicators" data-slide-to="0" className="active"></li>
						{ indices.length ? ( 
							indices.map((i,idx) => 
							<li data-target="#carouselIndicators" data-slide-to={i}key={idx}></li>
						)) : null } 
					</ol>
					<div className="carousel-inner">
						<div className="carousel-item active">
							<img src={imageUrls.length > 0 ? 
								imageUrls[0].urlPath :
								EmptyImage
							} className="d-block w-100" alt=""/>
						</div>
						{ imageUrls.length > 1 ? (
							imageUrls.slice(1).map((url, idx) => 
								<div className="carousel-item" key={idx}>
									<img src={url.urlPath} className="d-block w-100" alt=""/>
								</div>
						)) : null }
					</div>
					<a className="carousel-control-prev" href="#carouselIndicators" role="button" data-slide="prev">
    					<span className="carousel-control-prev-icon" aria-hidden="true"></span>
						<span className="sr-only">Previous</span>
					</a>
					<a className="carousel-control-next" href="#carouselIndicators" role="button" data-slide="next">
						<span className="carousel-control-next-icon" aria-hidden="true"></span>
						<span className="sr-only">Next</span>
					</a>
				</div>
			) : null	} 
		</React.Fragment>
	)
}

export default ImageCarousel
