import configureMockStore from "redux-mock-store";
import thunk from "redux-thunk";
import moxios from "moxios";
import { basketActionTypes } from "../basketAction";

// import action creators
import {
	addBasketItemAsync,
	removeBasketItemAsync,
	updateQuantityAsync,
	addExistingBasketItemsToDbAsync
} from "../basketAction";

// import factory function
import { BasketItem } from '../basketAction';

import { BASE_URL } from '../basketAction';

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

	it("dispatch addBasketItem action when user is NOT signed in", (done) => {
		// no token when user is not signed in
		const store = mockStore();
		const jwt = null;
		const item = { id: 1, title: "mockItem1" };
		const quantity = 1;
		const basketItem = BasketItem(item, quantity);
		const expectedActions = [
			{
				type: basketActionTypes.ADD_ITEM,
				basketItem,
			}
		];

		store.dispatch(addBasketItemAsync(jwt, item, quantity)).then(() => {
			expect(store.getActions()).toEqual(expectedActions);
			done();
		})	
	});

	it("dispatch updateBasket action when user IS signed in", (done) => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 5);
		const itemToAdd = { id: 3, title: "mockItem3" };
		const quantity = 10;
		const basketItemToAdd = BasketItem(itemToAdd, quantity);
		const expectedBasketItems = [basketItem1, basketItem2, basketItemToAdd];
		const store = mockStore();
		const jwt = "mockJwt";
		
		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${itemToAdd.id}/${quantity}`,
				{
					status: 200,
				}
			);

			moxios.wait(async () => {
				moxios.stubRequest(
					BASE_URL,
					{
						status: 200,
						response: expectedBasketItems,
					}
				);

				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_BASKET,
						basketItems: expectedBasketItems
					},
				];
				store.dispatch(addBasketItemAsync(jwt, itemToAdd, quantity)).then(() => {
					expect(store.getActions()).toEqual(expectedActions);
					done();
				})
			});
		});
	});

	it("dispatch addItemError action when API requests failed", (done) => {
		const store = mockStore();
		const jwt = "mockJwt";
		const itemToAdd = { id: 3, title: "mockItem3" };
		const quantity = 10;
		
		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${itemToAdd.id}/${quantity}`,
				{
					status: 400,
					response: "Invalid data"
				}
			);
			const expectedActions = [
				{
					type: basketActionTypes.ADD_ITEM_ERROR,
					errorMessage: "Invalid data"
				}
			];
			store.dispatch(addBasketItemAsync(jwt, itemToAdd, quantity)).then(() => {
				expect(store.getActions()).toEqual(expectedActions);
				done();
			})
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

	it("dispatch addBasketItem action when user is NOT signed in", (done) => {
		// no token when user is not signed in
		const jwt = null;
		const store = mockStore();
		const item = { id: 1, title: "mockItem1" };
		const quantity = 1;
		const basketItem = BasketItem(item, quantity);
		const expectedActions = [
			{
				type: basketActionTypes.REMOVE_ITEM,
				basketItem,
			}
		];

		store.dispatch(removeBasketItemAsync(jwt, basketItem)).then(() => {
			expect(store.getActions()).toEqual(expectedActions);
			done();
		})
	});

	it("dispatch updateBasket action when user IS signed in", (done) => {
		const jwt = "mockJwt";
		const store = mockStore();
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 5);
		const basketItemToRemove = { id: 3 }; 
		const expectedBasketItems = [basketItem1, basketItem2];
		
		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${basketItemToRemove.id}`,
				{
					status: 200
				}
			);

			moxios.wait(() => {
				moxios.stubRequest(
					BASE_URL,
					{
						status: 200,
						response: expectedBasketItems
					}
				);
				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_BASKET,
						basketItems: expectedBasketItems
					},
				];

				store.dispatch(removeBasketItemAsync(jwt, basketItemToRemove)).then(() => {
					expect(store.getActions()).toEqual(expectedActions);
					done();
				})
			});
		});
	});

	it("dispatch removeItemError action when API requests failed", (done) => {
		const store = mockStore();
		const jwt = "mockJwt";
		const basketItemToRemove = { id: 3 }; 

		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${basketItemToRemove.id}`,
				{
					status: 400,
					response: 'Invalid data'
				}
			);

			moxios.wait(() => {
				moxios.stubRequest(
					BASE_URL,
					{
						status: 200
					}
				);

				const expectedActions = [
					{
						type: basketActionTypes.REMOVE_ITEM_ERROR,
						errorMessage: "Invalid data"
					}
				];
				store.dispatch(removeBasketItemAsync(jwt, basketItemToRemove)).then(() => {
					expect(store.getActions()).toEqual(expectedActions);
					done();
				})
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

	it("dispatch updateQuantity action when user is NOT signed in", (done) => {
		// no token when user is not signed in
		const jwt = null;
		const quantity = 1;
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, quantity);
		const newQuantity = 10;
		const store = mockStore();
		const expectedActions = [
			{
				type: basketActionTypes.UPDATE_QUANTITY,
				basketItem: basketItem1,
				quantity: newQuantity
			}
		];

		store.dispatch(updateQuantityAsync(jwt, basketItem1, newQuantity)).then(() => {
			expect(store.getActions()).toEqual(expectedActions);
			done();
		})
	});

	it("dispatch updateBasket action when user IS signed in", (done) => {
		const jwt = "mockJwt";
		const store = mockStore();
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 1);
		const id = 2;
		basketItem2.id = 2; // add Id property 
		const newQuantity = 10;
		const updatedBasketItem2 = BasketItem({ id: 2, title: "mockItem2" }, newQuantity);
		
		const expectedBasketItems = [basketItem1, updatedBasketItem2];

		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${id}/${newQuantity}`,
				{
					status: 200
				}
			);

			moxios.wait(() => {
				moxios.stubRequest(
					BASE_URL,
					{
						status: 200,
						response: expectedBasketItems
					}
				);

				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_BASKET,
						basketItems: expectedBasketItems
					},
				];
				store.dispatch(updateQuantityAsync(jwt, basketItem2, newQuantity)).then(() => {
					expect(store.getActions()).toEqual(expectedActions)
					done()
				})
			});
			
		});
	});

	it("dispatch updateQuantityError action when API requests failed", (done) => {
		const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
		const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 1);
		const id = 2;
		basketItem2.id = 2; // add Id property 
		const newQuantity = 10;
		const updatedBasketItem2 = BasketItem({ id: 2, title: "mockItem2" }, newQuantity);
		
		const initialState = [basketItem1, basketItem2];
		const expectedBasketItems = [basketItem1, updatedBasketItem2];

		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${id}/${newQuantity}`,
					{
						status: 400,
						response: "Invalid request"
					}
			);

			moxios.wait(() => {
				moxios.stubRequest(
					BASE_URL,
					{
						status: 200,
						response: expectedBasketItems
					}
				);

				const jwt = "mockJwt";
				const store = mockStore({ basket: initialState });
				const expectedActions = [
					{
						type: basketActionTypes.UPDATE_QUANTITY_ERROR,
						errorMessage: "Invalid request"
					}
				];
				store.dispatch(updateQuantityAsync(jwt, basketItem2, newQuantity)).then(() => {
					expect(store.getActions()).toEqual(expectedActions);
					done();
				})
			});
		});
	});
});


// addExistingBasketItemsToDbAsync
describe("Testing addExistingBasketItemsToDoAsync action", () => {
	beforeEach(function () {
		moxios.install();
	});
	afterEach(function () {
		moxios.uninstall();
	});
	const jwt = "mockJwt";
	const basketItem1 = BasketItem({ id: 1, title: "mockItem1" }, 1);
	const basketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 1);
	const basket = [basketItem1, basketItem2];
	const expectedBasketItems = [basketItem1, basketItem2];
	
	it("dispatch updateBasket action when POST requests are successful", (done) => {
		const store = mockStore();

		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${basketItem1.itemId}/${basketItem1.quantity}`,
				{
					status: 200
				}
				);

				moxios.wait(() => {
					moxios.stubRequest(
						`${BASE_URL}/${basketItem2.itemId}/${basketItem2.quantity}`,
						{
							status: 200
						}
						);
						
						moxios.wait(() => {
							moxios.stubRequest(
								BASE_URL,
								{
									status: 200,
									response: expectedBasketItems
								}
								);
								
								const expectedActions = [
									{
										type: basketActionTypes.UPDATE_BASKET,
										basketItems: expectedBasketItems
									},
								];
								store.dispatch(addExistingBasketItemsToDbAsync(jwt, basket)).then(() => {
									expect(store.getActions()).toEqual(expectedActions)
									done()
								})
							});
			});
		});
	});

	it("dispatch addItemError action when the first POST request is unsuccessful", (done) => {
		const store = mockStore();
		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${basketItem1.itemId}/${basketItem1.quantity}`,
				{
					status: 400,
					response: 'Invalid request'
				}
			);

			moxios.wait(() => {
				moxios.stubRequest(
					`${BASE_URL}/${basketItem2.itemId}/${basketItem2.quantity}`,
					{
						status: 200,
					}
				);

				moxios.wait(() => {
					moxios.stubRequest(
						BASE_URL,
						{
							status: 200,
							response: expectedBasketItems
						}
					);

					const expectedActions = [
						{
							type: basketActionTypes.ADD_ITEM_ERROR,
							errorMessage: 'Invalid request'
						},
					];
					store.dispatch(addExistingBasketItemsToDbAsync(jwt, basket)).then(() => {
						expect(store.getActions()).toEqual(expectedActions)
						done()
					})
				});
			});
		});
	});

	it("dispatch addItemError action when the first POST request is successful but the subsequent POST request is unsuccessful", (done) => {
		const store = mockStore();
		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${basketItem1.itemId}/${basketItem1.quantity}`,
				{
					status: 200,
				}
			);

			moxios.wait(() => {
				moxios.stubRequest(
					`${BASE_URL}/${basketItem2.itemId}/${basketItem2.quantity}`,
					{
						status: 400,
						response: 'Invalid request'
					}
				);

				moxios.wait(() => {
					moxios.stubRequest(
						BASE_URL,
						{
							status: 200,
							response: expectedBasketItems
						}
					);

					const expectedActions = [
						{
							type: basketActionTypes.ADD_ITEM_ERROR,
							errorMessage: 'Invalid request'
						},
					];
					store.dispatch(addExistingBasketItemsToDbAsync(jwt, basket)).then(() => {
						expect(store.getActions()).toEqual(expectedActions)
						done()
					})
				});
			});
		});
	});

	it("dispatch updateBasketError action when the GET request is unsuccessful", (done) => {
		const store = mockStore();
		moxios.wait(() => {
			moxios.stubRequest(
				`${BASE_URL}/${basketItem1.itemId}/${basketItem1.quantity}`,
				{
					status: 200,
				}
			);

			moxios.wait(() => {
				moxios.stubRequest(
					`${BASE_URL}/${basketItem2.itemId}/${basketItem2.quantity}`,
					{
						status: 200
					}
				);

				moxios.wait(() => {
					moxios.stubRequest(
						BASE_URL,
						{
							status: 400,
							response: 'Invalid request'
						}
					);

					const expectedActions = [
						{
							type: basketActionTypes.UPDATE_BASKET_ERROR,
							errorMessage: 'Invalid request'
						},
					];
					store.dispatch(addExistingBasketItemsToDbAsync(jwt, basket)).then(() => {
						expect(store.getActions()).toEqual(expectedActions)
						done()
					})
				});
			});
		});
	});

});

