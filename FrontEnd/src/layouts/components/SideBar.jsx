import { HomeOutlined, SettingOutlined, ShoppingCartOutlined, TagsOutlined } from "@ant-design/icons";
import { Menu, Layout } from "antd";
import React, { useEffect, useRef, useState } from 'react'
import { useLocation } from "react-router-dom";
import { useAuthStore, useOrderStore, useTheme, useUiStore } from "../../hooks";
import { NavMenuItem } from "../../ui/components";
import { v4 as uuidv4 } from 'uuid';
import { getMenuItem } from "../../ui/utils";
const { Sider } = Layout;

export const SideBar = () => {
    const sideBarRef = useRef(null);
    const location = useLocation();
    const [currentMenuOption, setCurrentMenuOption] = useState(location.pathname);
    const [items, setItems] = useState([])
    const { isAdmin } = useAuthStore();
    const {
        colorInfo,
    } = useTheme();

    const { sideBar, toggleSideBar, closeSideBar } = useUiStore();

    const { setActiveOrder, setOrderSaved } = useOrderStore();
    const onClickSalesPage = () => {
        setOrderSaved();
    }



    useEffect(() => {
        setCurrentMenuOption(location.pathname);
    }, [location]);


    useEffect(() => {
        // Add event listener to the document object
        document.addEventListener('mousedown', handleClickOutside);

        // Remove event listener when the component unmounts
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);



    function handleClickOutside(event) {
        if (sideBarRef.current && !sideBarRef.current.contains(event.target)) {
            closeSideBar()
            // Clicked outside the side navigation bar, close it
            // Implement your close side navigation bar logic here
        }
    }

    useEffect(() => {
        const menu = [getMenuItem(
            uuidv4(),
            (<span style={{ color: 'white' }}>Inicio</span>),
            (<NavMenuItem
                path={'/'}
                icon={<HomeOutlined style={{ color: 'white' }} />}
            />
            ),
            colorInfo,
            // 'group'
        ),
        getMenuItem(
            uuidv4(),
            (<span style={{ color: 'white' }}>Pedido</span>),
            (<NavMenuItem
                path={'/sales'}
                icon={<ShoppingCartOutlined />}
            />
            ),
            colorInfo,
            null
        )
        ]
        if (isAdmin)
            menu.push(getMenuItem(
                uuidv4(),
                (<span style={{ color: 'white' }}>Productos</span>),
                (<NavMenuItem
                    path={'/products'}
                    icon={<TagsOutlined />}
                />
                ),
                colorInfo,
                null
            )
            )

        setItems(menu)
    }, [])


    return (
        <Sider
            ref={sideBarRef}
            defaultCollapsed={false}

            collapsed={!sideBar.isSideBarOpen}
            breakpoint="md"
            collapsedWidth="0"
            collapsible={true}
            zeroWidthTriggerStyle={{
                top: '15px',
                backgroundColor: 'rgba(0, 0, 0, 0.88)'
            }}
            onBreakpoint={(broken) => {
                // console.log(broken);
            }}
            onCollapse={(collapsed, type) => {
                // console.log(collapsed, type);
                toggleSideBar();

            }}
            reverseArrow={false}
        >
            <Menu
                theme="dark"
                mode="inline"
                defaultSelectedKeys={currentMenuOption}
                items={items}

                style={{
                    height: '100%', borderRight: 0,

                    backgroundColor: 'black'
                }}
                selectedKeys={currentMenuOption}
            />
        </Sider>
    )
}
