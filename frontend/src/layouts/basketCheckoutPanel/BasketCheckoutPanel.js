import React from 'react'
import './styles.scss'
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';

const BasketCheckoutPanel = ({ basket }) => {
	
	// calculate the total of all basket items
	const total = basket.length ? basket.reduce(
		(accumulator, basketItem) => accumulator + basketItem.item.price * basketItem.quantity, 0
	) : 0;

	return (
		<div className="basket-checkout-panel">
			<Link to='/checkout'>
				<button id="checkout-button" className="btn btn-primary">
					Go to checkout
				</button>
			</Link>
			<Link to='/'>
				<button id="continue-shopping-button" className="btn btn-light">
					Continue shopping
				</button>
			</Link>
			<div className="summary">
				<div className="align-left">Items ({basket.length})</div>
				<div className="align-right">£{total.toFixed(2)}</div>
			</div>
			<hr />
			<div className="footer">
				<div className="align-left">Total</div>
				<div className="align-right">£{total.toFixed(2)}</div>
			</div>
		</div>
	);
}

const mapStateToProps = (state) => {
	return { basket: state.basket }
}

export default connect(mapStateToProps)(BasketCheckoutPanel)
