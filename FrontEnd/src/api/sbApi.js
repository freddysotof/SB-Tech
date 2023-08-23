import axios from 'axios';
import { getEnvVariables } from '../helpers';
import { generateHash } from '../libs/generateHash';


const { VITE_SB_API_URL, VITE_AUTH_SIGNATURE_KEY } = getEnvVariables()

export const sbApi = axios.create({
    baseURL: VITE_SB_API_URL,

})

sbApi.interceptors.request.use(config => {
    if (config.data)
        config.data = {
            data: {
                ...config.data
            }
        }

    const authSignature = generateHash(config.data, VITE_AUTH_SIGNATURE_KEY);

    const token = localStorage.getItem('jwt')
    config.headers = {
        ...config.headers,
        'X-Auth-Signature': authSignature,
        'Authorization': `Bearer ${token}`
    };


    return config;
})

// Refresh token interceptor
sbApi.interceptors.response.use(
    (res) => {
        return res;
    },
    async (err) => {
        const originalConfig = err.config;

        if (originalConfig.url !== "/auth/login" && err.response) {
            // Access Token was expired
            if ((err.response.status === 401 || err.response.status === 403) && !originalConfig._retry) {
                originalConfig._retry = true;

                try {
                    const accessToken = localStorage.getItem('jwt');
                    const refreshToken = localStorage.getItem('refresh-token');
                    const { credentials } = await sbApi.post("/auth/token/renew", {
                        accessToken,
                        refreshToken
                    });
                    localStorage.setItem('jwt', credentials.accessToken.token);
                    localStorage.setItem('jwt-init-date', new Date().getTime());
                    localStorage.setItem('refresh-token', credentials.refreshToken.token);
                    return sbApi(originalConfig);
                } catch (_error) {

                    return Promise.reject(_error);
                }
            }
        }
        return Promise.reject(err);
    }
);


sbApi.interceptors.response.use(
    response => {
        const { status, statusText } = response;
        const { Messages, Data, IsSuccessStatusCode, NextPage, PreviousPage } = response.data;
        return {

            hasNextPage: !!NextPage,
            hasPreviousPage: !!PreviousPage,
            status,
            statusText,
            data: Data,
            ...Data,
            messages: Messages,
            isSuccessStatusCode: IsSuccessStatusCode
        }
    },
    error => {
        let status, statusText, Errors = null, IsSuccessStatusCode = false, Exception = null;
        if (error.response) {
            ({ status, statusText } = error.response);
            ({ Errors, IsSuccessStatusCode, Exception } = error.response.data);
        }
        else {
            status = error.code;
            statusText = error.message
            Errors = [{ Message: error.message, StatusError: error.code }];
            IsSuccessStatusCode = false;
        }
        throw {
            isSuccessStatusCode: IsSuccessStatusCode,
            statusText,
            message: Exception,
            statusCode: status,
            details: Errors.map(error => {
                return {
                    message: error.Message,
                    statusError: error.StatusError,
                    description: error.Description,
                    stackTrace: error.StackTrace
                };
            })
        }
    },
)