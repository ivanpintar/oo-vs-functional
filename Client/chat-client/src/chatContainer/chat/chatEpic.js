import { combineEpics } from 'redux-observable';
import { leftChatAction, chatLoadedAction } from './chatActions'
import { Observable } from 'rxjs/Observable'
import { ajax } from 'rxjs/observable/dom/ajax'
import constants from '../../constants'


const getChatUrl = constants.apiUrl + 'chat/'
const sendMessageUrl = constants.apiUrl + 'chat/sendMessage'
const leaveChatUrl = constants.apiUrl + 'chat/leave'

const loadChatEpic = (action$, store) =>
    action$.ofType('CHAT.LIST.CHAT_SELECTED')
        .mergeMap(a => {
            const chat = store.getState().chats.find(c => c.name === a.name)
            if(chat.isLoaded) return Observable.of({ type: 'VOID' })

            return ajax.getJSON(getChatUrl + a.name)
                .map(chatLoadedAction)
        })

const sendMessageEpic = (action$) => 
    action$.ofType('CHAT.CHAT.SEND_MESSAGE')   
        .mergeMap(a => {
            const message = { chatName: a.chatName, from: a.from, text: a.text }
            return ajax.post(sendMessageUrl, message, { 'Content-Type': 'application/json' })
                .map(response => ({ type: 'VOID' }));                
        })

const leaveChatEpic = (action$) =>
    action$.ofType('CHAT.CHAT.LEAVE')       
        .mergeMap(a => {
            const message = { chatName: a.chatName, participant: a.userThatLeft }
            return ajax.post(leaveChatUrl, message, { 'Content-Type': 'application/json' })
                .map(response => ({ type: 'VOID' }));           
        })

export default combineEpics(loadChatEpic, sendMessageEpic, leaveChatEpic)