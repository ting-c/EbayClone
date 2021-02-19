import React, { useEffect } from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'
import { connect } from 'react-redux'
import { fetchItemsAsync } from '../../redux/displayItems/displayItemsAction'
import SearchBar from '../../layouts/searchbar/SearchBar'

const Home = ({ displayItems, fetchItemsAsync }) => {

	useEffect(() => {
		fetchItemsAsync();
	},[]);

	return (
		<div>
			<SearchBar />
			<ItemContainer items={displayItems}/>
		</div>
	)
}

const mapStateToProps = (state) => {
	return { displayItems: state.displayItems }
};

const mapDispatchToProps = { fetchItemsAsync };

export default connect(mapStateToProps, mapDispatchToProps)(Home);
