import React from 'react'
import { connect } from 'react-redux'
import { Redirect } from 'react-router-dom'

const ProtectedRoute = ({ component: Component, user }) => {
	if (!user) {
		return (<Redirect to='/signin' />)
	} else {
		return (
			<Component/>
		)
	}

}

const mapStateToProps = (state) => {
	return { user: state.user }
}

export default connect(mapStateToProps)(ProtectedRoute)
