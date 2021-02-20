import React from 'react'
import Price from '../../components/price/Price'
import SummaryButton from '../../components/summaryButton/SummaryButton'
import './style.scss'
import { connect } from 'react-redux'
import { addBasketItemAsync } from '../../redux/basket/basketAction'
import { Link } from 'react-router-dom'

const SummaryMain = ({ jwt, item, basket, addBasketItemAsync }) => {

	const onAddToBasket = () => {
		const quantity = 1;
		addBasketItemAsync(jwt, item, quantity);
	}

	// check if item is already in basket
	const isItemInBasket = basket.find(basketItem => 
		basketItem.itemId === item.id);

	return (
		<div className="summary-main">
			<Price price={item.price} />
			<div className="summary-button-group">
				<SummaryButton text="Buy it now" styles="primary" />
				{ isItemInBasket ? (
					<Link to='/basket'>
					<SummaryButton
						text="View basket"
						styles="info"
					/></Link>
				) : (
					<SummaryButton
						text="Add to basket"
						styles="info"
						onAddToBasket={onAddToBasket}
					/>
				)}
			</div>
		</div>
	);
}

const mapStateToProps = (state) => {
	return { 
		jwt: state.jwt,
		basket: state.basket
	};
}

const mapDispatchToProps = { addBasketItemAsync }

export default connect(mapStateToProps, mapDispatchToProps)(SummaryMain)
