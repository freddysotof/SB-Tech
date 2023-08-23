import { Route, Routes, Navigate } from 'react-router-dom'
import { AppLayout } from '../../layouts/AppLayout'
import { HomePage, SalesPage, ProductsPage } from '../../pages'
import { useEffect } from 'react'
import { useAuthStore, useOrderStore, useProductStore } from '../../hooks'

export const SBRoutes = () => {

    const {
        startLoadingProducts
    } = useProductStore();

    useEffect(() => {
        startLoadingProducts();
    }, [])


    return (

        <AppLayout
            appName={"SB App"}
        >
            <Routes>
                <Route path='/' element={<HomePage />} />
                <Route path='/sales' element={<SalesPage />} />
                <Route path='/products' element={<ProductsPage />} />
                <Route path='/*' element={<Navigate to="/" />}></Route>
            </Routes>

        </AppLayout>


    )
}
