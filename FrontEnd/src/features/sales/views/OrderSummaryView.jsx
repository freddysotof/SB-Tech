import { InfoOutlined, QuestionCircleFilled, QuestionOutlined } from "@ant-design/icons";
import { Avatar, Button, Card, Checkbox, Col, Divider, FloatButton, Form, Grid, Input, List, Radio, Row, Select, Space, Typography } from "antd"
import { useEffect, useState } from "react";
import { onFormValuesChange } from "../../../helpers";
import { useCartStore,  useForm, useOrderStore, useTheme, useUiStore } from "../../../hooks"
import {  Summary, SummaryItem } from "../components";

const { Option } = Select;

const { Title, Text } = Typography;
const { TextArea } = Input;

const {
    useBreakpoint
} = Grid;
export const OrderSummaryView = () => {
    const [isHover, setIsHover] = useState(false)

    const {
        items,
        summary
    } = useCartStore();


    const {
        activeOrder,
        shippingMethods,
        paymentMethods,
        startProcessingOrder,
        startUpdatingOrder
    } = useOrderStore();



    const {
        colorSbBg,
        colorSbText,
        colorSbActiveBg,
        colorPrimary,
        colorMenuBg,
        colorInfo,
        colorSuccess,
        colorSuccessBg,
        colorSuccessHover
    } = useTheme();

    const {
        showConfirm,
        showErrorModal,
        showWarningMessage,
        customerAddressModal,
        openCustomerAddressModal
    } = useUiStore();

    const screens = useBreakpoint();
    const [form] = Form.useForm();


    const data = [{
        text: 'Subtotal',
        value: 650.00
    },
    {
        text: 'Total',
        value: 650.00
    }

    ]
    const cardSelectedStyle = {
        border: colorSbActiveBg,
        background: colorSbActiveBg,
        color: colorSbText
    }

    const handleMouseEnter = () => {
        setIsHover(true);
    };
    const handleMouseLeave = () => {
        setIsHover(false);
    };

    const onFormChange = (_, allFields) => {
        const { note, shippingMethodCode, paymentMethodCode } = onFormValuesChange(_, allFields);
        startUpdatingOrder({ note, shippingMethodCode, paymentMethodCode });
    }

    const onChangeCustomerAddress = (value, { record }) => {
        if (value.length > 0) {
            openCustomerAddressModal();
        }

    }

    const handleProcessingOrder = async () => {
        const res = await startProcessingOrder();
        if (res && !res.isSuccessStatusCode)
            showErrorModal(res.message, (<Space direction={"vertical"}>{res.content.map((f, index) => (<Text key={index} strong>{f}</Text>))}</Space>))
    }




    const onSubmit = async (values) => {
        if (items.length === 0)
            return await showWarningMessage('Debe de tener al menos un item en carrito')
        await showConfirm('Procesar orden', 'Esta seguro que desea procesar esta orden?', <QuestionCircleFilled />, handleProcessingOrder);
    }

    return (
        <>
            <Row
                gutter={[0, { lg: 15 }]}
                align={{lg:''}}
                style={{height:'100%'}}
            >
                <Col lg={{ span: 24 }}>
                </Col>
                <Col>
                    <Form
                        form={form}
                        // initialValues={formValues}
                        initialValues={{
                            note: activeOrder?.note,
                            paymentMethodCode: activeOrder?.paymentMethodCode,
                            shippingMethodCode: activeOrder?.shippingMethodCode ?? [...shippingMethods].filter(x => x.isDefault).shift()?.code
                        }}
                        onFinish={onSubmit}
                        onFieldsChange={onFormChange}
                        autoComplete="off"
                    >
                        <Row
                            style={{ width: '100%' }}
                            gutter={[0, { xs: 13, sm: 30, lg: 0 }]}
                        >
                            <Col
                                lg={{ span: 24, offset: 1 }}
                            >
                                <Row
                                    gutter={[0, { xs: 10, sm: 10, lg: 0 }]}
                                >
                                    <Col
                                        xs={{ span: 24 }}
                                        sm={{ span: 22, push: 1, pull: 1 }}
                                        lg={{ order: 2, push: 0, pull: 0, span: 24 }}
                                    >
                                        <Summary info={{ items: { label: 'Cantidad de articulos', amount: items.length }, ...summary }} />
                                    </Col>
                                    <Col
                                        xs={{ span: 24 }}
                                        sm={{ span: 22, push: 1, pull: 1 }}
                                        lg={{ order: 1, span: 24, push: 0, pull: 0, offset: 0 }}
                                    >
                                        <Title level={5}>Notas</Title>
                                        <Form.Item
                                            name="note"
                                        >
                                            <TextArea
                                                rows={3}
                                                placeholder="Informacion adicional"
                                            />
                                        </Form.Item>
                                    </Col>
                                  
                                 
                                </Row>
                            </Col>
                            <Col
                                xs={{ span: 12, offset: 2, push: 4 }}
                                sm={{ span: 8, offset: 4, push: 4 }}
                                lg={{ span: 16, push: 2, pull: 0, offset: 0 }}
                            >
                                <Button
                                    size={'large'}
                                    ghost
                                    onMouseEnter={handleMouseEnter}
                                    onMouseLeave={handleMouseLeave}
                                    // type="primary"
                                    style={{
                                        background: colorSuccess,
                                        color: isHover ? colorSuccessHover : undefined,
                                        height: '2.5em'
                                    }}

                                    htmlType={"submit"}
                                    block
                                >
                                    Procesar orden
                                </Button>

                            </Col>
                        </Row>

                    </Form >
                </Col>
            </Row>



        </>
    )
    return (
        <>
            <Title style={{
                marginTop: '2em',
                textAlign: 'center'
            }} level={4}>Resumen de orden</Title>
            <Card
                bordered={false}
                bodyStyle={{
                    padding: '1em',
                    height: '20em',
                    overflow: 'auto'
                }}
            >
                <Row
                    gutter={[0, { xs: 10 }]}
                >
                    {
                        items.map(item => (
                            <Col key={item.code}
                                xs={{
                                    span: 24
                                }}
                            >
                                <SummaryItem  {...item} />
                            </Col>
                        ))
                    }
                </Row>

            </Card>
            <List
                style={{ marginTop: '1em' }}
                grid={{
                    gutter: 4,
                    xs: 1

                }}
                // itemLayout='horizontal'
                // bordered
                dataSource={data}
                renderItem={(item, index) => (
                    <List.Item>
                        <Row>
                            <Col xs={{ push: 1, span: 20 }}>
                                <Title level={5}>{item.text}</Title>
                            </Col>
                            <Col xs={{ span: 4 }}>
                                <Title level={5}>${item.value}</Title>
                            </Col>

                        </Row>
                        <Divider style={{ margin: '1em' }} />
                    </List.Item>

                )}
            />

            <Button
                size={'large'}
                ghost
                // type="primary"
                style={{
                    background: colorSuccess
                }}
                block
                onClick={onProcessOrder}
            >
                Procesar orden
            </Button>

        </>

    )
}
