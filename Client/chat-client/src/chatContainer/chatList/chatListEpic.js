import { combineEpics } from 'redux-observable';
import { chatExistsAction, chatsLoadedAction, chatNameInvalidAction } from './chatListActions'
import constants from '../../constants'
import { Observable } from 'rxjs/Observable'
import { ajax } from 'rxjs/observable/dom/ajax'
import { serverErrorAction } from '../../app/appActions';

const createChatUrl = constants.apiUrl + 'chat/create'
const getChatsUrl = constants.apiUrl + 'chat/list'

const loggedInEpic = (action$) =>
    action$.ofType('USER.LOGGED_IN')
        .mergeMap(a => 
            ajax.getJSON(getChatsUrl)
                .map(chatsLoadedAction)
                .catch(error => Observable.of(serverErrorAction(error)))
        )

const createChatEpic = (action$, store) => 
    action$.ofType('CHAT.LIST.CREATE_CHAT')
        .mergeMap(a => 
            ajax.post(createChatUrl, { name: a.name }, { 'Content-Type': 'application/json'})
                .map(response => ({ type: 'VOID'}))
                .catch(error => {
                    switch(error.status){
                        case 409:
                            return Observable.of(chatExistsAction(a.name))
                        case 400:
                            return Observable.of(chatNameInvalidAction())
                        default:
                            return Observable.of(serverErrorAction(error))                            
                    }
                })
                    
        )

export default combineEpics(loggedInEpic, createChatEpic)