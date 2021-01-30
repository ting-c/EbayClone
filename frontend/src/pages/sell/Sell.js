import axios from 'axios';
import React, { useState } from 'react'
import { useHistory } from 'react-router-dom'
import { connect } from 'react-redux';
import getErrorMessage from '../../redux/error/errorMessage';
import './styles.scss'

const Sell = ({ jwt }) => {

	const [title, setTitle] = useState('');
	const [description, setDescription] = useState('');
	const [price, setPrice] = useState(null);
	const [condition, setCondition] = useState('');

	const [errorMessage, setErrorMessage] = useState(null);

	const history = useHistory();

	async function handleSubmit() {
		const data = {
			title, description, price, condition, isAuction: false
		};
		const config = {
			headers: { 'Authorization': `Bearer ${jwt}` }
		};
		const BASE_URL = "https://localhost:5001/api/items";
		try {
			const response = await axios.post(BASE_URL, data, config);
			const { id } = response.data;
			history.push(`/upload/${id}`)
		} catch (error) {
			const errorMessage = getErrorMessage(error);
			setErrorMessage(errorMessage);
		}
	};

	return (
		<div className="sell-page">
			<form onSubmit={handleSubmit}>
				<div class="form-group">
					<label for="title">Title</label>
					<input
						type="text"
						class="form-control"
						id="title"
						value={title}
						onChange={(e) => setTitle(e.target.value)}
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
	return { jwt: state.jwt }
}

export default connect(mapStateToProps)(Sell);
