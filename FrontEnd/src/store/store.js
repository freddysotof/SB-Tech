import { configureStore } from "@reduxjs/toolkit";
import { cartSlice } from "./cart";
import { productSlice } from "./product";
import { authSlice } from "./auth";
import { uiSlice } from "./ui";
import { orderSlice } from "./order";
import { userSlice } from "./user";


export const store = configureStore({
    reducer: {
        auth: authSlice.reducer,
        cart: cartSlice.reducer,
        order:orderSlice.reducer,
        product:productSlice.reducer,
        user:userSlice.reducer,
        ui: uiSlice.reducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({
        serializableCheck: false
    })
        .concat()
})