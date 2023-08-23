import { createSlice } from '@reduxjs/toolkit';

export const cartSlice = createSlice({
    name: 'cart',
    initialState: {
        items: [],
        activeItem: null,
        messageSaved: '',
        errorMessage: undefined,
        summary: {
            total: { label: 'Subtotal:', amount: 0.00, isMoney: true, },
            tax: { label: 'Impuesto:', amount: 0.00, isMoney: true, },
            net: { label: 'Total:', amount: 0.00, isMoney: true, },
        }
    },
    reducers: {
        onAddItem: (state, { payload }) => {
            const { newItem } = payload;

            newItem.taxPercentage = 18;
            newItem.totalAmount = (newItem.unitPrice * newItem.quantity);


            newItem.taxAmount = ((newItem.totalAmount) * (newItem.taxPercentage / 100));
            newItem.netAmount = (newItem.totalAmount + newItem.taxAmount)


            state.items.push(newItem);
            //* Get the total values
            state.summary.total.amount = [...state.items].reduce((a, v) => a = a + v.totalAmount, 0.00);
            state.summary.tax.amount = [...state.items].reduce((a, v) => a = a + v.taxAmount, 0.00);
            state.summary.net.amount = [...state.items].reduce((a, v) => a = a + v.netAmount, 0.00);
        },
        onUpdateItem: (state, { payload }) => {
            const { updatedItem } = payload;
            state.isSaving = false;

            const newItems = [...state.items].map((item) => {
                if (item.id == updatedItem.id) {

                    updatedItem.totalAmount = (updatedItem.unitPrice * updatedItem.quantity);

                    updatedItem.taxAmount = ((updatedItem.totalAmount) * (updatedItem.taxPercentage / 100));
                    updatedItem.netAmount = (updatedItem.totalAmount + updatedItem.taxAmount)
                    return updatedItem;
                }
                return item;
            });
            state.items = newItems;
            state.summary.total.amount = [...newItems].reduce((a, v) => a = a + v.totalAmount, 0.00);
            state.summary.tax.amount = [...newItems].reduce((a, v) => a = a + v.taxAmount, 0.00);
            state.summary.net.amount = [...newItems].reduce((a, v) => a = a + v.netAmount, 0.00);
        },
        onSetActiveItem: (state, { payload }) => {
            state.activeItem = payload;
            state.messageSaved = '';
        },

        onDeleteItem: (state, { payload }) => {
            state.items = state.items
                .filter(item => item.id !== payload);
            state.summary.total.amount = [...state.items].reduce((a, v) => a = a + v.totalAmount, 0.00);
            state.summary.tax.amount = [...state.items].reduce((a, v) => a = a + v.taxAmount, 0.00);
            state.summary.net.amount = [...state.items].reduce((a, v) => a = a + v.netAmount, 0.00);

        },
        onSetErrorMessage: (state, { payload }) => {
            state.errorMessage = payload;
        },
        onClearItems: (state) => {
            state.items = [];
            state.activeItem = null;
            state.summary.total.amount = 0.00;
            state.summary.tax.amount = 0.00;
            state.summary.net.amount = 0.00;

        },
        onClearErrorMessage: (state) => {
            state.errorMessage = undefined;
        },
        onRecalculateCart: (state, { payload }) => {
            const { averageTicket, discountPercentage } = payload;
            state.items = [...state.items].map(item => {
                item.totalAmount = (item.unitPrice * item.quantity);

             
                item.taxAmount = ((item.totalAmount ) * (item.taxPercentage / 100));
                item.netAmount = (item.totalAmount  + item.taxAmount);

                return item;
            });

            state.summary.total.amount = [...state.items].reduce((a, v) => a = a + v.totalAmount, 0.00);
            state.summary.tax.amount = [...state.items].reduce((a, v) => a = a + v.taxAmount, 0.00);
            state.summary.net.amount = [...state.items].reduce((a, v) => a = a + v.netAmount, 0.00);

          
        }
    }
});
// Action creators are generated for each case reducer function
export const {
    onAddItem,
    onUpdateItem,
    onDeleteItem,
    onSetActiveItem,
    onSetErrorMessage,
    onClearErrorMessage,
    onSetActivePriceLevel,
    onSetActiveLocationCode,
    onSetAverageTicketProgress,
    onClearItems,
    onRecalculateCart

} = cartSlice.actions;
//! https://react-redux.js.org/tutorials/quick-start