import React from 'react'
import Price from '../../components/price/Price'
import SummaryButton from '../../components/summaryButton/SummaryButton'
import './style.scss'

const SummaryMain = ({ price }) => {

	return (
		<div className="summary-main">
			<Price price={price} />
			<div className="summary-button-group">
				<SummaryButton text="Buy it now" styles="primary" />
				<SummaryButton text="Add to basket" styles="info" />
				<SummaryButton text="♡ Watch this item" styles="outline-primary" />
			</div>
		</div>
	);
}

export default SummaryMain
