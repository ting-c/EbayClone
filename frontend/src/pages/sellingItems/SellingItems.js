import React, { useEffect, useState } from 'react'
import './styles.scss'
import { connect } from 'react-redux'
import { Link, withRouter } from 'react-router-dom'
import axios from 'axios'
import EmptyImage from '../../images/empty.png'
import getErrorMessage from '../../redux/error/errorMessage'
import { displayErrorMessage, errorActionTypes } from '../../redux/error/errorAction'

const SellingItems = ({ jwt, history }) => {

	const [items, setItems] = useState([]);

	useEffect(() => {
			fetchItemsAsync();	
	},[])

	const fetchItemsAsync = async () => {
		console.log('run fetch')
		const URL = "https://localhost:5001/api/items/selling"
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};
		try {
			let response = await axios.get(URL, config);
			if (response) {
				setItems(response.data);
			}
		} catch (error) {
			const errorMessage = getErrorMessage(error);
			displayErrorMessage(
				errorActionTypes.FETCH_SELLING_ITEMS_ERROR, 
				errorMessage
			);
		}
	}

	const removeItem = async (itemId) => {
		const config = {
			headers: { Authorization: `Bearer ${jwt}` },
		};
		const url = `https://localhost:5001/api/items/${itemId}`;
		try {
			let response = await axios.delete(url, config);
			console.log(response);
			if (response.status === 204) {
				console.log('remove success')
				await fetchItemsAsync()
			}
		} catch (error) {
			const errorMessage = getErrorMessage(error);
			displayErrorMessage(
				errorActionTypes.REMOVE_SELLING_ITEM_ERROR,
				errorMessage
			);
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
								<img src={ item.imageUrl.length > 0 ? 
									item.imageUrl[0].urlPath : 
									EmptyImage
									} 
									alt={item.title}/>
							</div>
							</Link>
							<div className='details'>
								<p className='title'>{item.title}</p>
								<p><span>Quantity available: </span>{item.quantity}</p>
								<p><span>Selling Price: £</span>{item.price.toFixed(2)}</p>
								<button className='btn btn-warning' onClick={() => removeItem(item.id)}>Remove</button>
								<button className='btn btn-primary' onClick={() => history.push(`/upload/${item.id}`)}>Upload images</button>
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

const mapDispatchToProps = {
	displayErrorMessage : (errorType, errorMessage) => displayErrorMessage(errorType, errorMessage)
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(SellingItems))
