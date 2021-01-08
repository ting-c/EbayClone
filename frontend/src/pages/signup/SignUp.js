import React, { useState } from 'react'

const SignUp = () => {
	const [firstName, setFirstName] = useState(null);
	const [lastName, setLastName] = useState(null);
	const [email, setEmail] = useState(null);
	const [address, setAddress] = useState(null);
	const [username, setUsername] = useState(null);
	const [password, setPassword] = useState(null);
	const [confirmPassword, setConfirmPassword] = useState(null);

	return (
		<form>
			<div class="mb-3">
				<label for="firstNameInput" className="form-label">
					First Name
				</label>
				<input
					type="text"
					className="form-control"
					id="firstNameInput"
					aria-describedby="firstNameInput"
					value={firstName}
					onChange={(e) => setFirstName(e.target.value)}
				/>
			</div>
			<div class="mb-3">
				<label for="lastNameInput" className="form-label">
					Last Name
				</label>
				<input
					type="text"
					className="form-control"
					id="lastNameInput"
					aria-describedby="lastNameInput"
					value={lastName}
					onChange={(e) => setLastName(e.target.value)}
				/>
			</div>
			<div class="mb-3">
				<label for="emailInput" className="form-label">
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
			<div class="mb-3">
				<label for="addressInput" class="form-label">
					Address
				</label>
				<input
					type="text"
					className="form-control"
					id="addressInput"
					aria-describedby="addressInput"
					value={address}
					onChange={(e) => setAddress(e.target.value)}
				/>
			</div>
			<div class="mb-3">
				<label for="usernameInput" class="form-label">
					Username
				</label>
				<input
					type="text"
					className="form-control"
					id="usernameInput"
					aria-describedby="usernameInput"
					value={username}
					onChange={(e) => setUsername(e.target.value)}
				/>
			</div>
			<div className="mb-3">
				<label for="passwordInput" class="form-label">
					Password
				</label>
				<input
					type="password"
					className="form-control"
					id="passwordInput"
					value={password}
					onChange={(e) => setPassword(e.target.value)}
				/>
			</div>
			<div className="mb-3">
				<label for="confirmPasswordInput" className="form-label">
					Confirm Password
				</label>
				<input
					type="password"
					className="form-control"
					id="confirmPasswordInput"
					value={confirmPassword}
					onChange={(e) => setConfirmPassword(e.target.value)}
				/>
			</div>
			<button type="submit" className="btn btn-primary">
				Submit
			</button>
		</form>
	);
}

export default SignUp
