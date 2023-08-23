import { List } from 'antd'
import React from 'react'
import { useCartStore } from '../../../../hooks'
import { SummaryItem } from '..'

export const Summary = ({info}) => {
  return (
    <List
                itemLayout='horizontal'
                size={'small'}
                split={false}
                dataSource={Object.values(info)}
                renderItem={(item, index) => (
                    <SummaryItem key={index} item={item} />
                )}
            />
  )
}
