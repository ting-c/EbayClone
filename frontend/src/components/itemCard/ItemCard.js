import React from 'react'
import { Link } from 'react-router-dom';
import './style.scss'

const ItemCard = ({ item }) => {

	const { id, title, price, imageUrl } = item;

	return (
		<div className="card">
			<Link to={`/item/${id}`}>
			<img className="card-img-top" src={imageUrl[0].urlPath} alt="Card cap"/>
			<div className="card-body">
				<h5 className="card-title" href={`/item/${id}`}>{title}</h5>
				<div className="price">£{price.toFixed(2)}</div>
			</div>
			</Link>
		</div>
	)
}

export default ItemCard
