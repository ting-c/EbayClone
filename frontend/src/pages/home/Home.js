import React, { useEffect } from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'
import { connect } from 'react-redux'
import { fetchItemsAsync } from '../../redux/displayItems/displayItemsAction'

const Home = ({ displayItems, fetchItemsAsync }) => {

	useEffect(() => {
		fetchItemsAsync();
	},[]);

	return (
		<div>
			<ItemContainer items={displayItems}/>
		</div>
	)
}

const mapStateToProps = (state) => {
	return { displayItems: state.displayItems }
};

const mapDispatchToProps = { fetchItemsAsync };

export default connect(mapStateToProps, mapDispatchToProps)(Home);
