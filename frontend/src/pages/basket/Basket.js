import React from 'react'
import { connect } from 'react-redux'
import BasketCheckoutPanel from '../../layouts/basketCheckoutPanel/BasketCheckoutPanel';
import BasketItemPanel from '../../layouts/basketItemPanel/BasketItemPanel';

const Basket = ({ basket }) => {
	return (
		<div className="basket-page">
			<BasketItemPanel basket={basket}/>
			<BasketCheckoutPanel />
		</div>
	);
}

const mapStateToProps = (state) => {	
	return { basket: state.basket }
}

export default connect(mapStateToProps)(Basket)
