import React, { useState } from "react";
import { Redirect, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { signInAsync } from "../../redux/auth/authAction";
import { addExistingBasketItemsToDbAsync } from "../../redux/basket/basketAction";

const SignIn = ({ signInAsync, jwt, basket, addExistingBasketItemsToDbAsync }) => {
	
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
	
	async function handleSubmit(e){
		e.preventDefault();
		const data = { email, password };
		let { jwtString: jwt } = await signInAsync(data);
		if (jwt) {
			await addExistingBasketItemsToDbAsync(jwt, basket);
		}
	};
	
	if (jwt) {
		// Redirect to main page if authenticated
		return (<Redirect to='/' />)
	}

	return (
		<form onSubmit={handleSubmit}>
			<div className="mb-3">
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
		</form>
	);
}

const mapStateToProps = (state) => {
	return { 
		jwt: state.jwt,
		basket: state.basket 
	}
};

const mapDispatchToProps = { signInAsync, addExistingBasketItemsToDbAsync };

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(SignIn));
