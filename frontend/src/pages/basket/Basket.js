import React from 'react'
import './styles.scss'
import BasketCheckoutPanel from '../../layouts/basketCheckoutPanel/BasketCheckoutPanel';
import BasketItemPanel from '../../layouts/basketItemPanel/BasketItemPanel';

const Basket = () => {
	return (
		<div className="basket-page">
			<BasketItemPanel />
			<BasketCheckoutPanel />
		</div>
	);
}

export default Basket
