import { Button, Col, Result, Row } from "antd"
import { useNavigate } from "react-router-dom"
import { useEffect, useState } from "react";
import { useOrderStore } from "../hooks";

const boundingBoxStyle = {
  stroke: '#fff',
  lineWidth: 4,
  radii: 10,
  gap: 0.5,
  margin: 0.1
};
export const HomePage = () => {
  const navigate = useNavigate();

  const {
    clearOrder
  } = useOrderStore();

  const onClick = () => {
    // clearOrder();
    navigate('/sales');
  }



  return (
    <Row
      style={{ height: '100%' }}
    >
      <Col
        xs={{ span: 24 }}
        style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}
      >
        <Result
          status="404"
          title="Freddy Soto"
          subTitle=""
          extra={<Button htmlType="button" onClick={onClick} type="primary">Nuevo pedido</Button>}
        />
      </Col>
    </Row>

  )
}
