import React from 'react'
import BasketItem from '../../components/basketItem/BasketItem';
import './styles.scss'

const BasketItemPanel = ({ basket }) => {
	return (
		<div className="basket-item-panel">
			<ul className="list-group">
				{basket.map((basketItem, idx) => (
					<BasketItem basketItem={basketItem} key={idx} /> 
				))}
			</ul>
		</div>
	);
}

export default BasketItemPanel
