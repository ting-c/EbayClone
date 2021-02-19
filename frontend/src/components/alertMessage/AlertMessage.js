import React from 'react'
import { connect } from "react-redux";
import { clearError } from "../../redux/error/errorAction";

const AlertMessage = ({ error, clearError }) => {
	return (
		error ? (
			<div
				class="alert alert-warning alert-dismissible fade show"
				role="alert"
			>
				<strong>{error}</strong>
				<button
					type="button"
					class="close"
					data-dismiss="alert"
					aria-label="Close"
					onClick={clearError}
				>
					<span aria-hidden="true">&times;</span>
				</button>
			</div>
		) : null
	);
}

const mapStateToProps = (state) => {
	return { error: state.error };
};

const mapDispatchToProps = { clearError };

export default connect(mapStateToProps, mapDispatchToProps)(AlertMessage)
