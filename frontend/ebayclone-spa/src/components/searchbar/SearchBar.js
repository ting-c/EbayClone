import React from 'react'
import './styles.scss'

const SearchBar = () => {
	return (
		<form class="searchbar d-flex p-3">
			<input
				class="form-control"
				type="search"
				placeholder="Search for anything"
				aria-label="Search"
			/>
			<button class="search-button btn btn-primary" type="submit">
				Search
			</button>
		</form>
	);
}

export default SearchBar
