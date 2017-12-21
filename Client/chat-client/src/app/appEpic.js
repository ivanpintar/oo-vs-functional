import { combineEpics } from 'redux-observable'
import chatEpic from '../chatContainer/chat/chatEpic'
import chatListEpic from '../chatContainer/chatList/chatListEpic'
import loginEpic from '../login/loginEpic'

export default combineEpics(
    chatEpic,
    loginEpic,
    chatListEpic
)