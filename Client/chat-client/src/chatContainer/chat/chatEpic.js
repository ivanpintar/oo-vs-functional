import { combineEpics } from 'redux-observable';
import { messageReceivedAction, leftChatAction, chatLoadedAction } from './chatActions'
import { Observable } from 'rxjs/Observable'
import { ajax } from 'rxjs/observable/dom/ajax'
import constants from '../../constants'


const getChatUrl = constants.apiUrl + 'chat/'

const loadChatEpic = (action$, store) =>
    action$.ofType('CHAT.LIST.CHAT_SELECTED')
        .mergeMap(a => {
            const chat = store.getState().chats.find(c => c.name === a.name)
            if(chat.isLoaded) return Observable.of({ type: 'VOID' })

            return ajax.getJSON(getChatUrl + a.name)
                .map(chatLoadedAction)
        })

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

export default combineEpics(loadChatEpic, sendMessageEpic, leaveChatEpic)