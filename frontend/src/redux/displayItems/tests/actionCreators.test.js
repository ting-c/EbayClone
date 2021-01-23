import configureMockStore from "redux-mock-store";
import thunk from "redux-thunk";
import moxios from "moxios";

// Action types
import { displayItemsActionTypes } from '../displayItemsAction';

// Action creator
import { fetchItemsAsync } from '../displayItemsAction';

import { BASE_URL } from '../displayItemsAction';

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

describe("Testing fetchItemsAsync action", () => {
	beforeEach(function () {
		moxios.install();
	});
	afterEach(function () {
		moxios.uninstall();
	});

	// Without searchTerm
	describe("When searchTerm argument is null or empty", () => {

		describe('and received a success API response', () => {
			it('should dispatch updateDisplayItems action', (done) => {
				const store = mockStore();
				const expectedDisplayItems = [
					{ id: 1, title: 'Item1', price: 10 },
					{ id: 2, title: 'Item2', price: 20 }
				]
				moxios.wait(() => {
					moxios.stubRequest(BASE_URL, {
						status: 200,
						response: expectedDisplayItems
					})
					
					const expectedActions = [
						{
							type: displayItemsActionTypes.UPDATE,
							displayItems: expectedDisplayItems
						}
					]
					store.dispatch(fetchItemsAsync()).then(() => {
						expect(store.getActions()).toEqual(expectedActions);
						done();
					})
				})
			})
		})	
		describe('and received an unsuccessful API response', () => {
			it('should dispatch updateError action', (done) => {
				const store = mockStore();
				const expectedErrorMessage = 'Invalid request';
	
				moxios.wait(() => {
					moxios.stubRequest(BASE_URL, {
						status: 400,
						response: expectedErrorMessage
					})
					
					const expectedActions = [
						{
							type: displayItemsActionTypes.UPDATE_ERROR,
							errorMessage: expectedErrorMessage
						}
					]
					store.dispatch(fetchItemsAsync()).then(() => {
						expect(store.getActions()).toEqual(expectedActions);
						done();
					})
				})
			})
		})
	})

	// With searchTerm
	describe("When a non-empty searchTerm is provided as an argument", () => {
		const searchTerm = 'Item';
		const url = `${BASE_URL}/search/${searchTerm}`

		describe("and received a successful API response", () => {
			it("should dispatch updateDisplayItems action", (done) => {
				const store = mockStore();
				const expectedDisplayItems = [
					{ id: 1, title: "Item1", price: 10 },
					{ id: 2, title: "Item2", price: 20 },
				];

				moxios.wait(() => {
					moxios.stubRequest(url, {
						status: 200,
						response: expectedDisplayItems
					})
	
					const expectedActions = [
						{
							type: displayItemsActionTypes.UPDATE,
							displayItems: expectedDisplayItems
						}
					]
	
					store.dispatch(fetchItemsAsync(searchTerm)).then(() => {
						expect(store.getActions()).toEqual(expectedActions);
						done();
					})
				})
			})
		});
		describe("and the response from API is falsy", () => {
			it("should dispatch updateError action", (done) => {
				const store = mockStore();
				const expectedErrorMessage = 'Failed to fetch data';
	
				moxios.wait(() => {
					moxios.stubRequest(url, {
						status: 200,
						response: undefined
					})
	
					const expectedActions = [
						{
							type: displayItemsActionTypes.UPDATE_ERROR,
							errorMessage: expectedErrorMessage
						}
					];
	
					store.dispatch(fetchItemsAsync(searchTerm)).then(() => {
						expect(store.getActions()).toEqual(expectedActions);
						done();
					})
				})
			})
		});	
		describe("and received an unsuccessful response from API", () => {
			it("should dispatch updateError action", (done) => {
				const store = mockStore();
				const expectedErrorMessage = "Invalid request";
	
				moxios.wait(() => {
					moxios.stubRequest(url, {
						status: 400,
						response: expectedErrorMessage
					});
	
					const expectedActions = [
						{
							type: displayItemsActionTypes.UPDATE_ERROR,
							errorMessage: expectedErrorMessage
						}
					];
	
					store.dispatch(fetchItemsAsync(searchTerm)).then(() => {
						expect(store.getActions()).toEqual(expectedActions);
						done();
					});
				})
			})
		})
	})
})