export const getEnvVariables = ()=>{

    return{
        VITE_SB_API_URL:import.meta.env.VITE_SB_API_URL,
        VITE_MODE:import.meta.env.VITE_MODE,
        VITE_AUTH_SIGNATURE_KEY:import.meta.env.VITE_AUTH_SIGNATURE_KEY,
        VITE_SYSTEM_CODE:import.meta.env.VITE_SYSTEM_CODE,
        ...import.meta.env
    }
}