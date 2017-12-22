import { combineEpics } from 'redux-observable';
import { messageReceivedAction, leftChatAction } from './chatActions'

const sendMessageEpic = (action$, store) => 
    action$.ofType('CHAT.CHAT.SEND_MESSAGE')        
        .delay(1000) // TODO: api call to send a message
        .map(a => {
            const order = store.getState().chats.find(c => c.name === a.chatName).messages.count() + 1;
            return messageReceivedAction(a.chatName, a.from, a.text, order)
        })

const leaveChatEpic = (action$) =>
    action$.ofType('CHAT.CHAT.LEAVE')
        .delay(1000) // TODO: API call to remove the participant from the chat
        .map(a => leftChatAction(a.chatName, a.userThatLeft))

export default combineEpics(sendMessageEpic, leaveChatEpic)