import React from 'react'
import { connect } from 'react-redux'
import { Redirect, Route, withRouter } from 'react-router-dom'

const ProtectedRoute = ({ component: Component, user, path }) => {
	if (!user) {
		return (<Redirect to='/signin' />)
	} else {
		return (
			<Route path={path}>
				<Component />
			</Route>
		);
	}

}

const mapStateToProps = (state) => {
	return { user: state.user }
}

export default withRouter(connect(mapStateToProps)(ProtectedRoute))
