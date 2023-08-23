import { ArrowLeftOutlined, ArrowRightOutlined, CheckOutlined, CopyFilled, ShoppingCartOutlined, SolutionOutlined, UserOutlined } from '@ant-design/icons';
import { Space, Typography, Card, Steps, FloatButton, QRCode, Grid, Row, Col } from 'antd';
import { useEffect } from 'react';
import { useCounter, useOrderStore, useTheme, useUiStore } from '../hooks';
import { ActionResult } from '../ui';
import { CartView, OrderSummaryView } from '../features/sales';
import { useNavigate } from 'react-router-dom';

const { Title, Text, Paragraph } = Typography;

const {
    useBreakpoint
} = Grid;
export const SalesPage = () => {
    const { colorPrimary, colorSbActiveBg } = useTheme()
    const screens = useBreakpoint();
    const {
        showErrorMessage,

    } = useUiStore();

    const {
        activeOrder,
        messageSaved,
        setOrderSaved,
        setActiveOrder,
        isNew
    } = useOrderStore();


    const {
        counter: currentStep,
        increment: nextStep,
        decrement: prevStep,
        refresh: setCurrentStep,
        reset: resetStep } = useCounter(1)

    const onStepChange = (value) => {
        setCurrentStep(value)
    }

    const onClickNextStep = () => {

        nextStep()
    }

    const steps = [


        {
            key: 1,
            title: 'Carrito',
            // description:'Description',
            // status: 'process',
            icon: <ShoppingCartOutlined />,
        },
        {
            key: 2,
            title: 'Confirmar',
            // description:'Description',
            // status: 'wait',
            icon: <SolutionOutlined />,
        },

    ]
    const onOrderSave = () => {
        resetStep();
        setOrderSaved();
    }

    const downloadQRCode = () => {
        const canvas = document.getElementById('myqrcode')?.querySelector('canvas');
        if (canvas) {
            const url = canvas.toDataURL();
            const a = document.createElement('a');
            a.download = 'QRCode.png';
            a.href = url;
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        }
    };

    useEffect(() => {
        setActiveOrder({})
    }, []);
    useEffect(() => {
        if (isNew) {
            setActiveOrder({})
            setCurrentStep(1)
        }

    }, [isNew])




    if (messageSaved?.length > 0)
        return (<ActionResult
            title={<Title level={3}>Orden Procesada</Title>}
            subTitle={<Text style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }} strong type='secondary'>{messageSaved}</Text>}
            onClick={onOrderSave}
        >
            <Space direction={'vertical'} style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>

                <QRCode
                    size={250}
                    type="canva"
                    value={activeOrder?.orderNumber}
                />
                <Paragraph

                    strong
                    copyable={{
                        tooltips: false,
                        icon: [<CopyFilled style={{ color: colorPrimary }} />, <CheckOutlined style={{ color: colorPrimary }} />]
                    }}>{activeOrder?.orderNumber}</Paragraph>
                <Text
                    type={"secondary"}
                    style={{
                        display: 'flex',
                        justifyContent: 'center',
                        textAlign: 'center'
                    }}
                    strong>
                </Text>
            </Space>

        </ActionResult>)
    return (
        <>
            {
                screens.lg || screens.xl || screens.xxl
                    ? (<Row>
                        <Col span={12}>
                            <CartView />
                        </Col>
                        <Col span={12}>
                            <OrderSummaryView />
                        </Col>
                    </Row>)
                    : <div style={{ display: 'flex', flexFlow: 'column wrap', height: '100%' }}>
                        <Steps
                            direction='horizontal'
                            current={currentStep}
                            onChange={onStepChange}
                            labelPlacement={'horizontal'}
                            responsive={false}
                            size={'small'}
                            initial={1}
                            items={steps}
                        />

                        <div
                            style={{
                                marginTop: '1em',
                                width: '100%',
                                flexGrow: 1,
                                display: 'flex',
                                flexFlow: 'column wrap'
                            }}
                        >

                            {currentStep === 1 && (
                                <CartView />
                            )}
                            {currentStep === 2 && (
                                <OrderSummaryView />
                            )}

                        </div>
                        {currentStep > 1 && (
                            <FloatButton
                                style={{
                                    right: 'calc(100% - 7em)',
                                    bottom: 10,
                                    width: '5rem',
                                    background: colorSbActiveBg
                                }}
                                shape={'square'}
                                type='primary'
                                onClick={() => prevStep()}
                                icon={<ArrowLeftOutlined />}
                            />
                        )}


                        {currentStep < steps.length && (
                            <FloatButton
                                shape={'square'}
                                type='primary'
                                onClick={() => onClickNextStep()}
                                style={{
                                    bottom: 10,
                                    width: '5rem',
                                }}

                                icon={<ArrowRightOutlined />}
                            />
                        )}

                    </div>

            }

        </>
    )

}
