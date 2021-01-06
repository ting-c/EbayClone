import React from 'react';

const SignIn = () => {
	return (
		<form>
			<div class="mb-3">
				<label for="emailInput" class="form-label">Email address</label>
				<input type="email" class="form-control" id="emailInput" aria-describedby="emailHelp" />
			</div>
			<div class="mb-3">
				<label for="passwordInput" class="form-label">Password</label>
				<input type="password" class="form-control" id="passwordInput" />
			</div>
			<button type="submit" class="btn btn-primary">Submit</button>
		</form>
	)
}

export default SignIn
