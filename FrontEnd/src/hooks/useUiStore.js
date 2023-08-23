import { Modal } from 'antd';
import { useContext } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { onCloseBarcodeScannerModal, onCloseCustomerActionModal, onCloseCustomerAddressModal, onCloseCustomerModal, onCloseProductModal, onCloseSideBar, onHideBackTop, onLoadingPage, onOpenBarcodeScannerModal, onOpenCustomerActionModal, onOpenCustomerAddressModal, onOpenCustomerModal, onOpenProductModal, onOpenSideBar, onPageLoaded, onSetBarcode, onSetBarcodeScanned, onShowBackTop } from '../store/ui';
import { FeedbackContext } from '../context/FeedbackContext';
export const useUiStore = () => {
    const {
        customerModal,
        customerActionModal,
        productModal,
        barcodeScannerModal,
        customerAddressModal,
        loadingPageSpin,
        backTopButton,
        sideBar

    } = useSelector(state => state.ui);


    const dispatch = useDispatch();
    const {showErrorMessage,showWarningMessage,showSuccessMessage} = useContext(FeedbackContext);
    // const defaultSpinIcon=();

    const setLoadingPage = (spinContent = null, spinSize = 'large', spinIcon = undefined) => {
        dispatch(onLoadingPage({ spinContent, spinSize, spinIcon }));
    }

    const setPageLoaded = () => {
        dispatch(onPageLoaded());
    }

    const showBackTop = () => dispatch(onShowBackTop());
    const hideBackTop = () => dispatch(onHideBackTop());


    const openCustomerModal = () => {
        dispatch(onOpenCustomerModal());
    }

    const closeCustomerModal = () => {
        dispatch(onCloseCustomerModal());
    }

    const openCustomerActionModal = () => {
        dispatch(onOpenCustomerActionModal());
    }

    const closeCustomerActionModal = () => {
        dispatch(onCloseCustomerActionModal());
    }

    const openCustomerAddressModal = () => {
        dispatch(onOpenCustomerAddressModal());
    }

    const closeCustomerAddressModal = () => {
        dispatch(onCloseCustomerAddressModal());
    }



    const toggleCustomerModal = () => {
        (customerModal.isModalOpen)
            ? openCustomerModal() :
            closeCustomerModal();
    }

    const openProductModal = () => {
        dispatch(onOpenProductModal());
    }

    const closeProductModal = () => {
        dispatch(onCloseProductModal());
    }

    const openBarcodeScannerModal = () => {
        dispatch(onOpenBarcodeScannerModal());
    }

    const closeBarcodeScannerModal = () => {
        dispatch(onCloseBarcodeScannerModal());
    }

    const setBarcode = (barcode)=> dispatch(onSetBarcode(barcode))

    const setBarcodeScanned = (scanned)=>dispatch(onSetBarcodeScanned(scanned))


    const toggleProductModal = () => {
        (productModal.isModalOpen)
            ? openProductModal() :
            closeProductModal();
    }

    const openSideBar = () => {
        dispatch(onOpenSideBar());
    }

    const closeSideBar = () => {
        dispatch(onCloseSideBar());
    }

    const toggleSideBar = () => {
        (sideBar.isSideBarOpen)
            ? closeSideBar() :
            openSideBar();
    }


    const showErrorModal = (title, content) => {
        return Modal.error({
            title: title,
            content: content
        })
    }
    const showSuccessModal = (title, content) => {
        return Modal.success({
            title: title,
            content: content
        })
    }

    const showInfoModal = (title, content) => {
        return Modal.info({
            title: title,
            content: content
        })
    }

    const showWarningModal = (title, content) => {
        return Modal.warning({
            title: title,
            content: content,
        })
    }

    const showConfirm = async (title, content, icon = React.Component, onOk = () => { }, onCancel = () => { }) => {
        return Modal.confirm({
            title: title,
            content: content,
            icon: icon,
            onOk,
            onCancel,
            centered:true,
            confirmLoading: true
        });
    }



    // const showSuccessMessage = (content) => {
    //   messageApi.open({
    //     type: 'success',
    //     content: content,
    //   });
    // };
    // const showErrorMessage = (content) => {
    //   messageApi.open({
    //     type: 'error',
    //     content: content,
    //   });
    // };
    // const showWarningMessage = (content) => {
    //   messageApi.open({
    //     type: 'warning',
    //     content:content,
    //   });
    // };


    return {
        //* Propiedades
        customerModal,
        customerActionModal,
        productModal,
        barcodeScannerModal,
        customerAddressModal,
        sideBar,
        ...loadingPageSpin,
        ...backTopButton,
        //* Metodos
        openCustomerModal,
        closeCustomerModal,
        openCustomerActionModal,
        closeCustomerActionModal,
        toggleCustomerModal,
        openProductModal,
        closeProductModal,
        toggleProductModal,
        openBarcodeScannerModal,
        closeBarcodeScannerModal,
        openCustomerAddressModal,
        closeCustomerAddressModal,
        openSideBar,
        closeSideBar,
        toggleSideBar,
        showErrorModal,
        showSuccessModal,
        showInfoModal,
        showWarningModal,
        showSuccessMessage,
        showWarningMessage,
        showErrorMessage,
        setLoadingPage,
        setPageLoaded,
        showBackTop,
        hideBackTop,
        showConfirm
    }
}