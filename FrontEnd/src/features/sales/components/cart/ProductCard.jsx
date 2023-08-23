import { Card, Col, Row,  Spin, Typography } from "antd"
import PropTypes from 'prop-types'
import { useState } from "react";
import  '../../styles/ProductCard.css'
const { Text } = Typography;
export const ProductCard = ({ code, name,description, onCardClick = async(() => { }), cardStyle = {}, size = 'small', hoverable = true }) => {
    const [loading, setLoading] = useState(false)



    const onClick = async () => {
        setLoading(true)
        await onCardClick();
        setLoading(false)
    }
    return (
        <Spin
            size={"large"}
            spinning={loading}
            wrapperClassName={"spin-product-card"}
        >
            <Card
                onClick={onClick}
                style={{
                    width: '100%',
                    ...cardStyle,
                }}
                hoverable={hoverable}
                size={size}
                bodyStyle={{
                    width:'100%',
                    padding: 14,
                }}

            >
                <Card.Meta
                    description={(
                        <Row
                            gutter={[0, { xs: 5 }]}
                            wrap={true}
                        >
                            <Col xs={{ span: 24 }} style={{ textAlign: 'center' }}>
                                <Text style={{ color: cardStyle?.color }} strong>{name} ({code})</Text>
                            </Col>
                            <Col xs={{ span: 24 }} style={{ textAlign: 'center' }}>
                                <Text style={{ color: cardStyle?.color }} strong>{description}</Text>
                            </Col>
                        </Row>
                    )
                    }
                >
                </Card.Meta >
            </Card >
        </Spin>
    )
}
ProductCard.propTypes = {
    name: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,
    code: PropTypes.string.isRequired,
    onCardClick: PropTypes.func,
    cardStyle: PropTypes.object,
    size: PropTypes.string
}
