import axios from 'axios';
import React, { useState } from 'react'
import { useHistory } from 'react-router-dom'
import { connect } from 'react-redux';
import getErrorMessage from '../../redux/error/errorMessage';
import { errorActionTypes, displayErrorMessage } from '../../redux/error/errorAction';
import './styles.scss'

const Sell = ({ jwt, displayErrorMessage }) => {

	const [title, setTitle] = useState('');
	const [description, setDescription] = useState('');
	const [price, setPrice] = useState(null);
	const [quantity, setQuantity] = useState(null);
	const [condition, setCondition] = useState('');

	const history = useHistory();

	async function handleSubmit(e) {
		e.preventDefault();
		const data = {
			title, description, price, condition, quantity, isAuction: false
		};
		const config = {
			headers: { Authorization: `Bearer ${jwt}` }
		};
		const BASE_URL = "https://localhost:5001/api/items";
		try {
			const response = await axios.post(BASE_URL, data, config);
			const { id } = response.data;
			history.push(`/upload/${id}`)
		} catch (error) {
			const errorMessage = getErrorMessage(error);
			displayErrorMessage(
				errorActionTypes.SELL_ITEM_ERROR,
				errorMessage
			);
		}
	};

	return (
		<div className="sell-page">
			<h5 className="header">Sell an item</h5>
			<form onSubmit={handleSubmit}>
				<div class="form-group">
					<label for="title">Title</label>
					<input
						type="text"
						class="form-control"
						id="title"
						value={title}
						onChange={(e) => setTitle(e.target.value)}
						required
					/>
				</div>
				<div class="form-group">
					<label for="description">Description</label>
					<textarea
						class="form-control"
						id="description"
						value={description}
						onChange={(e) => setDescription(e.target.value)}
					/>
				</div>
				<div class="form-group">
					<label for="price">Price</label>
					<input
						type="number"
						class="form-control"
						min="0"
						step="any"
						id="price"
						value={price}
						onChange={(e) => setPrice(e.target.value)}
						required
					/>
				</div>
				<div class="form-group">
					<label for="condition">Quantity</label>
					<input
						type="number"
						class="form-control"
						min="0"
						step="1"
						id="quantity"
						value={quantity}
						onChange={(e) => setQuantity(e.target.value)}
						required
					/>
				</div>
				<div class="form-group">
					<label for="condition">Condition</label>
					<input
						type="text"
						class="form-control"
						id="condition"
						value={condition}
						onChange={(e) => setCondition(e.target.value)}
						required
					/>
				</div>
				<button type="submit" class="btn btn-primary">
					Add Item
				</button>
			</form>
		</div>
	);
}

const mapStateToProps = (state) => {
	return { 
		jwt: state.jwt }
}

const mapDispatchToProps = {
	displayErrorMessage
};

export default connect(mapStateToProps, mapDispatchToProps)(Sell);
