import { Modal } from 'antd';
import { useContext } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { onCloseBarcodeScannerModal, onCloseProductModal, onCloseSideBar, onHideBackTop, onLoadingPage, onOpenBarcodeScannerModal,  onOpenProductModal, onOpenSideBar, onPageLoaded, onSetBarcode, onSetBarcodeScanned, onShowBackTop } from '../store/ui';
import { FeedbackContext } from '../context/FeedbackContext';
export const useUiStore = () => {
    const {
      
        productModal,
        barcodeScannerModal,
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





    return {
        //* Propiedades
      
        productModal,
        barcodeScannerModal,
        sideBar,
        ...loadingPageSpin,
        ...backTopButton,
        //* Metodos
      
        openProductModal,
        closeProductModal,
        toggleProductModal,
        openBarcodeScannerModal,
        closeBarcodeScannerModal,
       
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