import { createSlice } from '@reduxjs/toolkit';
import React from 'react';

export const uiSlice = createSlice({
    name: 'ui',
    initialState: {
        customerActionModal: {
            isModalOpen: false,
            okText: '',
            cancelText: '',
            title: '',
            onOk: () => { },
            onCancel: () => { },
            showTextArea: false
        },
        customerModal: {
            isModalOpen: false,
            okText: '',
            cancelText: '',
            title: '',
            onOk: () => { },
            onCancel: () => { },
            showTextArea: false
        },
        productModal: {
            isModalOpen: false,
            okText: '',
            cancelText: '',
            title: '',
            onOk: () => { },
            onCancel: () => { },
            showTextArea: false
        },
        barcodeScannerModal: {
            barcode: '',
            scanned: false,
            isModalOpen: false,
            okText: '',
            cancelText: '',
            title: '',
            onOk: () => { },
            onCancel: () => { },
            showTextArea: false
        },
        customerAddressModal: {
            isModalOpen: false,
            okText: 'Confirmar',
            cancelText: '',
            title: '',
            onOk: () => { },
            onCancel: () => { },
            showTextArea: false
        },
        loadingPageSpin: {
            isSpinning: false,
            spinContent: React.Fragment,
            spinSize: 'large',
            spinIcon: undefined
        },
        backTopButton: {
            backTopVisible: true,
        },
        sideBar: {
            isSideBarOpen: false
        }

    },
    reducers: {
        onOpenCustomerActionModal: (state, { payload }) => {
            state.customerActionModal.isModalOpen = true;
        },
        onCloseCustomerActionModal: (state) => {
            state.customerActionModal.isModalOpen = false;
        },
        onOpenCustomerModal: (state, { payload }) => {
            state.customerModal.isModalOpen = true;
        },
        onCloseCustomerModal: (state) => {
            state.customerModal.isModalOpen = false;
        },

        onOpenProductModal: (state, { payload }) => {
            state.productModal.isModalOpen = true;
        },
        onCloseProductModal: (state) => {
            state.productModal.isModalOpen = false;
        },
        onOpenBarcodeScannerModal: (state, { payload }) => {
            state.barcodeScannerModal.isModalOpen = true;
        },
        onCloseBarcodeScannerModal: (state) => {
            state.barcodeScannerModal.isModalOpen = false;
        },
        onOpenCustomerAddressModal: (state, { payload }) => {
            state.customerAddressModal.isModalOpen = true;
        },
        onCloseCustomerAddressModal: (state) => {
            state.customerAddressModal.isModalOpen = false;
        },
        onSetBarcode: (state, { payload }) => {
            state.barcodeScannerModal.barcode = payload;
        },
        onSetBarcodeScanned: (state, { payload }) => {
            state.barcodeScannerModal.scanned = payload;
        },
        onLoadingPage: (state, { payload }) => {
            state.loadingPageSpin.isSpinning = true;
            if (payload.spinContent)
                state.loadingPageSpin.spinContent = payload.spinContent;
            state.loadingPageSpin.spinSize = payload.spinSize;
            state.loadingPageSpin.spinIcon = payload.spinIcon;
        },
        onPageLoaded: (state) => {
            state.loadingPageSpin.isSpinning = false;
            state.loadingPageSpin.spinContent = '';
            state.loadingPageSpin.spinSize = '';
            state.loadingPageSpin.spinIcon = undefined;
        },
        onShowBackTop: (state) => {
            state.backTopButton.backTopVisible = true
        },
        onHideBackTop: (state) => {
            state.backTopButton.backTopVisible = false
        },
        onOpenSideBar: (state) => {
            state.sideBar.isSideBarOpen = true
        },
        onCloseSideBar: (state) => {
            state.sideBar.isSideBarOpen = false;
        },
    }
});
// Action creators are generated for each case reducer function
export const {
    onOpenCustomerModal,
    onCloseCustomerModal,
    onOpenCustomerActionModal,
    onCloseCustomerActionModal,
    onOpenProductModal,
    onCloseProductModal,
    onOpenBarcodeScannerModal,
    onCloseBarcodeScannerModal,
    onOpenCustomerAddressModal,
    onCloseCustomerAddressModal,
    onSetBarcode,
    onSetBarcodeScanned,
    onLoadingPage,
    onPageLoaded,
    onShowBackTop,
    onHideBackTop,
    onOpenSideBar,
    onCloseSideBar
} = uiSlice.actions;
//! https://react-redux.js.org/tutorials/quick-start