import React , { useState } from 'react'
import './styles.scss'
import { useHistory } from 'react-router-dom'
import { fetchItemsAsync } from '../../redux/displayItems/displayItemsAction'
import { connect } from 'react-redux'

const SearchBar = ({ fetchItemsAsync }) => {

	const [searchTerm, setSearchTerm] = useState("");

	let history = useHistory();
	
	const handleSubmit = (e) => {
		e.preventDefault(); 
		fetchItemsAsync(searchTerm)
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

const mapDispatchToProps = { fetchItemsAsync };

export default connect(null, mapDispatchToProps)(SearchBar);
