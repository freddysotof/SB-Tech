import { createSlice } from '@reduxjs/toolkit';
const initialValue = {
    quantity: 1,
    unitPrice: 915.25,
    code: "0123",
    description: "El expertise y el know-how de la familia Rothschild están detrás de este vino que expresa la suavidad, los sabores a fruta y la personalidad de una de las dos uvas principales de Bordeaux. ",
}
export const productSlice = createSlice({
    name: 'product',
    initialState: {
        page: null,
        limit: null,
        hasNextPage: false,
        hasPreviousPage: false,
        products: [],
        activeProduct: initialValue,
        isLoading: false,
        errorMessage: null,
    },
    reducers: {
        onSetLoading: (state) => {
            state.isLoading = true;
        },
        onLoadProducts: (state, { payload }) => {
            state.isLoading = false;
            state.products = payload.products;
            state.page = payload.page;
            state.hasNextPage = payload.hasNextPage;
            state.hasPreviousPage = payload.hasPreviousPage;
        },
        onAddProduct: (state, { payload }) => {
            state.isLoading = false;
            state.products.push(payload);
        },
        onUpdateProduct: (state, { payload }) => {
            state.isLoading = false;
            state.products = [...state.products].map((product => {
                if (product.id === payload.id)
                    return payload;
                return product;
            }))
        },
        onDeleteProduct: (state, { payload }) => {
            state.isLoading = false;
            state.products = [...state.products].filter((product => product.id !== payload));
        },
        onSetActiveProduct: (state, { payload }) => {
            state.activeProduct = payload
            state.isLoading = false;
        },
        onClearErrorMessage: (state) => {
            state.errorMessage = undefined;
        },
        onSetErrorMessage: (state, { payload }) => {
            state.errorMessage = payload;
            state.isLoading = false;
        },
        onClearProducts: (state) => {
            state.products = [];
            state.activeProduct = null;
            state.hasNextPage = false;
            state.hasPreviousPage = false;

        }
    }
});
// Action creators are generated for each case reducer function
export const {
    onSetLoading,
    onLoadProducts,
    onSetActiveProduct,
    onAddProduct,
    onUpdateProduct,
    onDeleteProduct,
    onClearProducts,
    onSetErrorMessage,
    onClearErrorMessage
} = productSlice.actions;
//! https://react-redux.js.org/tutorials/quick-start