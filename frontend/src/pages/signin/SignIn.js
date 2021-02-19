import React, { useState } from "react";
import { Redirect, withRouter, Link } from 'react-router-dom'
import { connect } from 'react-redux'
import { signInAsync } from "../../redux/auth/authAction";
import { addExistingBasketItemsToDbAsync } from "../../redux/basket/basketAction";

const SignIn = ({ signInAsync, jwt, basket, addExistingBasketItemsToDbAsync }) => {
	
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
	
	async function handleSubmit(e){
		e.preventDefault();
		const data = { email, password };
		let responseData = await signInAsync(data);
		if (responseData) {
			const jwt = responseData.jwtString;
			await addExistingBasketItemsToDbAsync(jwt, basket);
		}
	};
	
	if (jwt) {
		// Redirect to main page if authenticated
		return (<Redirect to='/' />)
	}

	return (
		<div>
			<h5 className='my-3'>Sign in</h5>
			<hr />
			<form onSubmit={handleSubmit}>
				<div className="my-3">
					<label htmlFor="emailInput" className="form-label">
						Email address
					</label>
					<input
						type="email"
						className="form-control"
						id="emailInput"
						aria-describedby="emailHelp"
						value={email}
						required
						onChange={(e) => setEmail(e.target.value)}
					/>
				</div>
				<div className="mb-3">
					<label htmlFor="passwordInput" className="form-label">
						Password
					</label>
					<input
						type="password"
						className="form-control"
						id="passwordInput"
						value={password}
						required
						onChange={(e) => setPassword(e.target.value)}
					/>
				</div>
				<button type="submit" className="btn btn-primary">
					Login
				</button>
				<hr/>
				<div className='mt-3'><Link to='/signup'>Sign Up</Link></div>
			</form>
		</div>
	);
}

const mapStateToProps = (state) => {
	return { 
		jwt: state.jwt,
		basket: state.basket
	}
};

const mapDispatchToProps = { 
	signInAsync, 
	addExistingBasketItemsToDbAsync 
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(SignIn));
