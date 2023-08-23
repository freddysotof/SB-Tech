import { useEffect, useState } from "react";
import { useBarcode, useProductStore, useUiStore } from "../../../../hooks"
import { Modal } from "antd";
import { QrScanner } from "@yudiel/react-qr-scanner";

export const ScanBarcodeModal = () => {
    const [loading, setLoading] = useState(false)
    const {
        setLoadingPage,
        setPageLoaded,
        closeBarcodeScannerModal,
        barcodeScannerModal
    } = useUiStore();


    const {
        startSearchingProductByBarcode,
    } = useProductStore();

    const {
        barcode,
        scanned,
        scanBarcode,
        setScanned,
        options,
        setBarcode
    } = useBarcode();

    const onCancelModal = () => {
        closeBarcodeScannerModal();
        setScanned(false);
    }

    const handleCaptureBarcode = (result) => {
        setScanned(true);
        scanBarcode(result);
        setTimeout(() => {
            setScanned(false)
            closeBarcodeScannerModal();
        }, 1000);

    }



    useEffect(() => {
        const asyncFunc = async () => {
            setLoadingPage();
            await startSearchingProductByBarcode(barcode);
            setPageLoaded();
        }
        if (barcode && barcode.length > 0) {
            asyncFunc();
            setBarcode("");
        }
    }, [barcode])


    useEffect(() => {
    }, [])

    return (
        <Modal
            closable={false}
            open={barcodeScannerModal.isModalOpen}
            okButtonProps={{ disabled: !barcode }}
            onCancel={() => onCancelModal()}
            centered
            forceRender={true}
            keyboard={false}
            confirmLoading={scanned}
            destroyOnClose={true}
        >
            <QrScanner
                scanDelay={1000}
                onDecode={handleCaptureBarcode}
            />
        </Modal>
    )
}
