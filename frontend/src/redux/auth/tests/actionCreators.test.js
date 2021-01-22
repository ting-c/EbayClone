import configureMockStore from "redux-mock-store";
import thunk from "redux-thunk";
import moxios from "moxios";

// Action types
import { authActionTypes } from "../authAction";

// Action creators
import { signInAsync, signUpAsync, signOut } from '../authAction'

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

// signInAsync
describe("Testing signInAsync action", () => {
	beforeEach(function () {
		moxios.install();
	});
	afterEach(function () {
		moxios.uninstall();
	});
	
	const store = mockStore();
	it("dispatch addUser and addJwt actions when user sign in successfully", (done) => {
		const data = { firstName: 'FirstName', password: 'password'};
		const jwt = "mockJwt123";
		const user = { 
			id: 1,
			name: 'Mock User',
			sellingItems: { item: { id: 1, title: 'Item 1'} }
		};

		moxios.wait(() => {
			moxios.stubRequest("https://localhost:5001/api/auth/signin", {
				status: 200,
				response: { jwtString: jwt, user: user },
			});
			const expectedActions = [
				{
					type: authActionTypes.ADD_USER,
					user: user
				},
				{
					type: authActionTypes.ADD_JWT,
					token: jwt
				}
			];

			store.dispatch(signInAsync(data)).then(() => {
				expect(store.getActions()).toEqual(expectedActions);
				done()
			})
		});	
	})
})

// signUpAsync
describe("Testing signUpAsync action", () => {
	beforeEach(function () {
		moxios.install();
	});
	afterEach(function () {
		moxios.uninstall();
	});
	
	const store = mockStore();
	it("dispatch addUser and addJwt actions when user sign up successfully", (done) => {
		const data = { 
			firstName: 'First', 
			lastName: 'Last',
			email: 'mock123@gmail.com',
			password: 'Password'
		};
		const jwt = "mockJwt123";
		const user = { 
			id: 1,
			name: 'Mock User',
			sellingItems: { item: { id: 1, title: 'Item 1'} }
		};

		moxios.wait(() => {
			moxios.stubRequest("https://localhost:5001/api/auth/signup", {
				status: 200,
				response: { jwtString: jwt, user: user },
			});

			moxios.wait(() => {
				moxios.stubRequest("https://localhost:5001/api/auth/signin", {
					status: 200,
					response: { jwtString: jwt, user: user },
				});
				const expectedActions = [
					{
						type: authActionTypes.ADD_USER,
						user: user
					},
					{
						type: authActionTypes.ADD_JWT,
						token: jwt
					}
				];
	
				store.dispatch(signUpAsync(data)).then(() => {
					expect(store.getActions()).toEqual(expectedActions);
					done()
				})
			});	
		})
	})
})

// signUpAsync
describe("Testing signOut action", () => {
	it("dispatch removeUser and removeJwt actions when user sign out", () => {
		const store = mockStore();

		const expectedActions = [
			{
				type: authActionTypes.REMOVE_USER
			},
			{
				type: authActionTypes.REMOVE_JWT
			}
		];

		store.dispatch(signOut())
		expect(store.getActions()).toEqual(expectedActions);
	})
})