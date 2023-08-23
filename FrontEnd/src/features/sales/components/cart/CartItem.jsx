import { Avatar, Button, Col, Row, Space, Typography } from 'antd';
import { DeleteColumnOutlined, DeleteOutlined, EditOutlined, UserOutlined } from '@ant-design/icons'
import {
  LeadingActions,
  SwipeableList,
  SwipeableListItem,
  SwipeAction,
  TrailingActions,
  Type as ListType
} from 'react-swipeable-list';

import 'react-swipeable-list/dist/styles.css'
import { useState } from 'react';
import { useCartStore, useTheme } from '../../../../hooks';
import { SwipeActionItem, SwipeTrailingActions } from '../../../../ui/components';
import { CartItemInfo } from '..';

const { Text, Title } = Typography;
export const CartItem = ({ item }) => {
  const {
    id,
  } = item;

  const {
    colorPrimary,
    colorMenuBg
  } = useTheme();


  const {
    startDeletingItem,
    setActiveItem
  } = useCartStore();


  const handleDeleteItem = () => {
    startDeletingItem(id);

  }

  const handleEditItem = () => {
    
    setActiveItem(item)
  }

  return (
    <SwipeableListItem
      fullSwipe={true}
      listType={ListType.IOS}
      trailingActions={(
        <SwipeTrailingActions>
          <SwipeActionItem
            style={{
              background: colorPrimary
            }}
            onClick={handleEditItem}
          >
            <EditOutlined style={{ fontSize: '2em' }} />
            <span>Editar</span>
          </SwipeActionItem>
          <SwipeActionItem
            destructive={true}
            
            style={{
              background: colorMenuBg
            }}

            onClick={handleDeleteItem}
          >
            <DeleteOutlined style={{ fontSize: '2em' }} />
            <span>Delete</span>
          </SwipeActionItem>
        </SwipeTrailingActions>
      )}
      

    >
      <CartItemInfo item={item}/>
    </SwipeableListItem>
  )
}
