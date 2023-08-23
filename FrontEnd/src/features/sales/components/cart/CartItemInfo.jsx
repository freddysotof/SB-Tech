import { Col, Row, Space, Typography } from 'antd'
import React from 'react'
import { formatToCurrency } from '../../../../helpers';


const { Text } = Typography;
export const CartItemInfo = ({ item }) => {
  const {
    id,
    code,
    name,
    description,
    unitPrice,
    quantity,
    totalAmount,
  } = item;
  return (
    <Row
      style={{
        minHeight: '3rem',
        width: '100%',
        margin: '0.5em'
      }}
    >
      <Col
        xs={{
          span: 12
        }}
        sm={{
          span: 12
        }}
      >
        <Row>
          <Col>
            <Text strong >{code} - {name ?? description}</Text>
          </Col>
          <Col>

          </Col>
        </Row>
      </Col>
      <Col
        xs={{
          span: 12
        }}
        sm={{
          push: 2,
          span: 10,
        }}
      >
        <Row
          justify={{ xs: 'end', sm: 'end',lg:'end' }}
        >
          <Col
          >
            <Space size={'small'}>
              <Text strong>{quantity}</Text>
              <Text strong>x</Text>
              <Text strong>{formatToCurrency(unitPrice)}</Text>
            </Space>
          </Col>
        </Row>

      </Col>
      <Col
        xs={{
          span: 24
        }}
        sm={{
          push: 2,
          span: 10,
        }}
        lg={{
          span: 24
        }}
      >
        <Row
          justify={{ xs: 'end', sm: 'end',lg:'end' }}
        >

          <Col
            sm={{ span: 24, push: 20 }}
          // lg={{
          //   span:24,push:0
          // }}
          
          >
            <Space direction='horizontal'>
              <Text strong>${formatToCurrency(totalAmount)}</Text>
            </Space>
          </Col>
        </Row>






      </Col>
    </Row>
  )
}
