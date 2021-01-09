import React from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer'

const SearchResults = ({ items }) => {

	return (
		<div>
			<h5>Search Results</h5>
			<ItemContainer items={items} />
		</div>
	);
};

export default SearchResults
