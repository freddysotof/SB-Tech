// import { CircularProgress, Grid } from "@mui/material"

import { Spin } from "antd";
import { useAuthStore } from "../../hooks";

export const CheckingAuth = () => {

    const { status } = useAuthStore();

    return (
        <Spin size='small' spinning={status === 'checking'}>

        </Spin>
        // <>
        //     <h1>Autenticando</h1>
        // </>
        // <Grid
        //     container
        //     spacing={0}
        //     direction="column"
        //     alignItems="center"
        //     justifyContent="center"
        //     sx={{ minHeight: '100vh', backgroundColor: 'primary.main', padding: 4 }}
        // >

        //     <Grid
        //         container
        //         direction='row'
        //         justifyContent='center'
        //     >
        //         <CircularProgress color="warning"/>
        //     </Grid>
        // </Grid>
    )
}
