import { IssuesCloseOutlined, ScanOutlined, SearchOutlined } from '@ant-design/icons';
import { Alert, Badge, Button, Card, Col, Descriptions, Divider, Drawer, Grid, Input, List, Progress, Radio, Row, Space, Spin, Tag, Typography } from 'antd'
import  {  useEffect, useRef, useState } from 'react'
import {  useCartStore,  useProductStore, useTheme, useUiStore } from '../../../hooks';
import {
    SwipeableList,
} from 'react-swipeable-list';

import 'react-swipeable-list/dist/styles.css'
import { CartItem, FindProductModal, ProductDrawer,  SummaryItem } from '../components';
import { formatToCurrency } from '../../../helpers';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBottleWater, faBox, faGift,  faWhiskeyGlass, faWineBottle } from '@fortawesome/free-solid-svg-icons';
import { ScanBarcodeModal } from '../components/cart/ScanBarcodeModal';

const { Title, Text } = Typography;

const {
    useBreakpoint
} = Grid;
export const CartView = () => {
    const inputRef = useRef();
    const [productCode, setProductCode] = useState(null)
    const [drawerPlacement, setDrawerPlacement] = useState('bottom');
    const [drawerFullScreen, setDrawerFullScreen] = useState(true)
    const screens = useBreakpoint();
    const {
        setLoadingPage,
        setPageLoaded,
        productModal,
        openProductModal,
        barcodeScannerModal,
        showErrorMessage,
        openBarcodeScannerModal
    } = useUiStore();




    const {
        colorSbActiveBg
    } = useTheme();



    const {
         items,

        activeItem,
        isActiveItem,
        summary,
        setActiveItem,
    } = useCartStore();

    const {
        errorMessage,
        hasActiveProduct,
        setActiveProduct,
        startSearchingProductByBarcode,
        startLoadingProducts,
        clearErrorMessage
    } = useProductStore();



    const onCloseDrawer = () => {
        setActiveItem(null);
        if (hasActiveProduct) {
            openProductModal();
            setActiveProduct(null);
        }

    }

    const onInputChange = ({ target }) => setProductCode(target.value)
    const onInputKeyDown = async ({ target, key, ...rest }) => {

        if (target.value.length == 0)
            return;
        if (key === "Enter") {
            setLoadingPage();
            await startSearchingProductByBarcode(target.value);
            setPageLoaded();
        }

    }

    const onClickSearchProduct = () => {
        startLoadingProducts();
        openProductModal();
    }

    const onClickScanBarcode = () => {
        openBarcodeScannerModal()
    }

    useEffect(() => {
        // setDrawerOpen(isActiveItem);
        if (!isActiveItem) {
            setProductCode(null)
            inputRef?.current?.focus({
                cursor: 'all',
            });
        }

    }, [isActiveItem])

    useEffect(() => {
        if (errorMessage?.length > 0) {
            setProductCode('')
            showErrorMessage(errorMessage)
            clearErrorMessage()
        }
    }, [errorMessage])


    

    useEffect(() => {
        if (screens.lg || screens.xl || screens.xxl) {
            setDrawerFullScreen(false);
            setDrawerPlacement('right')
        }
        else {
            setDrawerFullScreen(false);
            setDrawerPlacement('bottom');
        }
    }, [screens])


    if (productModal.isModalOpen)
        return (<FindProductModal />)

    if (barcodeScannerModal.isModalOpen)
        return (<ScanBarcodeModal />)
    return (
        <>
            <Row gutter={[0, { xs: 10, sm: 15 }]} justify={{ xs: 'center' }} wrap
            >
                <Col
                    xs={{ span: 24 }}
                    sm={{ span: 22, push: 0, pull: 0 }}
                    lg={{ span: 23,offset:0, push: 0, pull: 0, order: 1 }}
                >
                    <Row>

                        <Col xs={{ span: 18 }}
                            sm={{ span: 18, push: 1 }}
                            lg={{span:20,push:0}}
                        >
                            <Input
                                size={"large"}
                                onKeyDown={onInputKeyDown}
                                onChange={onInputChange}
                                placeholder={'Escanee el producto'}
                                value={productCode}
                                autoFocus={false}
                                ref={inputRef}
                            />
                        </Col>

                        <Col
                            xs={{ span: 2, push: 1 }}
                            sm={{ span: 2, push: 1 }}
                            lg={{ span: 2, push: 1 }}
                        >
                            <Button
                                htmlType="button"
                                type='primary'
                                size={'large'}
                                block
                                onClick={() => onClickScanBarcode()}
                                icon={<ScanOutlined />}
                                style={{
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center'
                                }} />
                        </Col>
                        <Col
                            xs={{ span: 2, push: 2 }}
                            sm={{ span: 2, push: 2 }}
                            lg={{ span: 1, push: 1 }}
                        >
                            <Button
                                htmlType="button"
                                type='primary'
                                size={'large'}
                                block
                                onClick={onClickSearchProduct}
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
                    sm={{ span: 22, push: 0, pull: 0 }}
                    lg={{ span: 24, pull: 0, push: 0, order: 3 }}
                    style={{
                        width: '100%',
                        display: 'flex',
                        flexFlow: 'column wrap'
                    }}
                >

                    <Card
                        headStyle={{ textAlign: 'center', background: colorSbActiveBg, color: 'white' }}
                        bodyStyle={{
                            height: '22rem',
                            overflow: 'auto',
                            padding: 0
                        }}
                        className='site-card-border-less-wrapper scrollable-container'
                        title="Carrito"
                        bordered
                    >
                        <SwipeableList
                        >
                            {
                                items.map(item => (
                                    <CartItem
                                        key={item.id} item={item} {...item} />
                                ))
                            }
                        </SwipeableList>
                    </Card>
                </Col>
                <Col
                    xs={{ span: 24 }}
                    sm={{ span: 22, push: 0, pull: 0 }}
                    md={{ span: 12, push: 0, pull: 0, order: 4 }}
                    lg={{ span: 0, push: 0, pull: 0, offset: 0, order: 4 }}
                    xxl={{ span: 0, push: 0, pull: 0, order: 4 }}
                    style={{
                        height: 'auto',
                        width: '100%',
                    }}
                >
                    <List
                        itemLayout='horizontal'
                        size={'small'}
                        split={false}
                        dataSource={Object.values(summary)}
                        renderItem={(item, index) => {
                            return (
                                <SummaryItem key={index} item={item} />

                            )
                        }}
                    >
                    </List>
                </Col>

            </Row >
            {
                activeItem
                    ? (
                        <Drawer
                            placement={drawerPlacement}
                            open={isActiveItem}
                            size={"default"}
                            onClose={onCloseDrawer}
                            closable={true}
                        >
                            <ProductDrawer />
                        </Drawer>
                    )
                    : <></>
            }
        </>
    )
}
