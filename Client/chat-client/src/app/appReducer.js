import { combineReducers } from 'redux-immutable'
import loginReducer from '../login/loginReducer'
import chatListReducer from '../chatContainer/chatList/chatListReducer'
import { State } from '../models'

export default combineReducers({
    currentUser: loginReducer,
    chats: chatListReducer
}, State)