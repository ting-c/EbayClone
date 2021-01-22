// import actions
import { addJwt, removeJwt, addUser, removeUser } from '../authAction';

// import reducer
import jwtReducer from '../jwtReducer';
import userReducer from '../userReducer';

// Testing userReducer
const user = { 
	id: 1,
	name: 'Mock User',
	sellingItems: { item: { id: 1, title: 'Item 1'} }
 };

describe('When dispatching addUser action', () => {
	const initialState = null
	const expectedState = user;
	it('should set state to user object', () => {
		const state = userReducer(initialState, addUser(user));
		expect(state).toEqual(expectedState);
		expect(state).not.toEqual(initialState);
	});
});

describe('When dispatching removeUser action', () => {
	const initialState = user;
	const expectedState = null;
	it('should set state to null', () => {
		const state = userReducer(initialState, removeUser(user));
		expect(state).toEqual(expectedState);
		expect(state).not.toEqual(initialState);
	});
});

// Testing jwtReducer
const jwt = 'mockToken123';
describe('When dispatching addJwt action', () => {
	const initialState = null;
	const expectedState = jwt;
	it('should set state to jwt string', () => {
		const state = jwtReducer(initialState, addJwt(jwt));
		expect(state).toEqual(expectedState);
		expect(state).not.toEqual(initialState);
	});
});

describe('When dispatching removeJwt action', () => {
	const initialState = jwt;
	const expectedState = null;
	it('should set state to null', () => {
		const state = jwtReducer(initialState, removeJwt(jwt));
		expect(state).toEqual(expectedState);
		expect(state).not.toEqual(initialState);
	});
});
