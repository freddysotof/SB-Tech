import Base64 from 'crypto-js/enc-base64';
import { HmacSHA256, SHA256,enc } from 'crypto-js';
import { toHexString } from '../utils';

export const generateHash = (data,key) => {
    // const hashDigest = SHA256(data);
    // const keyDigest = SHA256(key);
    const hash  = HmacSHA256(JSON.stringify(data),key);
    return hash.toString(enc.Hex)
};
