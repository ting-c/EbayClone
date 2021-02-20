import React from 'react'
import './styles.scss'

const SummaryTop = ({ item }) => {
	return (
		<div className="summary-top">
			{ item.description ? (
				<React.Fragment>
					<div className="align-right">Description:</div>
					<div className="align-left">{item.description}</div>
				</React.Fragment>
			) : null }
			<div className="align-right">Condition:</div>
			<div className="align-left">{item.condition}</div>
			<div className="align-right">Quantity available:</div>
			<div className="align-left">{item.quantity}</div>
		</div>
	);
}

export default SummaryTop
