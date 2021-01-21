// import actions
import { addBasketItem, removeBasketItem, updateQuantity, updateBasket } from '../basketAction';

// import factory function
import { BasketItem } from '../basketAction';

// import reducer
import basketReducer from '../basketReducer';

// Testing basketReducer
describe('When dispatching addBasketItem action', () => {
	const item = { id: 1, title: 'mockItem1'};
	const quantity = 1;
	const basketItem = BasketItem(item, quantity);
	const initialState = [];
	const expectedState = [basketItem];

	it("should add a basket item in basket", () => {
		const state = basketReducer(initialState, addBasketItem(basketItem));
		expect(state).toEqual(expectedState);
	});

	// To ensure it contains the correct properties 
	it("should NOT add an item in basket", () => {
		const state = basketReducer(initialState, addBasketItem(item));
		expect(state).not.toEqual(expectedState);
	});
})	
describe('When dispatching removeBasketItem action', () => {
	const basketItem1 = BasketItem({ id: 1, title: 'mockItem1'}, 1);
	const basketItem2 = BasketItem({ id: 2, title: 'mockItem2'}, 1);
	const initialState = [basketItem1, basketItem2];
	const expectedState = [basketItem2];

	it("should correctly remove the basket item from basket", () => {
		const state = basketReducer(initialState, removeBasketItem(basketItem1));
		expect(state).toEqual(expectedState);
		// checking immutable updates
		expect(state).not.toEqual(initialState);
	});
})	
describe('When dispatching updateQuantity action', () => {
	const basketItem1 = BasketItem({ id: 1, title: 'mockItem1'}, 1);
	const basketItem2 = BasketItem({ id: 2, title: 'mockItem2'}, 1);
	const updatedBasketItem2 = BasketItem({ id: 2, title: "mockItem2" }, 10);
	const initialState = [basketItem1, basketItem2];
	const expectedState = [basketItem1, updatedBasketItem2];

	it("should correctly update the basket item from basket", () => {
		const state = basketReducer(initialState, updateQuantity(basketItem2, 10));
		expect(state).toEqual(expectedState);
		// checking immutable updates
		expect(basketItem2).not.toEqual(updatedBasketItem2);
	});
})
describe('When dispatching updateBasket action', () => {
	const basketItem1 = BasketItem({ id: 1, title: 'mockItem1'}, 1);
	const basketItem2 = BasketItem({ id: 2, title: 'mockItem2'}, 10);
	const basketItem3 = BasketItem({ id: 3, title: 'mockItem3'}, 20);
	const initialState = [basketItem1, basketItem2];
	const basket = [basketItem1, basketItem3];
	const expectedState = basket;

	
	it("should correctly update the basket item from basket", () => {
		const state = basketReducer(initialState, updateBasket(basket));
		expect(state).toEqual(expectedState);
	});
})
