import { LogoutOutlined, UserOutlined } from '@ant-design/icons';
import { Avatar, Col, Dropdown, Row, Space, Typography } from 'antd';
import { NavLink } from 'react-router-dom';
import { useAuthStore } from '../../hooks';
const { Title, Text } = Typography;
export const UserInfo = () => {
    const { displayName, username, startLogout } = useAuthStore();

    const onLogout = () => {
        startLogout()
    }
    const items = [
        {
            key: '1',
            label: (
                <NavLink to={"profile"}>
                    <Space
                    >
                        <UserOutlined />
                        <Text >Mi Perfil</Text>
                    </Space>
                </NavLink>
            ),
        },
        {
            key: '2',
            label: (
                <Space

                    onClick={onLogout}
                >
                    <LogoutOutlined />
                    <Text >Cerrar sesion</Text>
                </Space>
            ),
        },


    ];


    return (

        <Row
            id='row-user-info'
            justify={{ xs: 'end', sm: 'center', lg: 'center' }}
            align={{ xs: 'middle', sm: 'middle', lg: 'middle' }}
        >

            <Col
                xs={{ span: 14, pull: 2 }}
                sm={{ span: 17, pull: 1 }}
                lg={{ offset: 10, span: 11 }}
                style={{
                    marginTop: '0.8rem',
                    textAlign: 'end'
                }}
            >
                <Title

                    style={{ color: 'white' ,      marginTop: '0',}}
                    level={5}>{displayName ?? username}</Title>
            </Col>
            <Col
                sm={{ span: 6 }}
                xs={{ span: 8 }}
                lg={{ offset: 0, span: 2 }}
            >
                <Dropdown

                    menu={{ items }}
                    placement="bottom"
                >
                    <a
                        className='text-decoration-none'
                        onClick={(e) => e.preventDefault()}>
                        <Avatar
                            size={{
                                xs: 35
                            }}
                            icon={<UserOutlined
                                style={{ fontSize: '22.5px' }}
                            />}
                        />
                    </a>
                </Dropdown>
            </Col>
        </Row>

    )
}
