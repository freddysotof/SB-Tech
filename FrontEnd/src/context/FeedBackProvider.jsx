
import { message, notification } from 'antd';
import { createContext } from 'react';
import { FeedbackContext } from './FeedbackContext'

export const FeedBackProvider = ({ children }) => {
  const [messageApi, messageContextHolder] = message.useMessage();
  const [notificationApi, notificationContextHolder] = notification.useNotification();

  const showSuccessMessage = (content, duration = 1.5) => {
    messageApi.open({
      type: 'success',
      content,
      duration
    });
  }

  const showWarningMessage = (content, duration = 1.5) => {
    messageApi.open({
      type: 'warning',
      content,
      duration
    });
  };


  const showErrorMessage = (content, duration = 1.5) => {
    messageApi.open({
      type: 'error',
      content,
      duration
    });
  }
  const showNotification = (title, content, placement = 'topRight', duration = 1.5, icon = null) => {
    notificationApi.info({
      message: title,
      description: content,
      placement,
      duration,
      icon
    });
  };

  return (
    <FeedbackContext.Provider value={{
      showSuccessMessage,
      showWarningMessage,
      showErrorMessage,
      showNotification
    }}>
      <div key={'messageContextHolder'}>
        {messageContextHolder}
      </div>
      <div key={'notificationContextHolder'}>
        {notificationContextHolder}
      </div>
      {children}
    </FeedbackContext.Provider>
  )

}