import React from 'react'
import './styles.scss'

const Price = ({ price }) => {
	return (
		<div className='price'>
			<span id='label'>Price:</span>
			<span id='price-display'>£{price.toFixed(2)}</span>
		</div>
	)
}

export default Price
