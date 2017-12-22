import React from 'react'
import { Provider } from 'react-redux'
import LoginScreen from '../user/LoginScreen'
import ChatContainer from '../chatContainer/ChatContainer'
import store from '../store'

export default () => (
    <Provider store={store}>
        <div>
            <LoginScreen />
            <ChatContainer />
        </div>
    </Provider>
)
