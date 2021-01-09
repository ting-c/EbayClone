import React from 'react'
import './styles.scss'

const SummaryButton = ({ text, styles }) => {

	return (
		<button className={`summary-button btn ${styles}`}>
			{text}
		</button>
	)
}

export default SummaryButton
