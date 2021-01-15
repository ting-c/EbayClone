import React, { useEffect } from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'
import itemAPI from '../../api/itemAPI'
import { connect } from 'react-redux'
import { updateDisplayItems } from '../../redux/displayItems/displayItemsAction'

const Home = ({ displayItems, updateDisplayItems }) => {

	useEffect(() => {
		fetchItems();
	});

	async function fetchItems() {
		const items = await itemAPI.get();
		updateDisplayItems(items);
	}

	return (
		<div>
			<ItemContainer items={displayItems}/>
		</div>
	)
}

const mapStateToProps = (state) => {
	const { displayItems } = state;
	return displayItems;
};

const mapDispatchToProps = (dispatch) => {
	return {
		updateDisplayItems: (items) => dispatch(updateDisplayItems(items))
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(Home);
