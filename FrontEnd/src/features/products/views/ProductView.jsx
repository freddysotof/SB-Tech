import { Select } from 'antd';
import { EditableTable } from '../../../ui';
import { useProductStore } from '../../../hooks';
import { useEffect } from 'react';

export const ProductView = () => {

    const {
        products,
        startLoadingProducts,
        startSavingProduct,
        startDeletingProduct
    } = useProductStore();






    useEffect(() => {
        startLoadingProducts();
    }, [])

    const handleSaveProduct = async (product) => {
        await startSavingProduct({ ...product, id: product.key });
    }

    const handleDeleteProduct = async (product) => {
        await startDeletingProduct(product.key);
    }

    const columns = [
        {
            title: 'Código',
            dataIndex: 'code',

        },
        {
            title: 'Código de barras',
            dataIndex: 'barcode',
            editable: true,
        },
        {
            title: 'Nombre',
            dataIndex: 'name',
            editable: true,
        },
        {
            title: 'Descripción',
            dataIndex: 'description',
            width: '30%',
            editable: true,
        },
        {
            title: 'Precio',
            dataIndex: 'unitPrice',
            editable: true,
        },
    ];


    return (
        <EditableTable
            columns={columns}
            data={products}
            onSave={handleSaveProduct}
            onDelete={handleDeleteProduct}
        />
    );
}
