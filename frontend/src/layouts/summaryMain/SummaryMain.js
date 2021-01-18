import React from 'react'
import Price from '../../components/price/Price'
import SummaryButton from '../../components/summaryButton/SummaryButton'
import './style.scss'
import { connect } from 'react-redux'
import { addBasketItemAsync } from '../../redux/basket/basketAction'

const SummaryMain = ({ jwt, item, addBasketItemAsync }) => {

	const onAddToBasket = () => {
		const quantity = 1;
		addBasketItemAsync(jwt, item, quantity);
		console.log('click')
	}

	return (
		<div className="summary-main">
			<Price price={item.price} />
			<div className="summary-button-group">
				<SummaryButton text="Buy it now" styles="primary" />
				<SummaryButton text="Add to basket" styles="info" onAddToBasket={onAddToBasket}/>
				<SummaryButton text="♡ Watch this item" styles="outline-primary" />
			</div>
		</div>
	);
}

const mapStateToProps = (state) => {
	return { jwt: state.jwt }
}
const mapDispatchToProps = { addBasketItemAsync }

export default connect(mapStateToProps, mapDispatchToProps)(SummaryMain)
