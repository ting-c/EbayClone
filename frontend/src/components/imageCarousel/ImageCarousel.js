import React from 'react'
import './styles.scss'

const ImageCarousel = ({ imageUrls }) => {

	let indicesArray = [];

	if (imageUrls > 1) {
		for(let i=1; i<imageUrls.length; i++) {
			indicesArray.push(i);
		}
	};

	return (
		<React.Fragment> { 
			imageUrls ? (
				<div id="carousel" className="image-carousel carousel slide" data-bs-ride="carousel">
					<ol className="carousel-indicators">
						<li data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active"></li>
						{ indicesArray.length > 1 ? ( 
							indicesArray.map(i => 
							<li data-bs-target="#carouselExampleIndicators" data-bs-slide-to={i}></li>
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
						<a className="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-bs-slide="prev">
							<span className="carousel-control-prev-icon" aria-hidden="true"></span>
							<span className="visually-hidden">Previous</span>
						</a>
						<a className="carousel-control-next" href="#carouselExampleIndicators" role="button" data-bs-slide="next">
							<span className="carousel-control-next-icon" aria-hidden="true"></span>
							<span className="visually-hidden">Next</span>
						</a>
					</div>
				</div>
			) : null	} 
		</React.Fragment>
	)
}

export default ImageCarousel
