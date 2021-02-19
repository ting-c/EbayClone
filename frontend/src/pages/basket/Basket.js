import React from 'react'
import './styles.scss'
import BasketCheckoutPanel from '../../layouts/basketCheckoutPanel/BasketCheckoutPanel';
import BasketItemPanel from '../../layouts/basketItemPanel/BasketItemPanel';
import { connect } from 'react-redux';

const Basket = ({ basket }) => {
	return (
		<div className="basket-page">
			{	basket.length > 0 ? (
				<React.Fragment>
					<BasketItemPanel />
					<BasketCheckoutPanel />
				</React.Fragment>
				) : (
					<h5>No item in basket</h5>
				)
			}
		</div>
	);
}

const mapStateToProps = (state) => {
	return { basket: state.basket }
}

export default connect(mapStateToProps)(Basket)
