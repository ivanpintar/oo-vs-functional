import React from 'react';
import ReactDOM from 'react-dom';
import App from './app/App';

const root = document.getElementById('root')
window.constants = {
    apiUrl: 'http://localhost:63081/'
}

ReactDOM.render(<App />, root);

if (module.hot) {
    module.hot.accept('./app/App', () => {
        const NextApp = require('./app/App').default
        ReactDOM.render(
            <NextApp />,
            root
        )
    })
}
