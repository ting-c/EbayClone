import React, { useState } from 'react'
import { withRouter } from 'react-router-dom'
import authAPI from '../../api/authAPI'

const SignUp = ({ history }) => {
	const [firstName, setFirstName] = useState('');
	const [lastName, setLastName] = useState('');
	const [email, setEmail] = useState('');
	const [address, setAddress] = useState('');
	const [username, setUsername] = useState('');
	const [password, setPassword] = useState('');
	const [confirmPassword, setConfirmPassword] = useState('');

	async function handleSubmit(e){
		e.preventDefault();
		if (confirmPassword !== password) {
			alert('Please enter the same passwords');
			return;
		}
		const data = {
			firstName, lastName, email, address, username, password 
		}
		const result = await authAPI.signup(data);
		if (result) {
			history.push('/signin');
		}
	}

	return (
		<form onSubmit={handleSubmit}>
			<div className="mb-3">
				<label htmlFor="firstNameInput" className="form-label">
					First Name
				</label>
				<input
					type="text"
					className="form-control"
					id="firstNameInput"
					aria-describedby="firstNameInput"
					value={firstName}
					required
					onChange={(e) => setFirstName(e.target.value)}
				/>
			</div>
			<div className="mb-3">
				<label htmlFor="lastNameInput" className="form-label">
					Last Name
				</label>
				<input
					type="text"
					className="form-control"
					id="lastNameInput"
					aria-describedby="lastNameInput"
					value={lastName}
					required
					onChange={(e) => setLastName(e.target.value)}
				/>
			</div>
			<div className="mb-3">
				<label htmlFor="emailInput" className="form-label">
					Email address
				</label>
				<input
					type="email"
					className="form-control"
					id="emailInput"
					aria-describedby="emailInput"
					value={email}
					onChange={(e) => setEmail(e.target.value)}
				/>
			</div>
			<div className="mb-3">
				<label htmlFor="addressInput" className="form-label">
					Address
				</label>
				<input
					type="text"
					className="form-control"
					id="addressInput"
					aria-describedby="addressInput"
					value={address}
					placeholder='optional'
					onChange={(e) => setAddress(e.target.value)}
				/>
			</div>
			<div className="mb-3">
				<label htmlFor="usernameInput" className="form-label">
					Username
				</label>
				<input
					type="text"
					className="form-control"
					id="usernameInput"
					aria-describedby="usernameInput"
					value={username}
					required
					onChange={(e) => setUsername(e.target.value)}
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
			<div className="mb-3">
				<label htmlFor="confirmPasswordInput" className="form-label">
					Confirm Password
				</label>
				<input
					type="password"
					className="form-control"
					id="confirmPasswordInput"
					value={confirmPassword}
					required
					onChange={(e) => setConfirmPassword(e.target.value)}
				/>
			</div>
			<button type="submit" className="btn btn-primary">
				Submit
			</button>
		</form>
	);
}

export default withRouter(SignUp);
