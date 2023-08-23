import { createSlice } from '@reduxjs/toolkit';

export const userSlice = createSlice({
    name: 'user',
    initialState: {
        users: [],
        page: 0,
        limit: 25,
        hasNextPage: false,
        hasPreviousPage: false,
        activeUser: null,
        isLoading: false,
        errorMessage: null,
    },
    reducers: {
        
           onLoadUsers: (state, { payload }) => {
            // state.isLoading = false;
            state.users = payload;
            // state.page = payload.page;
            // state.hasNextPage = payload.hasNextPage;
            // state.hasPreviousPage = payload.hasPreviousPage;
        },  
        onClearErrorMessage: (state) => {
            state.errorMessage = undefined;
        },
        onSetErrorMessage: (state, { payload }) => {
            state.errorMessage = payload;
            state.isLoading = false;
        },
        onClearUsers: (state) => {
            state.users = [];
            state.activeUser = null;
            state.hasNextPage = false;
            state.hasPreviousPage = false;
            state.page =0;

        }
    }
});
// Action creators are generated for each case reducer function
export const {
    onLoadUsers,
    onClearErrorMessage,
    onSetErrorMessage,
    onClearUserStore
} = userSlice.actions;
//! https://react-redux.js.org/tutorials/quick-start    