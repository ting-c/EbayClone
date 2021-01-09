import React from 'react'
import SummaryTop from '../../components/summaryTop/SummaryTop'
import SummaryMain from '../summaryMain/SummaryMain';
import SummaryTitle from '../summaryTitle/SummaryTitle';
import './styles.scss'

const SummaryPanel = ({ item }) => {
	return (
		<div className="summary-panel">
			<SummaryTitle title={item.title} />
			<hr />
			<SummaryTop item={item} />
			<hr />
			<SummaryMain price={item.price} />
			<hr />
			
		</div>
	);
}

export default SummaryPanel
