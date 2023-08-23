import { Button, Col, Layout, Result, Row } from "antd"

const { Content } = Layout;
export const ActionResult = ({ children,status = "success", title, subTitle, buttonTitle = "Continuar", onClick }) => {
  return (
    <Layout 
    className="layout" 
    >
      <Content
      style={{display:'flex',alignItems:'center',justifyContent:'center'}}
      >
        <Row
          style={{ height: '100%' }}
          justify={{ xs: 'center', lg: 'center' }}
          align={{ xs: 'middle', lg: 'middle' }}
        >
          <Col xs={{span:24}}>
            <Result
              style={{ alignSelf: 'center' }}
              status={status}
              title={title}
              subTitle={subTitle}
              extra={[
                <Button
                  type='primary'
                  key={"continue"}
                  htmlType="button"
                  onClick={onClick}
                >
                  {buttonTitle}
                </Button>
              ]}
            />
          </Col>
          <Col xs={{span:24}}>
            {children}
          </Col>
        </Row>
      </Content>
    </Layout>
  )
}
