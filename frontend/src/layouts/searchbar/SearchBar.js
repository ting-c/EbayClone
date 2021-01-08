import React from 'react'
import itemAPI from '../../api/itemAPI';
import './styles.scss'

const SearchBar = ({ setItems}) => {
	
	const handleSubmit = async (e) => {
		e.preventDefault();
		const items = await itemAPI.get();
		setItems(items);
	}

	return (
		<form className="searchbar d-flex p-3" onSubmit={handleSubmit}>
			<input
				className="form-control"
				type="search"
				placeholder="Search for anything"
				aria-label="Search"
			/>
			<button className="search-button btn btn-primary" type="submit">
				Search
			</button>
		</form>
	);
}

export default SearchBar
