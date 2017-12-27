import { List } from 'immutable'
import { Chat, Message } from '../../models'
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
        case 'CHAT.LIST.LOADED':  
            return List(action.chats.map(c => new Chat({
                    name: c.name,
                    selected: false,
                    loaded: false,
                    messages: List(c.messages.map(m => new Message({ order: m.order, text: m.text, from: m.from }))),
                    participants: List(c.participants),
                })))
        case 'CHAT.LIST.CHAT_CREATED': 
            return chatList.push(new Chat({
                name: action.name,
                selected: false,
                loaded: false,
                messages: List(),
                participants: List(),
            }));
        case 'CHAT.LIST.CHAT_EXISTS':
            alert('Chat "' + action.name + '" already exists');
            return chatList;
        case 'CHAT.LIST.CHAT_NAME_INVALID': 
            alert('Chat name is invalid');
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