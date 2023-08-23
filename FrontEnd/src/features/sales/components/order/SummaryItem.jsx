import { Avatar, Card, Col, Divider, List, Row, Space, Typography } from 'antd';
import { UserOutlined } from '@ant-design/icons'
import { formatToCurrency } from '../../../../helpers';
const { Text, Title } = Typography;
export const SummaryItem = ({ item }) => {
    return (
        <List.Item
            style={{ paddingTop: 0, paddingBottom: 0 }}
        >
            <Row style={{ width: '100%' }}>
                <Col
                    xs={{ span: 14 }} sm={{span:12}}
                >
                    <Title
                        style={{margin:0}}
                        level={5}
                        strong>
                            {item.label}
                    </Title>
                </Col>
                <Col xs={{ span: 10 }} sm={{span:12}} style={{ textAlign: 'end' }}>

                    <Title 
                    level={5} 
                    strong
                    style={{margin:0}}
                    >
                        {item.isMoney ? '$' : ''}{formatToCurrency(item.amount)}
                    </Title>
                </Col>
            </Row>
        </List.Item>
    )
  
}
