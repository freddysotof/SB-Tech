// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { getAuth } from 'firebase/auth'
import { getFirestore } from 'firebase/firestore/lite'
import { getDatabase } from 'firebase/database';
import { getStorage } from "firebase/storage";
import { getMessaging,getToken } from "firebase/messaging";
import { getEnvVariables } from "../helpers";


const {VITE_FIREBASE_API_KEY,
  VITE_FIREBASE_AUTH_DOMAIN,
  VITE_FIREBASE_PROJECT_ID,
  VITE_FIREBASE_MESSAGING_SENDER_ID,
  VITE_FIREBASE_APP_ID,
  VITE_FIREBASE_STORAGE_BUCKET,
  VITE_FIREBASE_MEASUREMENT_ID} = getEnvVariables();



const firebaseConfig = {
  
  apiKey: VITE_FIREBASE_API_KEY,

  authDomain: VITE_FIREBASE_AUTH_DOMAIN,

  projectId: VITE_FIREBASE_PROJECT_ID,

  storageBucket: VITE_FIREBASE_STORAGE_BUCKET,

  messagingSenderId:VITE_FIREBASE_MESSAGING_SENDER_ID,

  appId: VITE_FIREBASE_APP_ID,

  measurementId: VITE_FIREBASE_MEASUREMENT_ID
};

// Initialize Firebase
export const firebaseApp = initializeApp(firebaseConfig);
export const firebaseAuth = getAuth(firebaseApp);
firebaseAuth.useDeviceLanguage();
export const firebaseCloudStore = getFirestore(firebaseApp);
export const firebaseDatabase = getDatabase(firebaseApp);
export const firebaseStorage = getStorage(firebaseApp);
  // Initialize Firebase Cloud Messaging and get a reference to the service
export const firebaseMessaging = getMessaging(firebaseApp);
