import { combineReducers } from 'redux-immutable'
import userReducer from '../user/userReducer'
import chatListReducer from '../chatContainer/chatList/chatListReducer'
import { State } from '../models'

const errorReducer = (error = '', action) => {
    switch(action.type) {
        case 'SERVER_ERROR':
            alert('Server error: ' + action.error.status + ' - ' + action.error.message);
            return error;
        default: 
            return error;
    }
}

export default combineReducers({
    currentUser: userReducer,
    chats: chatListReducer,
    error: errorReducer
}, State)