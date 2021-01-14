import React, { useEffect } from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'
import itemAPI from '../../api/itemAPI'
import { connect } from 'react-redux'
import { updateDisplayItems } from '../../redux/displayItems/displayItemsAction'

const Home = ({ items, updateDisplayItems }) => {

	useEffect(() => {
		if (!items)
			fetchItems();
	});

	async function fetchItems() {
		const items = await itemAPI.get();
		updateDisplayItems(items);
	}

	return (
		<div>
			<ItemContainer items={items}/>
		</div>
	)
}

const mapStateToProps = (state) => {
	const { items } = state;
	return items;
};

const mapDispatchToProps = (dispatch) => {
	return {
		updateDisplayItems: (items) => dispatch(updateDisplayItems(items))
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(Home);
