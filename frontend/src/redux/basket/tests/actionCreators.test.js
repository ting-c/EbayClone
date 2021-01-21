import configureMockStore from "redux-mock-store";
import thunk from "redux-thunk";
import moxios from "moxios";
import { basketActionTypes } from "../basketAction";

// import action creators
import {
	addBasketItemAsync,
	removeBasketItemAsync,
	updateQuantityAsync,
} from "../basketAction";

// import factory function
import { BasketItem } from '../basketAction';

// Testing action creators
const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

// addBasketItemAsync
describe("Testing addBasketItemAsync action", () => {
	beforeEach(function () {
		moxios.install();
	});
	afterEach(function () {
		moxios.uninstall();
	});

	it("dispatch addBasketItem action when user is NOT signed in", async () => {
		// no token when user is not signed in
		const jwt = null;
		const item = { id: 1, title: "mockItem1" };
		const quantity = 1;
		const basketItem = BasketItem(item, quantity);

		const initialState = [];
		const store = mockStore({ basket: initialState });
		const expectedActions = [
			{
				type: basketActionTypes.ADD_ITEM,
				basketItem,
			}
		];

		await store.dispatch(addBasketItemAsync(jwt, item, quantity))	
		expect(store.getActions()).toEqual(expectedActions);
	});

	it("dispatch updateBasket action when user IS signed in", async () => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 5);
		const itemToAdd = { id: 3, title: "mockItem3" };
		const quantity = 10;
		const basketItemToAdd = BasketItem(itemToAdd, quantity);
		const expectedBasketItems = [basketItem1, basketItem2, basketItemToAdd];
		
		moxios.wait(() => {
			const postRequest = moxios.requests.mostRecent();
			postRequest.respondWith({
				status: 200
			});

			moxios.wait(async () => {
				const getRequest = moxios.requests.mostRecent();
				getRequest.respondWith({
					status: 200,
					response: expectedBasketItems,
				});

				const initialState = [basketItem1, basketItem2];
				const jwt = "mockJwt";
				const store = mockStore({ basket: initialState });
				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_BASKET,
						basketItems: expectedBasketItems
					},
				];
				await store.dispatch(addBasketItemAsync(jwt, itemToAdd, quantity));
				expect(store.getActions()).toEqual(expectedActions);
			});
		});
	});

	it("dispatch updateBasket action when API requests failed", async () => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 5);
		const itemToAdd = { id: 3, title: "mockItem3" };
		const quantity = 10;
		
		moxios.wait(async () => {
			const postRequest = moxios.requests.mostRecent();
			postRequest.reject({
				status: 400,
				response: { data: "Invalid data" },
			});

			const initialState = [basketItem1, basketItem2];
			const jwt = "mockJwt";
			const store = mockStore({ basket: initialState });
			const expectedActions = [
				{
					type: basketActionTypes.ADD_ITEM_ERROR,
					errorMessage: "Invalid data"
				}
			];
			await store.dispatch(addBasketItemAsync(jwt, itemToAdd, quantity));
			expect(store.getActions()).toEqual(expectedActions);
		});

	});
});

// removeBasketItemAsync
describe("Testing removeBasketItemAsync action", () => {
	beforeEach(function () {
		moxios.install();
	});
	afterEach(function () {
		moxios.uninstall();
	});

	it("dispatch addBasketItem action when user is NOT signed in", async () => {
		// no token when user is not signed in
		const jwt = null;
		const item = { id: 1, title: "mockItem1" };
		const quantity = 1;
		const basketItem = BasketItem(item, quantity);

		const initialState = [basketItem];
		const store = mockStore({ basket: initialState });
		const expectedActions = [
			{
				type: basketActionTypes.REMOVE_ITEM,
				basketItem,
			}
		];

		await store.dispatch(removeBasketItemAsync(jwt, basketItem))	
		expect(store.getActions()).toEqual(expectedActions);
	});

	it("dispatch updateBasket action when user IS signed in", async () => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 5);
		const basketItemToRemove = BasketItem({ id: 3, title: "mockItem2" }, 10);
		const expectedBasketItems = [basketItem1, basketItem2];
		
		moxios.wait(() => {
			const postRequest = moxios.requests.mostRecent();
			postRequest.respondWith({
				status: 200
			});

			moxios.wait(async () => {
				const getRequest = moxios.requests.mostRecent();
				getRequest.respondWith({
					status: 200,
					response: expectedBasketItems,
				});

				const initialState = [basketItem1, basketItem2];
				const jwt = "mockJwt";
				const store = mockStore({ basket: initialState });
				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_BASKET,
						basketItems: expectedBasketItems
					},
				];
				await store.dispatch(removeBasketItemAsync(jwt, basketItemToRemove));
				expect(store.getActions()).toEqual(expectedActions);
			});
		});
	});

	it("dispatch removeItemError action when API requests failed", async () => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 5);
		const basketItemToRemove = BasketItem({ id: 3, title: "mockItem2" }, 10);

		moxios.wait(() => {
			const postRequest = moxios.requests.mostRecent();
			postRequest.reject({
				status: 400,
				response: { data: "Invalid data" },
			});

			moxios.wait(async () => {
				const getRequest = moxios.requests.mostRecent();
				getRequest.respondWith({
					status: 200
				});

				const initialState = [basketItem1, basketItem2];
				const jwt = "mockJwt";
				const store = mockStore({ basket: initialState });
				const expectedActions = [
					{
						type: basketActionTypes.REMOVE_ITEM_ERROR,
						errorMessage: "Invalid data"
					}
				];
				await store.dispatch(removeBasketItemAsync(jwt, basketItemToRemove));
				expect(store.getActions()).toEqual(expectedActions);
			});
		});	
	});
});

// updateQuantityAsync
describe("Testing updateQuantitysync action", () => {
	beforeEach(function () {
		moxios.install();
	});
	afterEach(function () {
		moxios.uninstall();
	});

	it("dispatch updateQuantity action when user is NOT signed in", async () => {
		// no token when user is not signed in
		const jwt = null;
		const quantity = 1;
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, quantity);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, quantity);

		const initialState = [basketItem1, basketItem2];
		const newQuantity = 10;
		const store = mockStore({ basket: initialState });
		const expectedActions = [
			{
				type: basketActionTypes.UPDATE_QUANTITY,
				basketItem: basketItem1,
				quantity: newQuantity
			}
		];

		await store.dispatch(updateQuantityAsync(jwt, basketItem1, newQuantity))
		expect(store.getActions()).toEqual(expectedActions);
	});

	it("dispatch updateBasket action when user IS signed in", async () => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 1);

		const newQuantity = 10;
		const updatedBasketItem2 = BasketItem({ id: 2, title: "mockItem2" }, newQuantity);
		
		const initialState = [basketItem1, basketItem2];
		const expectedBasketItems = [basketItem1, updatedBasketItem2];

		moxios.wait(() => {
			const putRequest = moxios.requests.mostRecent();
			putRequest.respondWith({
				status: 200
			});

			moxios.wait(async () => {
				const getRequest = moxios.requests.mostRecent();
				getRequest.respondWith({
					status: 200,
					response: expectedBasketItems,
				});

				const jwt = "mockJwt";
				const store = mockStore({ basket: initialState });
				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_BASKET,
						basketItems: expectedBasketItems
					},
				];
				await store.dispatch(updateQuantityAsync(jwt, basketItem2, newQuantity));
		
				expect(store.getActions()).toEqual(expectedActions);
			});
			
		});
	});

	it("dispatch updateQuantityError action when API requests failed", async () => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 1);

		const newQuantity = 10;
		const updatedBasketItem2 = BasketItem({ id: 2, title: "mockItem2" }, newQuantity);
		
		const initialState = [basketItem1, basketItem2];
		const expectedBasketItems = [basketItem1, updatedBasketItem2];

		moxios.wait(() => {
			const putRequest = moxios.requests.mostRecent();
			putRequest.reject({
				status: 400,
				response: { data: "Invalid request" }
			});

			moxios.wait(async () => {
				const getRequest = moxios.requests.mostRecent();
				getRequest.respondWith({
					status: 200,
					response: expectedBasketItems,
				});

				const jwt = "mockJwt";
				const store = mockStore({ basket: initialState });
				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_QUANTITY_ERROR,
						errorMessage: "Invalid request"
					}
				];
				await store.dispatch(updateQuantityAsync(jwt, basketItem2, newQuantity));
		
				expect(store.getActions()).toEqual(expectedActions);
			});
		});
	});
});

