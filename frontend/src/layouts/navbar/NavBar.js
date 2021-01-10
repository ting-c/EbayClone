import React from 'react'
import { Link } from 'react-router-dom'
import './styles.scss'

const NavBar = ({ user, setUserAndJwt }) => {

	function signout(){
		setUserAndJwt(null, null);
	};
	
	return (
		<nav className="navbar navbar-expand-lg navbar-light">
			<div className="container-fluid">
				<span>Hello.</span>
				{user ? (
					<span id="firstName">{user.firstName}</span>
				) : (
					<span className="signin-signup">
						<span><Link to="/signin">Sign in</Link></span>
						<span>or</span>
						<span><Link to="/signup"> register</Link></span>
					</span>
				)}
				{ user ? (
					<div className="dropdown">
						<button
							className="dropdown-toggle"
							id="dropdownMenuButton"
							data-bs-toggle="dropdown"
							aria-expanded="false"
						></button>
						<ul
							className="dropdown-menu"
							aria-labelledby="dropdownMenuButton"
						>
							<li id="signout" onClick={signout}>
								<span>Sign out</span>
							</li>
						</ul>
					</div> 
				) : null}

				<button
					className="navbar-toggler"
					type="button"
					data-bs-toggle="collapse"
					data-bs-target="#navbarSupportedContent"
					aria-controls="navbarSupportedContent"
					aria-expanded="false"
					aria-label="Toggle navigation"
				>
					<span className="navbar-toggler-icon"></span>
				</button>
				<div
					className="collapse navbar-collapse"
					id="navbarSupportedContent"
				>
					<ul className="navbar-right navbar-nav mr-auto mb-2 mb-lg-0">
						<li className="nav-item nav-link">
							<Link to={user ? "/selling" : "/signin"}>Sell</Link>
						</li>
						<li className="nav-item nav-link">
							<div className="dropdown">
								<button
									className="dropdown-toggle"
									id="dropdownMenuButton"
									data-bs-toggle="dropdown"
									aria-expanded="false"
								>
									My eBay
								</button>
								<ul
									className="dropdown-menu"
									aria-labelledby="dropdownMenuButton"
								>
									<li></li>
								</ul>
							</div>
						</li>
					</ul>
				</div>
			</div>
		</nav>
	);
}

export default NavBar
