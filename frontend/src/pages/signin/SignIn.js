import React, { useState } from "react";
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { signInAsync } from "../../redux/auth/authAction";

const SignIn = ({ signInAsync }) => {
	
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');

	async function handleSubmit(e){
		e.preventDefault();
		const data = { email, password };
		signInAsync(data);
	};

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

const mapDispatchToProps = { signInAsync };

export default withRouter(connect(null, mapDispatchToProps)(SignIn));
