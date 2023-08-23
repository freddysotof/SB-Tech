
import { Form, Input, Checkbox, Space, Alert, Button } from 'antd';
import { LockOutlined, UserOutlined, GoogleOutlined } from "@ant-design/icons";
import { useMemo, useState } from 'react';
import { AuthLayout } from '../layouts';
import { Link } from 'react-router-dom'
import { regexNumberValidation } from '../helpers';
import { useAuthStore } from '../hooks';

const initialForm = {
  email: '',
  password: '',
}

export const LoginPage = () => {

  const {
    employeeCode,
    status,
    errorMessage,
    authenticationType,
    startLogin,
    startLoginWithGoogle,
  } = useAuthStore();

  const [formSubmitted, setFormSubmitted] = useState(false)

  const [form] = Form.useForm();

  const isAuthenticating = useMemo(() => status === 'checking', [status]);




  const onSubmit = ({ email, password }) => {
    setFormSubmitted(true);
    startLogin({ email, password });
  }

  const tailLayout = {
    wrapperCol: { offset: 6, span: 18 },
  };
  const onGoogleSignIn = async () => {
    await startLoginWithGoogle();
  }


  return (
    <AuthLayout title="LOGIN" classname="login">

      <Form
        form={form}
        initialValues={initialForm}
        onFinish={onSubmit}
        autoComplete="off"
        className="login-form "
      >
        <Form.Item
          name="email"
          preserve={true}
          rules={[
            {
              required: true,
              message: `Digite su correo`,
            },
          ]}
        >
          <Input
            prefix={
              <UserOutlined
                className="site-form-item-icon"
                style={{ color: 'rgba(0,0,0,.25)' }} />
            }
            placeholder={"Email"}
            allowClear
          />
        </Form.Item>
        <Form.Item

          name="password"

          rules={[
            { required: true, message: 'Digite su contrasena!' },
          ]}
        >
          <Input.Password
            prefix={
              <LockOutlined
                className="site-form-item-icon"
                style={{ color: 'rgba(0,0,0,.25)' }} />
            }
            allowClear
            placeholder="Contrasena"
          />
        </Form.Item>
        <Form.Item
          style={{
            display: !!errorMessage ? '' : 'none'
          }
          }
        >
          <Space
            direction="vertical"
            style={{
              width: '100%',
            }}
          >
            <Alert message={errorMessage} type="error" showIcon />
          </Space>
        </Form.Item>
        <Form.Item >
          <Space size={"middle"} style={{ width: '100%' }} direction={"vertical"}>
            <Button block
              // type='primary'
              className="login-form-button"
              disabled={isAuthenticating}
              htmlType='submit'
            >
              Iniciar Sesion
            </Button>
            <Button
            htmlType='button'
              block
              disabled={isAuthenticating}
              variant="contained"
              fullWidth
              onClick={onGoogleSignIn}
              aria-label="google-btn"
            >
              <GoogleOutlined /> Google
            </Button>
          </Space>

        </Form.Item>
      </Form>
    </AuthLayout >
  )

}
