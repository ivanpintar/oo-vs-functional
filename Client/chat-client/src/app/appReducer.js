import { combineReducers } from 'redux'
import loginReducer from '../login/loginReducer'

export default combineReducers({
    userName: loginReducer,
    chats: (s = [], a) => { return s }
})