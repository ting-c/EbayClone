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
	return { displayItems: state.displayItems };
};

export default connect(mapStateToProps)(SearchResults);
