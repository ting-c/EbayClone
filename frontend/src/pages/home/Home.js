import React, { useEffect } from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'
import itemAPI from '../../api/itemAPI'

const Home = ({ items, setItems }) => {

	useEffect(() => {
		if (!items){
			fetchItems();
		}
	})

	async function fetchItems() {
		const items = await itemAPI.get();
		setItems(items);
	}

	return (
		<div>
			<ItemContainer items={items}/>
		</div>
	)
}

export default Home
