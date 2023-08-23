import {  Col, Layout, Row } from "antd"
import { Content } from "antd/es/layout/layout"
import {useTheme} from '../hooks'
import LogoSb from '../assets/sb-logo.svg'
export const AuthLayout = ({ classname, children }) => {
  const {
    colorPrimary
  } = useTheme();
  return (
    <>
      <Layout
        className="page-layout"
        style={{
          height: '100vh',
          background:colorPrimary
        }}

      >
        <Content
          style={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center'
          }}
          className="layout-children"
          >
          <div
           className="container"

          >
            <div
            >
              <Row
                className="login-form-wrap"
                gutter={[0, { xs: 50 }]}
                // style={{width:'100%'}}
                // justify={{xs:'center',lg:'center'}}
                align={{xs:'middle',lg:'middle'}}
              >
                <Col 
                className="login-form-title" 
                xs={{ span: 22,offset:2 }} lg={{span:20,offset:4}} 
                // style={{width:'100%'}}
                >
                  <img
                    style={{
                      marginRight: 'auto',
                      // width: '150px',
                      marginBottom: '1em',
                    }}
                    // height={70} 
                    // width={150}
                    alt='Logo' 
                    src={LogoSb}
                     />
                </Col>
                <Col xs={{ span: 18,offset:3 }} lg={{span:12,offset:6}} >
                  {children}
                </Col>
              </Row>





            </div>
          </div>
        </Content>

      </Layout>

    </>
  )
}
