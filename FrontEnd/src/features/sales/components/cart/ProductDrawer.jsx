import { MinusOutlined, PlusOutlined } from "@ant-design/icons";
import { Button, Carousel, Col, Input, Radio, Row, Select, Typography } from "antd"
import { Fragment, useEffect } from "react";
import { useState } from "react";
import { formatToMoney } from "../../../../helpers";
import { useCartStore, useCounter, useForm, useProductStore, useTheme } from "../../../../hooks";
import { EmptyCarousel } from "../../../../ui/components";

const { Title, Text, Paragraph } = Typography;



export const ProductDrawer = () => {
    const [btnDisabled, setBtnDisabled] = useState(false);

    const {

        colorSbBg,
        colorSbActiveBg
    } = useTheme()


    const {
        isActiveItem,
        activeItem,
        hasEcommerceInfo,
        setActiveItem,
        setActivePriceLevel,
        setActiveLocationCode,
        startSavingItem
    } = useCartStore();

    const {
        startClearProducts
    } = useProductStore();



    const {
        name,
        description,
        unitPrice,
        photoUrl,
        taxPercentage
    } = activeItem;


    const { counter, increment, decrement, reset, refresh } = useCounter(activeItem?.quantity ?? 1);

    const {
        formState,
        onInputChange,
    } = useForm({

    });


    const onIncrement = () => {
        increment();
    }
    const onDecrement = () => {
        if (counter - 1 === 0)
            return;
        decrement();
    }

    const onCounterChange = ({ target }) => {
        refresh(target.value);
    }


    const onValueChange = (name, value) => {
        onInputChange({ target: { name, value } })

    }





    useEffect(() => {
        if (isActiveItem) {

        } else {
            reset()
            setFormValues(initialFormValue)
        }

    }, [activeItem])




    const handleAddToCart = () => {
        startSavingItem(counter)
        setActiveItem(null);
        startClearProducts();
    }

    return (
        <Row
            align={{ xs: 'middle' }}
            justify={{ xs: 'center' }}
            gutter={[0, { xs: 30, sm: 30 }]}
        >
            <Col
                xs={{ span: 24 }}
                sm={{ span: 22, push: 2 }}
            >
                <Title level={3} style={{ margin: 0, color: colorSbBg }}>
                    {name}
                </Title>

            </Col>
            <Col
                xs={{ span: 24 }}
                sm={{ span: 20, push: 2 }} >
                <Row
                    gutter={[0, { xs: 10, sm: 15 }]}>
                    <Col xs={{ span: 24 }}>
                        <Row
                            gutter={[0, { xs: 15, sm: 0 }]}
                        >
                            <Col xs={{ span: 12 }}>
                                <Row gutter={[{ xs: 0 }, { xs: 10, sm: 20 }]}
                                    justify={{ xs: 'center', sm: 'center' }}
                                    align={{ xs: 'top', sm: 'middle' }}
                                >

                                    <Col style={{ textAlign: 'center' }}>
                                        
                                            (<EmptyCarousel
                                                style={{ color: colorSbBg, }}
                                                quantity={2}
                                                position="bottom"
                                            />)

                                        {/* {!![photoUrl]
                                            ? (
                                                <Carousel
                                                    infinite={false}
                                                    style={{ color: colorSbBg, minHeight: '10rem' }}
                                                    // effect="fade"
                                                    autoplay
                                                    autoplaySpeed={'25'}
                                                    dotPosition='left'
                                                >
                                                    {

                                                        [photoUrl]?.map((img, index) => (
                                                            <div key={index}>
                                                                <img alt={index} src={img} />
                                                            </div>
                                                        ))

                                                    }
                                                </Carousel>
                                            )
                                            :
                                        } */}
                                        {/* {
                                            quantityAvailable > 0
                                                ? (<Text strong type="secondary">Solo {quantityAvailable.toFixed(1)} disponibles</Text>)
                                                : (<Text strong type="danger" >Agotado</Text>)
                                        } */}
                                    </Col>
                                </Row>
                            </Col>
                            <Col
                                style={{ display: 'flex', alignItems: 'center' }}
                                xs={{ span: 12 }}
                                lg={{ span: 12 }}
                            >
                                <Row gutter={[{ xs: 0, sm: 0 }, { xs: 10, sm: 15 }]}
                                    justify={{ xs: 'center', sm: 'center' }}
                                    align={{ xs: 'top', sm: 'middle' }}
                                >

                                    <Col
                                        xs={{ span: 24, push: 2 }}
                                        sm={{ span: 22, push: 2 }}
                                    >
                                        <Row gutter={[{ xs: 15, sm: 20, lg: 5 }, 0]}
                                            justify={{ xs: 'center', sm: 'center' }}
                                            align={{ xs: 'middle', sm: 'middle' }}
                                        >
                                            <Col>
                                                <Button
                                                    ghost
                                                    type='primary'
                                                    icon={<MinusOutlined />}
                                                    style={{
                                                        display: 'flex',
                                                        alignItems: 'center',
                                                        justifyContent: 'center',

                                                    }}
                                                    htmlType={'button'}
                                                    onClick={() => onDecrement()}
                                                />
                                            </Col>
                                            <Col xs={{ span: 8 }}>
                                                <Input
                                                    value={counter}
                                                    style={{ textAlign: 'center' }}
                                                    onChange={onCounterChange}
                                                    name={'quantity'}
                                                />
                                            </Col>
                                            <Col>
                                                <Button icon={<PlusOutlined />}
                                                    ghost
                                                    type="primary"
                                                    style={{
                                                        display: 'flex',
                                                        alignItems: 'center',
                                                        justifyContent: 'center',
                                                    }}
                                                    htmlType={'button'}
                                                    onClick={() => onIncrement()}
                                                />

                                            </Col>

                                        </Row>
                                    </Col>
                                    <Col
                                        xs={{ span: 24, push: 2 }}
                                        sm={{ span: 22, push: 2 }}
                                        style={{ textAlign: 'center' }}
                                    >
                                        <Title style={{
                                            margin: 0,
                                            color: colorSbBg
                                        }} level={4} strong>
                                            {formatToMoney(((unitPrice * taxPercentage) / 100) + unitPrice)}
                                        </Title>
                                        <Text type="secondary" italic>ITBIS incluido</Text>
                                    </Col>

                                    <Col
                                        xs={{ span: 24, push: 1, pull: 2 }}
                                        sm={{ span: 16, push: 2 }}
                                        lg={{ span: 24, push: 2 }}
                                    >
                                        <Button
                                            type="primary"
                                            block
                                            disabled={btnDisabled}
                                            size={"large"}
                                            htmlType="button"
                                            onClick={handleAddToCart}
                                        >Agregar</Button>

                                    </Col>
                                </Row>
                            </Col>

                            <Col xs={{ span: 24 }}>
                                <Paragraph
                                    ellipsis={{
                                        rows: 3,
                                        expandable: true,
                                        symbol: (<Text strong>Ver mas</Text>),
                                    }}
                                >
                                    {description}
                                </Paragraph>
                            </Col>
                        </Row>
                    </Col>
                </Row>




            </Col>


        </Row>
    )
}
