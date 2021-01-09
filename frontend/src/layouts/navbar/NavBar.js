import React from 'react'
import { Link } from 'react-router-dom'
import './styles.scss'

const NavBar = ({ user }) => {
	return (
		<nav className="navbar navbar-expand-lg">
			<div className="container-fluid">
				<div className="dropdown">
					<button
						className="dropdown-toggle"
						id="dropdownMenuButton"
						data-bs-toggle="dropdown"
						aria-expanded="false"
					>
						Hello. { user ? user.firstName : (
							<span className='signin-signup'>
								<Link to='/signin'>Sign in</Link> or <Link to='/signup'>register</Link>
							</span>
							)}
					</button>
					<ul
						className="dropdown-menu"
						aria-labelledby="dropdownMenuButton"
					>
						<li><a href='/signin'>Sign in</a></li>
					</ul>
				</div>

				<ul className="navbar-right navbar-nav mr-auto mb-2 mb-lg-0">
					<li className="nav-item nav-link">
						<Link to={user ? '/selling' : '/signin'}>
							Sell
						</Link>
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
		</nav>
	);
}

export default NavBar
