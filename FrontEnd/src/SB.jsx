import { ToastContainer } from 'react-toastify'
import { FeedBackProvider } from './context'
import { AppRouter } from './router/AppRouter'
import { AppTheme } from './theme'

import 'react-toastify/dist/ReactToastify.css';
export const SB = () => {

  window.onbeforeunload = function () {
    window.scrollTo(0, 0);
  }
  return (
      <FeedBackProvider>
        <AppTheme>
          <ToastContainer
            autoClose={3000}
            hideProgressBar
            newestOnTop={false}
            closeOnClick
            rtl={false}
            pauseOnFocusLoss={false}
            draggable
            pauseOnHover={false}
          />
          {/* <FirebaseNotification /> */}
          <AppRouter />
        </AppTheme>

      </FeedBackProvider>
  )
}
