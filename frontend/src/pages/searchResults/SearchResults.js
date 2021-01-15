import React from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'
import { connect } from 'react-redux'

const SearchResults = ({ displayItems }) => {

	return (
		<div>
			<h5>Search Results</h5>
			<ItemContainer items={displayItems} />
		</div>
	);
};

const mapStateToProps = (state) => {
	const { displayItems } = state;
	return displayItems;
};

export default connect(mapStateToProps)(SearchResults);
