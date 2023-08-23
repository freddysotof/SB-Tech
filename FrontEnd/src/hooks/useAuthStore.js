import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { onCheckingCredentials, onLogout, onLogin, onSetDisplayName } from "../store/auth";
import { onClearItems } from '../store/cart';
import { sbApi } from '../api';
import { getEnvVariables } from "../helpers";
import { logoutFirebase, signInWithGoogle } from '../firebase'

export const useAuthStore = () => {
    const authState = useSelector(state => state.auth);
    const
        {
            displayName,
            status,
            errorMessage,
            id,
            email,
            scheme,
            isAdmin
        } = authState
    const dispatch = useDispatch();


    const startLoginWithGoogle = async () => {
        dispatch(onCheckingCredentials())
        const resp = await signInWithGoogle();
        if (!resp.ok) return dispatch(onLogout(resp))
        localStorage.setItem('jwt-scheme', 'google')
        localStorage.setItem('jwt', resp.accessToken)
        dispatch(onLogin({ ...resp, scheme: 'google' }))
    }
    const startLogin = async (credentials) => {
        try {
            dispatch(onCheckingCredentials())
            let resp = {};
            resp = await sbApi.post('/auth/login', { email: credentials.email, password: credentials.password });
            if (resp) {
                if (!resp.isSuccessStatusCode) {
                    dispatch(onLogout({ errorMessage: resp.errorMessage }));
                } else {
                    const user = resp.data.user;
                    dispatch(onLogin({ scheme: 'local', displayName: user.name, email: user.email }))
                    localStorage.setItem('jwt', resp.data.credentials.accessToken.token);
                    localStorage.setItem('jwt-init-date', new Date().getTime());
                    localStorage.setItem('refresh-token', resp.data.credentials.refreshToken.token);
                    localStorage.setItem('jwt-scheme', 'local')
                }
            } else
                dispatch(onLogout({ errorMessage: 'Error de autenticacion' }));
        } catch (error) {
            dispatch(onLogout({ errorMessage: error.message }));
        }
    }


    const startLogout = async () => {
        try {
            localStorage.clear();
            await logoutFirebase();
            dispatch(onLogout());
            dispatch(onClearItems());
        } catch (error) {

        }
    }



    const startSetDisplayName = (displayName) => {
        dispatch(onSetDisplayName(displayName));
    }

    const checkAuth = async () => {
        let logged;
        let authUser;
        if (status === 'authenticated')
            return;
        const scheme = localStorage.getItem('jwt-scheme');

        if (scheme !== 'google') {
            const accessToken = localStorage.getItem('jwt');
            const refreshToken = localStorage.getItem('refresh-token');
            if (!accessToken || !refreshToken)
                return dispatch(onLogout({}));

            try {

                const { credentials, user } = await sbApi.post("/auth/token/renew", {
                    accessToken,
                    refreshToken
                });
                console.log(credentials, user)
                localStorage.setItem('jwt', credentials.accessToken.token);
                localStorage.setItem('jwt-init-date', new Date().getTime());
                localStorage.setItem('refresh-token', credentials.refreshToken.token);
                dispatch(onLogin({ scheme: 'local', displayName: user.name, email: user.email }))
            } catch (err) {
                console.log(err);
                return dispatch(onLogout({}));
            }
        }


    }

    return {
        //* Propiedades
        displayName,
        status,
        errorMessage,
        id,
        email,
        isAdmin,
        //* Metodos
        startLogin,
        startLoginWithGoogle,
        startLogout,
        startSetDisplayName,
        checkAuth
    }
}