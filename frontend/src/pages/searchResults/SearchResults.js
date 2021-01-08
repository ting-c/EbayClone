import React from 'react'
import ItemContainer from '../../components/itemContainer/ItemContainer';

const SearchResults = ({ items }) => {
	return (
		<div>
			<ItemContainer items={items} />
		</div>
	);
};

export default SearchResults
