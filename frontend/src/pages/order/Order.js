import axios from 'axios'
import React, { useEffect, useState } from 'react'
import './styles.scss'
import { connect } from 'react-redux'
import { withRouter } from 'react-router-dom'

const Order = ({ match, jwt }) => {

	const orderId = match.params.orderId;

	useEffect(() => {
		fetchOrderAsync();
	}, [])
	
	const fetchOrderAsync = async() => {
		const url = `https://localhost:5001/api/order/${orderId}`;
		const config = { headers: { Authorization: `Bearer ${jwt}` }};
		try {
			const response = await axios.get(url, config);
			if (response) {
				setOrder(response.data);
			}; 
		} catch (err) {
			console.log(err)
		}
	}

	const [order, setOrder] = useState(null)
	const date = order ? order.date.slice(0,10) : '';
	const time = order ? order.date.slice(11,19) : '';
	console.log(order);
	return (
		order ? (
		<div className='order-page'>
			<h6>Order details</h6>
			<p>
				<span>Date of purchase: </span>
				<span>{date} at {time}</span>
			</p>
			<ul>
				{order.items.map(item => (
					<li>{item.title}</li>
				))}
			</ul>
		</div>
		) : (
			<div>Loading</div>		
		)
	)
}

const mapStateToProps = (state) => {
	return { jwt: state.jwt }
}

export default withRouter(connect(mapStateToProps)(Order))
