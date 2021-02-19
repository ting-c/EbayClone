import React from 'react'
import { Link } from 'react-router-dom';
import './style.scss'
import EmptyImage from '../../images/empty.png'

const ItemCard = ({ item }) => {

	const { id, title, price, imageUrl } = item;

	return (
		<Link to={`/item/${id}`}>
			<div className="item">
				<div className='img-container'>
					<img className="img" src={
						imageUrl.length > 0 ? 
							imageUrl[0].urlPath :
							EmptyImage
						} 
						alt={title}/>
				</div>
				<div className="details">
					<h5 className="title" href={`/item/${id}`}>{title}</h5>
					<div className="price">£{price.toFixed(2)}</div>
				</div>
			</div>
		</Link>
	)
}

export default ItemCard
