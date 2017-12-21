import { List } from 'immutable'
import { Chat } from '../../models'
import chatReducer from '../chat/chatReducer'

export default function chatListReducer(chatList = List(), action) {
    if(action.type.indexOf('CHAT.CHAT') === 0) {
        const updateChat = (c) => {
            if(c.name === action.chatName) {
                return chatReducer(c, action);
            }

            return c;
        }
        chatList = chatList.map(updateChat);
    }
    
    switch(action.type) {        
        case 'CHAT.LIST.CHAT_CREATED': 
            return chatList.push(new Chat({
                name: action.name,
                selected: false
            }));
        case 'CHAT.LIST.CHAT_EXISTS':
            alert('Chat "' + action.name + '" already exists');
            return chatList;
        case 'CHAT.LIST.CHAT_SELECTED':
            return chatList.map(c => {
                if(c.name === action.name){
                    return c.set('selected', true)
                } 
                return c.set('selected', false);
            });
        case 'CHAT.CHAT.LEAVE':
            return chatList.map(c => {
                if(c.name === action.chatName){
                    return c.set('selected', false);
                } 
                return c;
            });
        default:
            return chatList;
    }
}