import {  Col, Layout, Row, Space, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';
import { useTheme } from '../../hooks';
import { UserInfo } from './UserInfo';
import LogoSb from '../../assets/sb-logo.svg'
//create a fixed header?

const { Header } = Layout;
const { Title } = Typography;
export const NavBar = () => {

    const {
        colorPrimary,
        colorInfoBg,
        colorInfo,

    } = useTheme();
    const style = {
        background: '#0092ff',
        padding: '8px 0',
    };

    const navigate = useNavigate()
    const onLogout = () => {
        // logout();
        navigate('/login', {
            replace: true
        });

    }

    return (
        <Header
            style={{
                padding: 0,
                background: colorInfo,
                marginBottom: '5px',
                width: '100%',
                backgroundColor: colorPrimary,
                color: 'white'
            }}

        >
            <Row
                style={{ width: '100%' }}
                justify={{ xs: 'center', lg: 'center' }}
                align={{ xs: 'middle', lg: 'middle' }}
            >
                <Col
                    xs={{ offset: 2, span: 10 }}
                    sm={{ span: 6, offset: 0, pull: 1 }}
                    lg={{ offset: 0, span: 2 }}
                >
                    <Space direction='horizontal'>
                        <img
                            style={{ marginRight: 'auto' }}
                            
                            height={25} 
                            // width={300} 
                            alt='Logo'
                            src={LogoSb}
                        />
                    </Space>

                </Col>
                <Col
                    xs={{ span: 0, offset: 0, push: 0, pull: 0 }}
                    sm={{ span: 8, push: 0 }}
                    lg={{ offset: 6, span: 5, }}
                >
                    <Title
                        style={{ color: 'white',margin:0, marginTop:'0.8rem', }}
                        level={4}

                    >  Prueba Técnica App</Title>
                </Col>
                <Col
                    xs={{ offset: 0, span: 12, pull: 0, push: 1 }}
                    sm={{ span: 6, push: 1 }}
                    lg={{ offset: 0, span: 9,push:0 }}
                >
                    <UserInfo />
                </Col>
            </Row>



        </Header >
    )
}
