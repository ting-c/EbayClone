import React from 'react'
import './styles.scss'

const SummaryTop = ({ item }) => {
	return (
		<div className="summary-top">
			<div className="align-right">Condition:</div>
			<div className="align-left">{item.condition}</div>
			<div className="align-right">Quantity:</div>
			<div className="align-left">1</div>
		</div>
	);
}

export default SummaryTop
