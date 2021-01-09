import React , { useState } from 'react'
import { useHistory } from 'react-router-dom'
import itemAPI from '../../api/itemAPI'
import './styles.scss'

const SearchBar = ({ setItems }) => {

	const [searchTerm, setSearchTerm] = useState("");

	let history = useHistory();
	
	const handleSubmit = async (e) => {
		e.preventDefault(); 
		const items = searchTerm ? 
			await itemAPI.getByTitle(searchTerm) : 
			// get all items if searchTerm is null
			await itemAPI.get();
		setItems(items);
		history.push("/results");
	}

	return (
		<form className="searchbar d-flex p-3" onSubmit={handleSubmit}>
			<input
				className="form-control"
				type="search"
				placeholder="Search for anything"
				aria-label="Search"
				value={searchTerm}
				onChange={(e) => setSearchTerm(e.target.value)}
			/>
			<button className="search-button btn btn-primary" type="submit">
				Search
			</button>
		</form>
	);
}

export default SearchBar
