import React from 'react'
import { Provider } from 'react-redux'
import LoginScreen from '../login/LoginScreen'
import ChatContainer from '../chatContainer/ChatContainer'
import store from '../store'

class App extends React.Component {
  render() {
    return (
      <Provider store={store}>
        <div>
          <LoginScreen />
          <ChatContainer />
        </div>
      </Provider>
    );
  }
}

export default App;
