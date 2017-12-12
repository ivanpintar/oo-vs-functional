import React from 'react'
import { Provider } from 'react-redux'
import LoginScreen from '../login/LoginScreen'
import store from '../store'

class App extends React.Component {
  render() {
    return (
      <Provider store={store}>
        <div>
          <LoginScreen />
        </div>
      </Provider>
    );
  }
}

export default App;
