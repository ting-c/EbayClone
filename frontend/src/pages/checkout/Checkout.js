import React from 'react'
import './style.scss'
import { connect } from 'react-redux'
import { withRouter } from 'react-router-dom'
import axios from 'axios'
import { removeBasketItemAsync } from "../../redux/basket/basketAction";

const Checkout = ({ jwt, user, basket, history, removeBasketItemAsync }) => {

	const total = basket.reduce((acc, {item, quantity}) => 
		acc + (item.price * quantity)
	,0);

	const confirmOrder = async () => {
		const BASE_URL = "https://localhost:5001/api/order";
		const config = { headers: { 'Authorization': `Bearer ${jwt}`}};
		const basketItems = basket;
		const data = { basketItems };
		try {
			const response = await axios.post(BASE_URL, data, config);
			console.log('21')
			if (response.status === 200) {
				// clear basket
				for (let basketItem of basketItems) {
					removeBasketItemAsync(jwt, basketItem);
				}
				const orderId = response.data.id;
				alert('Order confirmed');
				history.push(`/order/${orderId}`);
			}
		} catch (err) {
			console.log('Error' , err)
		}
	}

	return (
		<div className="basket-page">
			{basket.length === 0 ? (
				<div>No items in basket</div>
			) : (
			<React.Fragment>
				<div className="left-panel">
					<div className="address">
						<b>Post to</b>
						<p>{`${user.firstName} ${user.lastName}`}</p>
						<p>{user.address}</p>
					</div>
					<div className="items">
						<h6>Review items</h6>
						<ul className="item-list">
							{basket.map(({ item, quantity }) => (
								<React.Fragment key={item.id}>
									<small>
										Seller: {item.seller.userName}
									</small>
									<li className="item list-group-item">
										<div>
											<img
												src={item.imageUrl[0].urlPath}
												alt={item.Title}
											/>
										</div>
										<div>
											<p>{item.title}</p>
											<b>£{item.price.toFixed(2)}</b>
											<p>Quantity: {quantity}</p>
										</div>
									</li>
								</React.Fragment>
							))}
						</ul>
					</div>
				</div>
				<div className="right-panel">
					<div className="summary">
						<div>
							<span>Subtotal ({basket.length} items)</span>
							<span className="float-right">
								£{total.toFixed(2)}
							</span>
						</div>
						<hr className="w-100" />
						<div className="total">
							<span>Order total</span>
							<span className="float-right">
								£{total.toFixed(2)}
							</span>
						</div>
						<button className="btn btn-primary" onClick={confirmOrder}>
							Confirm order
						</button>
					</div>
				</div>
			</React.Fragment>
			)}
		</div>
	);
}

const mapStateToProps = (state) => {
	return { 
		basket: state.basket,	
		jwt: state.jwt,	
		user: state.user,	
	}
}

const mapDispatchToProps = { removeBasketItemAsync }

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Checkout))
