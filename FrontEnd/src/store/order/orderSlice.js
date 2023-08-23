import { createSlice } from '@reduxjs/toolkit';

export const orderSlice = createSlice({
    name: 'order',
    initialState: {
        shippingMethods: [],
        paymentMethods: [],
    
        messageSaved: '',
        activeOrder: null
    },
    reducers: {
        onLoadShippingMethods: (state, { payload }) => {
            state.shippingMethods = payload;
        },
        onLoadPaymentMethods: (state, { payload }) => {
            state.paymentMethods = payload;
        },
     
        onProcessOrder: (state, { payload }) => {
            state.activeOrder = { ...payload.order, ...state.activeOrder };
            state.messageSaved = payload?.messageSaved;
        },
        onSavedOrder: (state) => {
            state.messageSaved = '';
            state.activeOrder = {};
        },
        onSetActiveOrder: (state, { payload }) => {
            state.activeOrder = payload;
            state.messageSaved = '';
        }
    }
});
// Action creators are generated for each case reducer function
export const {
    onLoadShippingMethods,
    onLoadPaymentMethods,
    onProcessOrder,
    onSavedOrder,
    onSetActiveOrder,
    onSetNewOrder

} = orderSlice.actions;
//! https://react-redux.js.org/tutorials/quick-start