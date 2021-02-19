import React, { useEffect, useState } from 'react'
import './styles.scss'
import { connect } from 'react-redux'
import { Link, withRouter } from 'react-router-dom'
import axios from 'axios'

const SellingItems = ({ jwt }) => {

	const [items, setItems] = useState([]);

	useEffect(() => {
		fetchItemsAsync()
	},[])

	const fetchItemsAsync = async () => {
		const URL = "https://localhost:5001/api/items/selling"
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};
		let response = await axios.get(URL, config);
		console.log(response)
		let items = response.data;
		if (items) {
			setItems(response.data);
		}
	}

	if (items.length === 0){
		return (<h5 className='my-3'>No selling items</h5>)
	} else {
		return (
			<div className='selling-items-page'>
				<h5 className='header'>Selling Items</h5>
				<ul>
				{ items.map(item => (
						<li className='item'>
							<Link to={`/item/${item.id}`}>
							<div className='img-container'>
								<img src={item.imageUrl[0].urlPath} alt={item.title}/>
							</div>
							</Link>
							<div className='details'>
								<p className='title'>{item.title}</p>
								<p><span>Quantity available: </span>{item.quantity}</p>
								<p><span>Selling Price: £</span>{item.price.toFixed(2)}</p>
							</div>
						</li>
				))}
				</ul>
			</div>
		)
	}
}

const mapStateToProps = (state) => {
	return { jwt: state.jwt }
}

export default withRouter(connect(mapStateToProps)(SellingItems))
