import React from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'
import { connect } from 'react-redux'

const SearchResults = ({ items }) => {

	return (
		<div>
			<h5>Search Results</h5>
			<ItemContainer items={items} />
		</div>
	);
};

const mapStateToProps = (state) => {
	const { items } = state;
	return items;
};

export default connect(mapStateToProps)(SearchResults);
