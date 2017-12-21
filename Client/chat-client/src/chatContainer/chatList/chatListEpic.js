import { ofType, combineEpics } from 'redux-observable';
import { delay } from 'rxjs/operators';
import { chatCreatedAction, chatExistsAction, chatSelectedAction, selectChatAction } from './chatListActions'

const createChatEpic = (action$, store) => 
    action$.ofType('CHAT.LIST.CREATE_CHAT')
        .delay(1000)
        .map(a => {
            const chat = store.getState().chats.find(c => c.name === a.name);
            if(chat) return chatExistsAction(a.name);

            return chatCreatedAction(a.name)
        })

const selectChatEpic = (action$, store) => 
    action$.ofType('CHAT.LIST.SELECT_CHAT')
        .delay(1000)
        .map(a => chatSelectedAction(a.name))

export default combineEpics(createChatEpic, selectChatEpic)