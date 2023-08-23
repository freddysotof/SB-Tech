import { useDispatch, useSelector } from 'react-redux';
import { onLoadShippingMethods, onProcessOrder, onSavedOrder, onSetActiveOrder, onLoadPaymentMethods, onSetNewOrder } from '../store/order';
import { onClearItems} from '../store/cart';
import { sbApi } from '../api';
export const useOrderStore = () => {
    const {
        shippingMethods,
        paymentMethods,
        messageSaved,
        activeOrder
    } = useSelector(state => state.order);


    const {
        username,
        displayName,
        email,
        // phone,
        id,

    } = useSelector(state => state.auth);

    const {
        items,
        summary
    } = useSelector(state => state.cart);

    const dispatch = useDispatch();




    const startUpdatingOrder = ({ note }) => {
        dispatch(onSetActiveOrder({
            ...activeOrder,
            note,
        }))
    }


    const startProcessingOrder = async () => {
        try {
            const orderBody = {
                createdBy:email,
                username,
                customerCode: email,
                customerName: displayName,
                customerEmail: email,
                customerPhone: '',
                note: activeOrder.note,
                totalAmount: summary.total.amount,
                taxAmount: summary.tax.amount,
                netAmount: summary.net.amount,
                docDate: new Date(),
                details: items.map(item => {
                    return {
                        itemNumber: item.code,
                        itemDescription: item.name ?? item.description,
                        quantity: item.quantity,
                        unitPrice: item.unitPrice,
                        totalAmount: item.totalAmount,
                        taxAmount: item.taxAmount,
                        netAmount: item.netAmount,
                        notes: item.notes
                    }
                })
            }
            const { data } = await sbApi.post('/orders',  orderBody );
            const messageSaved = `El numero de orden es ${data.orderNumber}`;
            dispatch(onProcessOrder({ messageSaved, order: data }));
        } catch ({ isSuccessStatusCode, message, details, statusText, ...rest }) {
            return {
                isSuccessStatusCode,
                message: message,
                content: details.map(e => e.statusError)
            }
        }


    }


    const setActiveOrder = (order) => dispatch(onSetActiveOrder(order))

    const clearOrder = () => {
        dispatch(onClearItems());
        setActiveOrder()
    }

    const setOrderSaved = () => {
        dispatch(onClearItems());
        setActiveOrder()
    }

   



    return {
        //* Propiedades
        shippingMethods,
        paymentMethods,
        messageSaved,
        activeOrder,
        isNew: !activeOrder,
        //* Metodos
       
        startProcessingOrder,
        startUpdatingOrder,
        setOrderSaved,
        clearOrder,
        setActiveOrder

    }
}