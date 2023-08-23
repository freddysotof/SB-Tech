import { onAuthStateChanged } from "firebase/auth";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { onLogin, onLogout } from "../store/auth";
import { firebaseAuth } from "../firebase";

export const useCheckAuthGoogle = () => {
    const { status, scheme } = useSelector(state => state.auth);
    const dispatch = useDispatch();
    useEffect(() => {

        onAuthStateChanged(firebaseAuth, async (user) => {
            const scheme = localStorage.getItem('jwt-scheme');
            if (scheme === "google") {
                if (!user) return dispatch(onLogout())
                localStorage.setItem('jwt', user.accessToken);
                const { uid, email, displayName, photoURL } = user;
                dispatch(onLogin({ uid, email, displayName, photoURL, scheme }))
            }

        })



    }, [])

    return {
        status
    }
}


