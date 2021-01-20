import React from 'react'
import './styles.scss'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import { removeBasketItemAsync } from '../../redux/basket/basketAction'

const BasketItem = ({ jwt, basketItem, removeBasketItemAsync }) => {

	const { item, quantity } = basketItem;
	const { id, title, price, imageUrl, seller } = item;
	const subTotal = price*quantity;

	return (
		<li className="basket-item jumbotron list-group-item">
			<div className="header">
				<p id="seller-label">Seller
					<span id="seller-name">{seller.firstName}</span>
				</p>
			</div>
			<hr className="my-4"/>
			<div className="summary">
				<img id="image" src={imageUrl[0].urlPath} alt="item"/>
				<p id="title"><Link to={`/item/${id}`}>{title}</Link></p>
				<p id="quantity">Qty: {quantity}</p>
				<p id="sub-total">Sub-total: {subTotal}</p>
			</div>
			<div className="footer">
				<button 
					id="remove" 
					onClick={() => removeBasketItemAsync(jwt, basketItem)}>
					Remove
				</button>
			</div>
		</li>
	);
}

const mapStateToProps = (state) => {
	return { jwt: state.jwt }
}

const mapDispatchToProps = { removeBasketItemAsync }

export default connect(mapStateToProps, mapDispatchToProps)(BasketItem)
