import React from 'react'
import ItemCard from '../itemCard/ItemCard'
import './styles.scss'

const ItemContainer = ({ items }) => {
	return (
		<div className="item-container">
			{ items ? items.map((item, idx) => (
				<ItemCard item={item} key={idx} />
			)): null }
		</div>
	)
}

export default ItemContainer
