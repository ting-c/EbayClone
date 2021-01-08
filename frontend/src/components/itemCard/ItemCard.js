import React from 'react'
import './style.scss'

const ItemCard = ({ item }) => {

	const { id, title, price, imageUrl } = item;

	return (
		<div className="card">
			<img className="card-img-top" src={imageUrl[0].urlPath} alt="Card cap"/>
			<div className="card-body">
				<h5 className="card-title" href={`/item/${id}`}>{title}</h5>
				<h5>£{price.toFixed(2)}</h5>
			</div>
		</div>
	)
}

export default ItemCard
