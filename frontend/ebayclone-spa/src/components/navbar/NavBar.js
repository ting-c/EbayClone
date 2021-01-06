import React from 'react'
import './styles.scss'

const NavBar = () => {
	return (
		<nav class="navbar navbar-expand-lg">
			<div class="container-fluid">
				<div class="dropdown">
					<button
						class="dropdown-toggle"
						id="dropdownMenuButton"
						data-bs-toggle="dropdown"
						aria-expanded="false"
					>
						Hello.
					</button>
					<ul
						class="dropdown-menu"
						aria-labelledby="dropdownMenuButton"
					>
						<li><a href='/signin'>Sign in</a></li>
					</ul>
				</div>

				<ul class="navbar-right navbar-nav mr-auto mb-2 mb-lg-0">
					<li class="nav-item">
						<a class="nav-link" href="/">
							Sell
						</a>
					</li>
					<li class="nav-item nav-link">
						<div class="dropdown">
							<button
								class="dropdown-toggle"
								id="dropdownMenuButton"
								data-bs-toggle="dropdown"
								aria-expanded="false"
							>
								My eBay
							</button>
							<ul
								class="dropdown-menu"
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
