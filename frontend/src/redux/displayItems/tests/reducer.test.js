// import actions
import {
	updateDisplayItems
} from "../displayItemsAction";

// import reducer
import displayItemsReducer from "../displayItemsReducer";

describe("Testing displayItemReducer", () => {
	describe("When dispatching updateDisplayItems", () => {
		const item1 = { id: 1, title: 'Item1', price: 10 }
		const item2 = { id: 2, title: 'Item2', price: 20 }
		const item3 = { id: 3, title: 'Item3', price: 30 }

		it("should update with new items", () => {
			const initialState = [item1, item2];
			const expectedState = [item2, item3];
			const state = displayItemsReducer(initialState, updateDisplayItems(expectedState));
			expect(state).toEqual(expectedState);
			expect(state).not.toEqual(initialState);
		})
		it("should update with an empty erray if passed into action as an argument", () => {
			const initialState = [item1, item2];
			const expectedState = [];
			const state = displayItemsReducer(initialState, updateDisplayItems(expectedState));
			expect(state).toEqual(expectedState);
			expect(state).not.toEqual(initialState);
		})
	})
})