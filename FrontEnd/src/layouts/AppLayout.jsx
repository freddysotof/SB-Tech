import { Spin,Layout } from 'antd';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useTheme, useUiStore } from '../hooks';
import { NavBar, SideBar } from './components';

const { Content } = Layout;


export const AppLayout = ({ children, appName }) => {
    const navigate = useNavigate();

    const {
        isSpinning,
        spinIcon,
        spinContent,
        spinSize
    } = useUiStore()


    return (
        <>

            <Layout
                hasSider
                suffixcls=''
            >
                <SideBar />
                <Content
                    className='content'
                    style={{ 
                        display: 'flex', 
                        flexFlow: 'column nowrap',
                        height: '100vh'
                    }}
                >
                    <NavBar />
                    <div className='content-body'
                        style={{
                            flex: '1 1 auto',
                            margin: '1em',
                        }}
                    >
                        <Spin
                            spinning={isSpinning}
                            indicator={spinIcon ?? undefined}
                            size={spinSize}
                            tip={spinContent}
                            //* En styles.css se define height:100%
                            wrapperClassName={'spin-layout'}
                        >
                            {children}
                        </Spin>
                    </div>
                </Content>
               
            </Layout>

        </>
    )
}
