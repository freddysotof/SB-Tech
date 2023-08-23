import { da } from 'date-fns/locale';
import { useDispatch, useSelector } from 'react-redux';
import { onAddItem, onDeleteItem, onRecalculateCart, onSetActiveItem, onSetActiveLocationCode, onSetActivePriceLevel, onSetAverageTicketProgress, onSetErrorMessage, onUpdateItem } from '../store/cart';
import { useUiStore } from './useUiStore';
export const useCartStore = () => {
    const {
        items,
        activeItem,
        summary,
        messageSaved,
        errorMessage,
    } = useSelector(state => state.cart);



    const dispatch = useDispatch();



    const setActiveItem = (item) => {
        dispatch(onSetActiveItem(item))
    };



    const startSavingItem = (quantity) => {
        if (!!activeItem.id) {
            dispatch(onUpdateItem(
                {
                    updatedItem: {
                        ...activeItem,
                        quantity,
                    }
                }));
            return;
        }

        const newItem = {
            ...activeItem,
            id: Date.now(),
            quantity,
        };
        dispatch(onAddItem({ newItem }));
    }




    const startDeletingItem = (id) => {
        dispatch(onDeleteItem(id));
    }

    const recalculateCart = () => {
        dispatch(onRecalculateCart({}))
    }



    return {
        //* Propiedades
        items,
        total: items.reduce((a, v) => a = a + v.unitPrice, 0.00),
        activeItem,
        isActiveItem: !!activeItem,
        hasEcommerceInfo: !!activeItem?.title,
        summary,
        messageSaved,
        errorMessage,

        //* Metodos
        startSavingItem,
        startDeletingItem,
        setActiveItem,
        recalculateCart


    }
}