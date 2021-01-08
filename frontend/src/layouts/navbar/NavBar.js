import React from 'react'
import './styles.scss'

const NavBar = () => {
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
						Hello.
					</button>
					<ul
						className="dropdown-menu"
						aria-labelledby="dropdownMenuButton"
					>
						<li><a href='/signin'>Sign in</a></li>
					</ul>
				</div>

				<ul className="navbar-right navbar-nav mr-auto mb-2 mb-lg-0">
					<li className="nav-item">
						<a className="nav-link" href="/">
							Sell
						</a>
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
