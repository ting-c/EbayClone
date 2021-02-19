import axios from 'axios'
import React, { useEffect, useState } from 'react'
import './styles.scss'
import { connect } from 'react-redux'
import { withRouter, Link } from 'react-router-dom'

const Order = ({ match, jwt }) => {

	const [order, setOrder] = useState(null);
	
	useEffect(() => {
		if (!order) {
			fetchOrderAsync();
		}
	})
	
	const fetchOrderAsync = async() => {
		const orderId = match.params.orderId;
		const url = `https://localhost:5001/api/order/${orderId}`;
		const config = { headers: { Authorization: `Bearer ${jwt}` }};
		const response = await axios.get(url, config);
		if (response) {
			setOrder(response.data);
		}; 
	}

	const date = order ? order.date.slice(0,10) : '';
	const time = order ? order.date.slice(11,16) : '';

	return order ? (
		<div className="order-page">
			<h5>Order details</h5>
			<p>
				<span>Date of purchase: </span>
				<span>
					{date} at {time}
				</span>
			</p>
			<ul>
				{ order.items.map(({ item, quantity, price }) => (
					<li className="order-item" key={item.id}>
						<Link to={`/item/${item.id}`}>
							<div className="image-container">
								<img
									className="image"
									src={item.imageUrl[0].urlPath}
									alt={item.title}
								/>
							</div>
						</Link>
						<div>
							<p>{item.title}</p>
							<p>
								<span>Quantity: </span>
								{quantity}
							</p>
							<p>
								<span>Seller: </span>
								{item.seller.userName}
							</p>
							<p>
								<span>Price: </span>
								£{price.toFixed(2)}
							</p>
						</div>
					</li>
				))}
			</ul>
		</div>
	) : (
		<div>Loading</div>
	);
}

const mapStateToProps = (state) => {
	return { jwt: state.jwt }
}

export default withRouter(connect(mapStateToProps)(Order))
