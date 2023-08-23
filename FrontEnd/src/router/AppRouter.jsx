import { FloatButton, Spin } from 'antd'
import React, { useEffect } from 'react'
import { Navigate, Route, Routes } from 'react-router-dom'
import { useAuthStore, useCheckAuthGoogle, useUiStore } from '../hooks'
import { SBRoutes } from './'
import './AppRouter.css'
import { LoginPage } from '../pages/LoginPage'
export const AppRouter = () => {
  const { status, checkAuth } = useAuthStore();

  const {
    backTopVisible
  } = useUiStore()

  useEffect(() => {
    checkAuth()
  }, [])

  
  const authGoogle= useCheckAuthGoogle();


  // if (status === 'checking')
  //   return <CheckingAuth />;

  const { BackTop } = FloatButton;
  return (
    <>
      <Spin size={"large"} spinning={status === 'checking'}
        wrapperClassName='spin-layout'
      >
        <Routes>

          {
            (status === 'not-authenticated' || status === 'checking')
              ? (
                <>
                  <Route path='/auth/*' element={<LoginPage />} />
                  <Route path='/*' element={<Navigate to={'/auth/login'} />} />
                </>

              )
              : (
                <>
                  <Route path='/*' element={<SBRoutes />} />

                </>

              )
          }
        
        </Routes>

      </Spin>
      {
        backTopVisible
          ? <FloatButton.BackTop />
          : (<></>)
      }

    </>

  )
}
