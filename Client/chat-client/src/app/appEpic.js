import { combineEpics } from 'redux-observable'
import chatEpic from '../chatContainer/chat/chatEpic'
import chatListEpic from '../chatContainer/chatList/chatListEpic'
import userEpic from '../user/userEpic'

export default combineEpics(
    chatEpic,
    userEpic,
    chatListEpic
)