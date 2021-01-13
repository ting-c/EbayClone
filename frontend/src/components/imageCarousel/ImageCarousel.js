import React, { useState, useEffect } from 'react'
import './styles.scss';

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
	},[indices, imageUrls]);

	return (
		<React.Fragment> { 
			imageUrls ? (
				<div id="carouselIndicators" class="carousel slide" data-ride="carousel">
					<ol class="carousel-indicators">
						<li data-target="#carouselIndicators" data-slide-to="0" class="active"></li>
						{ indices.length ? ( 
							indices.map(i => 
							<li data-target="#carouselIndicators" data-slide-to={i}></li>
						)) : null } 
					</ol>
					<div className="carousel-inner">
						<div className="carousel-item active">
							<img src={imageUrls[0].urlPath} className="d-block w-100" alt=""/>
						</div>
						{ imageUrls.length > 1 ? (
							imageUrls.slice(1).map( url => 
								<div className="carousel-item">
									<img src={url.urlPath} className="d-block w-100" alt=""/>
								</div>
						)) : null }
					</div>
					<a class="carousel-control-prev" href="#carouselIndicators" role="button" data-slide="prev">
    					<span class="carousel-control-prev-icon" aria-hidden="true"></span>
						<span class="sr-only">Previous</span>
					</a>
					<a class="carousel-control-next" href="#carouselIndicators" role="button" data-slide="next">
						<span class="carousel-control-next-icon" aria-hidden="true"></span>
						<span class="sr-only">Next</span>
					</a>
				</div>
			) : null	} 
		</React.Fragment>
	)
}

export default ImageCarousel
