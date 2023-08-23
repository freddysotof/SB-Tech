import { SearchOutlined } from "@ant-design/icons"
import { Button, Col, Input, List, Modal, Row, Skeleton, Typography } from "antd"
import { useEffect, useRef, useState } from "react";
import { useProductStore, useTheme, useUiStore } from "../../../../hooks";
import { ProductCard } from "./ProductCard";

const { Title, Text } = Typography;
export const FindProductModal = () => {
    const inputRef = useRef();
    const {
        productModal,
        closeProductModal,
    } = useUiStore();
    const [value, setValue] = useState(null)
    const [confirmLoading, setConfirmLoading] = useState(false)
    const {
        colorSbActiveBg,
        colorSbText
    } = useTheme();


    const {
        products,
        isLoading,
        activeProduct,
        hasActiveProduct,
        setActiveProduct,
        startLoadingProducts,
        startSearchingProductByCode,
        startClearProducts
    } = useProductStore();




    const onSelectProduct = async (code) => {
        setConfirmLoading(true);
        await startSearchingProductByCode(code);
        setConfirmLoading(false);
        closeProductModal()
    }

    const onValueChange = async ({ target }) => {
        setValue(target.value);

    }

    const onKeyDown = async ({ key, target }) => {
        if (target.value.length > 0 && (key === 'Enter' || key === 'Tab'))
            await startLoadingProducts(target.value)

    }
    const searchProduct = async () => {
        await startLoadingProducts(value)
    }

    const onCancelModal = () => {
        closeProductModal();
        startClearProducts()
    }

    useEffect(() => {
        inputRef.current?.focus({
          cursor: 'all',
        });
      }, [products])




    return (
        <Modal
            closable={false}
            open={productModal.isModalOpen}
            okButtonProps={{ disabled: !hasActiveProduct,hidden:true }}
            onCancel={() => onCancelModal()}
            forceRender={false}
            keyboard={false}
            confirmLoading={confirmLoading}
        >
            <Row
                style={{ height: '100%' }}
                gutter={[0, { xs: 10,sm:15 }]}
            >
                <Col
                    xs={{ span: 24 }}
                    style={{ height: 'fit-content' }}
                >
                    <Row
                        style={{
                            marginTop: '1em',
                            height: '100%'
                        }}
                        gutter={[{ xs: 10,sm:15 }, 0]}
                    >
                        <Col xs={{ span: 20 }}>
                            <Input
                                placeholder='Nombre/Codigo'
                                value={value}
                                name='value'
                                allowClear
                                onChange={onValueChange}
                                onKeyDown={onKeyDown}
                                ref={inputRef}
                            />
                        </Col>

                        <Col xs={{ span: 4 }}>
                            <Button
                            htmlType="button"
                                type='primary'
                                block
                                onClick={searchProduct}
                                icon={<SearchOutlined />}
                                style={{
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center'
                                }}
                            />
                        </Col>
                    </Row>
                </Col>

                <Col
                    xs={{ span: 24 }}
                    style={{ height: '100%' }}
                >

                    <List
                        style={{
                            height: '35em',
                            overflow: 'auto',
                        }}
                        dataSource={products}
                        loading={isLoading && products.length === 0}
                        size={"small"}
                        pagination={{
                            onChange: (page) => {
                                // console.log(page)
                            },
                            pageSize: 5,
                            position: 'bottom',
                            align: 'center',
                            style: {
                                marginTop: 'auto !important'
                            }
                        }}

                        split={false}
                        renderItem={(product,index) => {
                            let cardStyle = {}
                            if (hasActiveProduct)
                                cardStyle = {
                                    border: activeProduct.code === product.code ? colorSbActiveBg : undefined,
                                    background: activeProduct.code === product.code ? colorSbActiveBg : undefined,
                                    color: activeProduct.code === product.code ? colorSbText : undefined
                                }

                            const handleCardClick = async () => {
                                setActiveProduct(product)
                                await onSelectProduct(product.code);
                            }

                            return (
                                <List.Item
                                    key={index}
                                    style={{
                                        marginBottom: '1em',
                                        width:'100%'
                                    }}
                                >
                                    <Skeleton
                                        round={true}
                                        paragraph={{
                                            rows: 3,
                                            width: '100%',
                                        }}
                                        title={false}
                                        loading={isLoading}
                                        active
                                        style={{
                                            width:'100%'
                                        }}
                                    >
                                        <ProductCard {...product} size={'large'} onCardClick={handleCardClick} cardStyle={cardStyle} />
                                    </Skeleton>
                                </List.Item>

                            )
                        }}
                    >
                    </List>
                </Col>

            </Row>

        </Modal >
    )
}
