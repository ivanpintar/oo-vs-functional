import { combineReducers } from 'redux-immutable'
import userReducer from '../user/userReducer'
import chatListReducer from '../chatContainer/chatList/chatListReducer'
import { State } from '../models'

export default combineReducers({
    currentUser: userReducer,
    chats: chatListReducer
}, State)