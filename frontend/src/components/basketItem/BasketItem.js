import React from 'react'
import './styles.scss'
import { connect } from 'react-redux'
import { removeBasketItemAsync } from '../../redux/basket/basketAction'

const BasketItem = ({ jwt, basketItem, removeBasketItemAsync }) => {
	return (
		<li className="basket-item jumbotron list-group-item">
			<div className="seller">
				<p>Seller: {basketItem.item.seller.firstName}</p>
			</div>	
			<hr className="my-4"/>
			<div className="summary">
				<img id="image" src={basketItem.item.imageUrl[0].urlPath} alt="item"/>
				<p id="title">{basketItem.item.title}</p>
				<p id="quantity">Qty: {basketItem.quantity}</p>
				<p id="sub-total">Sub-total: {basketItem.item.price*basketItem.quantity}</p>
			</div>
			<div className="summary-footer">
				<button id="remove" onClick={() => removeBasketItemAsync(jwt, basketItem)}>Remove</button>
			</div>
			
		</li>
	);
}

const mapStateToProps = (state) => {
	return { jwt: state.jwt }
}

const mapDispatchToProps = { removeBasketItemAsync }

export default connect(mapStateToProps, mapDispatchToProps)(BasketItem)
