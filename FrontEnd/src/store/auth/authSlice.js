import { createSlice } from '@reduxjs/toolkit';

export const authSlice = createSlice({
    name: 'auth',
    initialState: {
        status: 'checking', // 'checking', 'not-authenticated' , 'authenticated'
        id: null,
        email: null,
        displayName: null,
        photoUrl: null,
        errorMessage: null,
        isAdmin: false,
        scheme: ''
    },
    reducers: {
        onLogin: (state, { payload }) => {
            state.status = 'authenticated'; // 'checking', 'not-authenticated' , 'authenticated'
            state.id = payload.uid;
            state.email = payload.email;
            state.displayName = payload.displayName;
            state.photoUrl = payload.photoURL;
            state.scheme = payload.scheme;
            state.errorMessage = null;
            if (state.scheme === 'google')
                state.isAdmin = false;
            else
                state.isAdmin = true
        },
        onLogout: (state, { payload }) => {
            state.status = 'not-authenticated'; // 'checking', 'not-authenticated' , 'authenticated'
            state.id = null;
            state.email = null;
            state.displayName = null;
            state.photoUrl = null;
            state.scheme = null;
            state.isAdmin = false;
            state.errorMessage = payload?.errorMessage ?? null;
        },
        onCheckingCredentials: (state) => {
            state.errorMessage = null;
            state.status = "checking";
        },
        onSetDisplayName: (state, { payload }) => {
            state.displayName = payload;
        },

    }
});
// Action creators are generated for each case reducer function
export const {
    onLogin,
    onLogout,
    onCheckingCredentials,
    onSetDisplayName
} = authSlice.actions;
//! https://react-redux.js.org/tutorials/quick-start