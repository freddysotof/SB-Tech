import { useDispatch, useSelector } from 'react-redux';
import { sbApi } from '../api';
import { onClearProducts, onSetLoading, onClearErrorMessage, onSetErrorMessage, onLoadProducts, onSetActiveProduct, onAddProduct, onUpdateProduct, onDeleteProduct } from '../store/product';
import { onSetActiveItem, onSetActiveLocationCode } from '../store/cart';
import { useUiStore } from '../hooks';
export const useProductStore = () => {
    const {
        page,
        limit,
        products,
        activeProduct,
        isLoading,
        hasPreviousPage,
        hasNextPage,
        errorMessage
    } = useSelector(state => state.product);

    const dispatch = useDispatch();
    const { showErrorMessage } = useUiStore();
    const startLoadingProducts = async (value) => {
        try {

            dispatch(onSetLoading());
            const { data, hasNextPage, hasPreviousPage } = await sbApi.get(`/products?criteria=${value ?? null}`);
            dispatch(onLoadProducts({ products: data ?? [], page: page, hasNextPage, hasPreviousPage }));
        } catch ({ message }) {
            dispatch(onSetErrorMessage(message));
        }
    }

    const startSearchingProductByBarcode = async (barcode) => {
        try {
            const { data } = await sbApi.get(`/products/by-barcode/${barcode}`);
            dispatch(onSetActiveItem({ ...data, quantity: 1, id: null }));
        } catch ({ message, ...rest }) {
            dispatch(onSetErrorMessage(message));
        }
    }

    const startSearchingProductByCode = async (code) => {
        try {
            const { data } = await sbApi.get(`/products/by-code/${code}`);
            dispatch(onSetActiveItem({ ...data, quantity: 1, id: null, taxPercentage: 18 }));
        } catch ({ message, ...rest }) {
            dispatch(onSetErrorMessage(message));
        }
    }

    const startSavingProduct = async (product) => {
        try {
            console.log(product)
            if (product.id) {
                const { data } = await sbApi.put(`/products/${product.id}`, product);
                console.log(data);
                dispatch(onUpdateProduct(data))
            } else {
                const { data } = await sbApi.post(`/products`, product);
                dispatch(onAddProduct(data))
            }

        } catch ({ message, ...rest }) {
            dispatch(onSetErrorMessage(message));
        }
    }

    const startDeletingProduct = async (id) => {
        try {

           await sbApi.delete(`/products/${id}`);
            dispatch(onDeleteProduct(id))
        } catch ({ message, ...rest }) {
            dispatch(onSetErrorMessage(message));
        }
    }

    const setActiveProduct = (product) => dispatch(onSetActiveProduct({ ...product, taxPercentage: 18 }));

    const startClearProducts = () => {
        dispatch(onClearProducts());
    }

    const clearErrorMessage = () => dispatch(onClearErrorMessage())

    return {
        //* Propiedades
        products,
        activeProduct,
        ...activeProduct,
        hasActiveProduct: !!activeProduct,
        isLoading,
        limit,
        page,
        hasPreviousPage,
        hasNextPage,
        errorMessage,
        //* Metodos
        // setProductInfo,
        startLoadingProducts,
        startSearchingProductByBarcode,
        startSearchingProductByCode,
        startSavingProduct,
        startDeletingProduct,
        startClearProducts,
        setActiveProduct,
        clearErrorMessage
    }
}