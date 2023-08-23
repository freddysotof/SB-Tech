import { createUserWithEmailAndPassword, GoogleAuthProvider, signInWithEmailAndPassword, signInWithPopup, updateProfile, signInWithPhoneNumber, RecaptchaVerifier, PhoneAuthProvider, signInWithCredential } from "firebase/auth";
import { firebaseAuth, firebaseApp } from "../config";

const googleProvider = new GoogleAuthProvider();

export const resetRecaptcha = (widgetId) => {
    if (window.recaptchaVerifier?.recaptcha)
        window.recaptchaVerifier.recaptcha.reset(
            window.recaptchaWidgetId
        );
}

export const setUpRecaptcha = async () => {
    if (window.recaptchaVerifier)
        return resetRecaptcha()
    else {
        window.recaptchaVerifier = new RecaptchaVerifier(
            'recaptcha-container',
            {
                'size': 'invisible',
                'callback': (response) => {
                }
            },
            firebaseAuth
        );
        window.recaptchaWidgetId =
            await window.recaptchaVerifier.render();
    }
    return window.recaptchaVerifier;
}

export const loginWithPhone = async (phone) => {
    try {
        const appVerifier = window.recaptchaVerifier;
        if (!appVerifier)
            throw {
                code: 'auth/missing-recaptcha', message: 'the recaptcha could not be rendered'
            }
        const result = await signInWithPhoneNumber(firebaseAuth, phone, appVerifier);
        return {
            ok: true,
            result
        }
    } catch (err) {
        resetRecaptcha()
        return { ok: false, code: err?.code, message: err?.message }
    }

}

export const signInWithConfirmationCredentials = async (verificationId, smsCode) => {
    try {

        const credential = PhoneAuthProvider.credential(verificationId, smsCode);

        if (credential){
            const result = await signInWithCredential(firebaseAuth,credential);
            return {
                ok: true,
                result
            }
        }
        return {
            ok: false
        }
    } catch (error) {
        const errorCode = error.code;
        const errorMessage = error.message;
        return {
            ok: false,
            errorMessage
        }
    }
}

export const signInWithGoogle = async () => {
    try {
        const result = await signInWithPopup(firebaseAuth, googleProvider);
        const { displayName, email, photoURL, uid,accessToken } = result.user;
        return {
            ok: true,
            displayName, email, photoURL, uid,
            accessToken
        }
    } catch (error) {
        // Handle Errors here.
        const errorCode = error.code;
        const errorMessage = error.message;
        // // The email of the user's account used.
        // const email = error.customData.email;
        // The AuthCredential type that was used.
        const credential = GoogleAuthProvider.credentialFromError(error);
        // ...
        return {
            ok: false,
            errorMessage
        }
    }
}

export const registerUserWithEmailPassword = async ({ email, password, displayName }) => {

    try {
        const resp = await createUserWithEmailAndPassword(firebaseAuth, email, password);
        const { uid, photoURL } = resp.user;

        await updateProfile(firebaseAuth.currentUser, { displayName })
        return {
            ok: true,
            photoURL,
            uid
        }
    } catch (error) {
        return { ok: false, errorMessage: error.message }
    }
}

export const loginWithEmailPassword = async ({ email, password }) => {
    try {
        const resp = await signInWithEmailAndPassword(firebaseAuth, email, password);
        const { uid, photoURL, displayName } = resp.user;

        return {
            ok: true,
            photoURL,
            displayName,
            uid
        }
    } catch (error) {
        return { ok: false, errorMessage: error.message }
    }
}

export const logoutFirebase = async () => {
    return await firebaseAuth.signOut();
}