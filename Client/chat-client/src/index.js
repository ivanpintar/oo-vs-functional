import React from 'react';
import ReactDOM from 'react-dom';
import App from './app/App';
import registerSignalR from './signalr'
import store from './store';

const root = document.getElementById('root')

registerSignalR(store, () => ReactDOM.render(<App />, root))