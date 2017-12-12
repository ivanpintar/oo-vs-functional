import { createStore, applyMiddleware, compose } from 'redux'
import rootReducer from './app/appReducer'

const initialState = {
    userName : '',
    chats : [],
}
const enhancers = []
const middleware = [
    
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