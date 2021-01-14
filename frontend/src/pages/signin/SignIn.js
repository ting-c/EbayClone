import React, { useState } from "react";
import { withRouter } from 'react-router-dom'
import authAPI from "../../api/authAPI"
import { connect } from 'react-redux'
import { addUser } from "../../redux/auth/userAction";
import { addJwt } from "../../redux/auth/jwtAction";

const SignIn = ({ addUser, addJwt, history }) => {
	
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');

	async function handleSubmit(e){
		e.preventDefault();
		const data = { email, password }
		const responseData = await authAPI.signin(data);
		console.log(responseData);
		if (responseData) {
			const {user, jwtString} = responseData;
			addUser(user);
			addJwt(jwtString);
			history.push('/');
		}
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

const mapDispatchToProps = (dispatch) => {
	return {
		addUser : (user) => dispatch(addUser(user)),
		addJwt : (jwt) => dispatch(addJwt(jwt)),
	}
}

export default withRouter(connect(null, mapDispatchToProps)(SignIn));
