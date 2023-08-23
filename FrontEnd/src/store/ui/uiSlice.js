import { createSlice } from '@reduxjs/toolkit';
import React from 'react';

export const uiSlice = createSlice({
    name: 'ui',
    initialState: {
        
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
   
    onOpenProductModal,
    onCloseProductModal,
    onOpenBarcodeScannerModal,
    onCloseBarcodeScannerModal,
   
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