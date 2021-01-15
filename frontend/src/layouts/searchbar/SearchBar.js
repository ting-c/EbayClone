import React , { useState } from 'react'
import './styles.scss'
import { useHistory } from 'react-router-dom'
import itemAPI from '../../api/itemAPI'
import { updateDisplayItems } from '../../redux/displayItems/displayItemsAction'
import { connect } from 'react-redux'

const SearchBar = ({ updateDisplayItems }) => {

	const [searchTerm, setSearchTerm] = useState("");

	let history = useHistory();
	
	const handleSubmit = async (e) => {
		e.preventDefault(); 
		const items = searchTerm ? 
			await itemAPI.getByTitle(searchTerm) : 
			// get all items if searchTerm is null
			await itemAPI.get();
		console.log(items)
		updateDisplayItems(items);
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

const mapDispatchToProps = (dispatch) => {
	return {
		updateDisplayItems: (items) => dispatch(updateDisplayItems(items))
	}
}

export default connect(null, mapDispatchToProps)(SearchBar);
