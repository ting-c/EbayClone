import React , { useState } from 'react'
import './styles.scss'
import { useHistory, withRouter, Link } from 'react-router-dom'
import { fetchItemsAsync } from '../../redux/displayItems/displayItemsAction'
import { connect } from 'react-redux'
import Logo from '../../images/logo.png'

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
			<div className='logo-container'>
				<Link to='/'>
					<img src={Logo} alt='Logo' className='logo'/>
				</Link>
			</div>
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

export default withRouter(connect(null, mapDispatchToProps)(SearchBar))
