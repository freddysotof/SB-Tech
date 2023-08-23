import { useEffect, useState } from "react"


export const useBarcode = () => {
    const [barcodeDetector, setBarcodeDetector] = useState();
    const [barcode, setBarcode] = useState()
    const [scanned, setScanned] = useState(false);

    const scanBarcode = (result) => {
        if (result)
            setBarcode(result);
        // setScanned(false);
    }
   
    return {
        barcode,
        scanned,
        options: {
            delay: 1000,
            formats: [
                // 'aztec',
                'code_128',
                'code_39',
                'code_93',
                'codabar',
                // 'data_matrix',
                'ean_13',
                'ean_8',
                'itf',
                // 'pdf417',
                'qr_code',
                'upc_a',
                'upc_e'
            ],
        },

        scanBarcode,
        setBarcode,
        setScanned

    }
}