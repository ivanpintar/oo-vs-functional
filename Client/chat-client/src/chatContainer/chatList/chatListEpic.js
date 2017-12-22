import { combineEpics } from 'redux-observable';
import { chatExistsAction, chatsLoadedAction } from './chatListActions'
import constants from '../../constants'
import { Observable } from 'rxjs/Observable'
import { ajax } from 'rxjs/observable/dom/ajax'

const createChatUrl = constants.apiUrl + 'chat/create'
const getChatsUrl = constants.apiUrl + 'chat/list'

const loggedInEpic = (action$) =>
    action$.ofType('USER.LOGGED_IN')
        .mergeMap(a => 
            ajax.getJSON(getChatsUrl)
                .map(chatsLoadedAction)
        )

const createChatEpic = (action$, store) => 
    action$.ofType('CHAT.LIST.CREATE_CHAT')
        .mergeMap(a => 
            ajax.post(createChatUrl, { name: a.name }, { 'Content-Type': 'application/json'})
                .map(response => ({ type: 'VOID'}))
                .catch(response => Observable.of(chatExistsAction(a.name)))
        )

export default combineEpics(loggedInEpic, createChatEpic)