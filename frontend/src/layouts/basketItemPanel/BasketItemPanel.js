import React from 'react'
import BasketItem from '../../components/basketItem/BasketItem';
import './styles.scss'
import { connect } from 'react-redux'

const BasketItemPanel = ({ basket }) => {
	return (
		<div className="basket-item-panel">
			<ul className="list-group">
				{ basket.length !== 0 ? basket.map((basketItem, idx) => (
					<BasketItem basketItem={basketItem} key={idx} /> 
				)) : null
				}
			</ul>
		</div>
	);
}

const mapStateToProps = (state) => {
	return { basket: state.basket }
}

export default connect(mapStateToProps)(BasketItemPanel)
