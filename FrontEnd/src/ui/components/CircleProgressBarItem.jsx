import { Col,  Progress, Row, Typography } from 'antd'


const { Title, Text } = Typography;
export const CircleProgressBarItem = ({ width = 60, percent = 25, title, subtitle, status, icon }) => {
    return (
        <>
            <Row

                justify={{ xs: 'center' }}
                align={{ xs: 'middle' }}
            >
                <Col
                    xs={{ span: title ?10:24 }}>
                    <Progress
                        type='circle'
                        percent={percent}
                        width={width}
                        // showInfo={false}
                        status={status}
                        format={() => (icon)}
                        success={{percent:0}}
                    />
                </Col>
                {title ?
                    (<Col
                        xs={{ span: 14 }}
                    >
                        <Row
                            justify={{ xs: 'center' }}
                            align={{ xs: 'middle' }}
                        >
                            <Col
                                xs={{ span: 24 }}
                            >
                                <Title
                                    level={4}
                                    style={{ 
                                        marginBottom: 0 
                                    }}
                                >{title}</Title>
                            </Col>
                            <Col
                                xs={{ span: 24 }}
                            >
                                <Text
                                    className='text-muted'
                                    style={{ 
                                        marginTop: 0
                                    
                                    }}
                                >{subtitle}</Text>
                            </Col>

                        </Row>
                    </Col>)
                    :(<></>)}

            </Row>
        </>


    )
}
