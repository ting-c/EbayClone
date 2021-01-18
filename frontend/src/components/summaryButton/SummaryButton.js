import React from 'react'
import './styles.scss'

const SummaryButton = ({ text, styles, onAddToBasket }) => {

	return (
		<button className={`summary-button btn ${styles}`} onClick={onAddToBasket}>
			{text}
		</button>
	)
}

export default SummaryButton
