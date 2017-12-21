import { createStore, applyMiddleware, compose } from 'redux'
import logger from 'redux-logger'
import rootReducer from './app/appReducer'
import rootEpic from './app/appEpic'
import { State } from './models' 
import { List } from 'immutable'
import { createEpicMiddleware } from 'redux-observable'
import 'rxjs';

const initialState = new State({
    currentUser: '',
    chats: List()
});
const enhancers = []
const middleware = [
    createEpicMiddleware(rootEpic),
    logger
]

if (process.env.NODE_ENV === 'development') {
    const devToolsExtension = window.devToolsExtension

    if (typeof devToolsExtension === 'function') {
        enhancers.push(devToolsExtension())
    }
}

const composedEnhancers = compose(
    applyMiddleware(...middleware),
    ...enhancers
)

const store = createStore(
    rootReducer,
    initialState,
    composedEnhancers
)

export default store